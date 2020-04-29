using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane02 : MonoBehaviour
{
    private GameController gameCtr;
    // Start is called before the first frame update
    Animator animator;
    Rigidbody2D rigidbody2d;
    public float maxHealth;
    public float health { get { return currentHealth;}}
    float currentHealth;
    public ParticleSystem Smoke;
    public GameObject bullet;
    public GameObject upgradeobj;
    public float attackgap;
    float timer = 0;
    public float speeddown;
    public float speedhrz;
    float boomtimer = 0.8f;
    bool isdestroy = false;
    public float downtimer;
    public float time_hrz;
    float hrztimer;
    public float idletimer;

    // Start is called before the first frame update
    void Start()
    {
        gameCtr = GameController.instance;

        currentHealth = maxHealth;
        Smoke.Stop();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = attackgap;
        animator = GetComponent<Animator>();
        hrztimer = time_hrz;
    }

    // Update is called once per frame
    void Update()
    {
        if (isdestroy) {
            boomtimer -= Time.deltaTime;
            if (boomtimer < 0) {
                releaseUpgrade();
                Destroy(gameObject);
            }
            return;
        }
        if (timer < 0) {
            Fire();
            timer = attackgap;
        }
        if (currentHealth <= 0) {
            isdestroy = true;
            animator.SetBool("isdestroyed", true);
            rigidbody2d.simulated = false;
            Smoke.Stop();
            
            return;
        }
        timer -= Time.deltaTime;
        Vector2 position = rigidbody2d.position;
        if (downtimer > 0) { 
        position.y = position.y + Time.deltaTime * -speeddown;
        rigidbody2d.MovePosition(position);
        downtimer -= Time.deltaTime;
        return;
        }
        if (idletimer > 0) {
            idletimer -= Time.deltaTime;
            return;
        }
        position.x = position.x +Time.deltaTime * speedhrz;
        rigidbody2d.MovePosition(position);
        hrztimer -= Time.deltaTime;
        if (hrztimer < 0) {
            hrztimer = 2 *time_hrz;
            speedhrz = speedhrz * -1;
        }
    }

    public void ChangeHealth(float amount)
    {
        Debug.Log(currentHealth);
        if (currentHealth < maxHealth/1.5f) {
            Smoke.Play();
        }
        currentHealth = Mathf.Clamp(currentHealth - amount, -1, maxHealth);
    }
    void releaseUpgrade(){
        Vector2 firePoint = rigidbody2d.position + Vector2.down * 0.2f;
        GameObject projectileObject = Instantiate(upgradeobj, firePoint, Quaternion.identity);

        UpgradeCollective projectile = projectileObject.GetComponent<UpgradeCollective>();
        Vector2 target = gameCtr.player.position;
        Vector2 dir = target - firePoint;
        projectile.Launch(dir, 0.5f);
    }

    public void Fire() {
        Vector2 newpos1 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.right * 0.1f;
        Vector2 newpos2 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.left * 0.1f;

        GameObject projectileObject1 = Instantiate(bullet, newpos1, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet, newpos2, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        projectile1.Launch(Vector2.down, 3);
        projectile2.Launch(Vector2.down, 3);
    }

    public void FireAimed() 
    {
        Vector2 firePoint = rigidbody2d.position + Vector2.down * 0.2f;
        GameObject projectileObject = Instantiate(bullet, firePoint, Quaternion.identity);

        EnemyBullet projectile = projectileObject.GetComponent<EnemyBullet>();
        Vector2 target = gameCtr.player.position;
        Vector2 dir = target - firePoint;
        projectile.Launch(dir, 0.2f);
    }

    public void OnCollisionEnter2D(Collision2D other){
        DestroyRegion obj = other.collider.GetComponent<DestroyRegion>();
        if (obj != null) {
            Destroy(gameObject);
        }
    }
}
