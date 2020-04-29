using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane_02 : MonoBehaviour
{
    AudioSource audioSource;
    private GameController gameCtr;
    // Start is called before the first frame update
    Animator animator;
    Rigidbody2D rigidbody2d;
    public float maxHealth;
    public float health { get { return currentHealth;}}
    float currentHealth;
    public ParticleSystem Smoke;
    public GameObject bullet;
    public GameObject bullet2;
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
    public int score;
    public GameObject explosion;
    public AudioClip[] Clip;
    // Start is called before the first frame update
    void Start()
    {
        gameCtr = GameController.instance;
        audioSource = GetComponent<AudioSource>();
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
                Vector2 newpos1 = rigidbody2d.position;
                GameObject projectileObject1 = Instantiate(explosion, newpos1, Quaternion.identity);
                Boom projectile1 = projectileObject1.GetComponent<Boom>();
                Life.instance.SetScore(score);
                
                Destroy(gameObject);
            }
            return;
        }
        
        if (currentHealth <= 0) {
            isdestroy = true;
            animator.SetBool("isdestroyed", true);
            rigidbody2d.simulated = false;
            Smoke.Stop();
            PlaySound(0);
            return;
        }
        
        Vector2 position = rigidbody2d.position;
        if (downtimer > 0) { 
        position.y = position.y + Time.deltaTime * -speeddown;
        rigidbody2d.MovePosition(position);
        downtimer -= Time.deltaTime;
        return;
        }
        if (idletimer > 0) {
            idletimer -= Time.deltaTime;
            if (timer < 0) {
            FireAimed();
            timer = attackgap;
        }
            timer -= Time.deltaTime;
            return;
        }
        position.x = position.x +Time.deltaTime * speedhrz;
        rigidbody2d.MovePosition(position);
        if (timer < 0) {
            Fire();
            timer = attackgap;
        }
        timer -= Time.deltaTime;
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


    public void Fire() {
        Vector2 newpos1 = rigidbody2d.position + Vector2.down * 0.3f + Vector2.right * 0.1f;
        Vector2 newpos2 = rigidbody2d.position + Vector2.down * 0.3f + Vector2.left * 0.1f;
        Vector2 newpos3 = rigidbody2d.position + Vector2.down * 0.3f;
        GameObject projectileObject1 = Instantiate(bullet2, newpos1, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet2, newpos2, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        GameObject projectileObject3 = Instantiate(bullet2, newpos3, Quaternion.identity);
        EnemyBullet projectile3 = projectileObject3.GetComponent<EnemyBullet>();
        projectile1.Launch(Vector2.down+Vector2.right*0.3f, 3);
        projectile2.Launch(Vector2.down+Vector2.left*0.3f, 3);
        projectile3.Launch(Vector2.down, 3);
    }

    public void FireAimed() 
    {
        Vector2 firePoint1 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.right * 0.3f;
        Vector2 firePoint2 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.left * 0.3f;
        GameObject projectileObject1 = Instantiate(bullet, firePoint1, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet, firePoint2, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        Vector2 target = gameCtr.player.position;
        Vector2 dir1 = target - firePoint1;
        Vector2 dir2 = target - firePoint2;
        projectile1.Launch(dir1, 2f);
        projectile2.Launch(dir2, 2f);
    }

    public void OnCollisionEnter2D(Collision2D other){
        DestroyRegion obj = other.collider.GetComponent<DestroyRegion>();
        if (obj != null) {
            Destroy(gameObject);
        }
    }
    public void PlaySound(int i)
{
    audioSource.clip = Clip[i];
    audioSource.Play();
}
}
