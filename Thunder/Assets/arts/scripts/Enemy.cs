using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private GameController gameCtr;

    AudioSource audioSource;
    Animator animator;
    Rigidbody2D rigidbody2d;
    public float maxHealth;
    public float health { get { return currentHealth;}}
    float currentHealth;
    public ParticleSystem Smoke;
    public GameObject bullet;
    public float attackgap;
    float timer = 0;
    public float speed;
    float boomtimer = 0.5f;
    bool isdestroy = false;
    public int score;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isdestroy) {
            boomtimer -= Time.deltaTime;
            if (boomtimer < 0) {
                Life.instance.SetScore(score);
                
                Destroy(gameObject);
            }
            return;
        }
        if (timer < 0) {
            FireAimed();
            timer = attackgap;
        }
        if (currentHealth <= 0) {
            isdestroy = true;
            animator.SetBool("isdestroyed", true);
            rigidbody2d.simulated = false;
            Smoke.Stop();
            PlaySound(0);
            return;
        }
        timer -= Time.deltaTime;
        Vector2 position = rigidbody2d.position;
        position.y = position.y + Time.deltaTime * -speed;
        rigidbody2d.MovePosition(position);
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
        GameObject projectileObject = Instantiate(bullet, rigidbody2d.position + Vector2.down * 0.2f, Quaternion.identity);

        EnemyBullet projectile = projectileObject.GetComponent<EnemyBullet>();
        projectile.Launch(Vector2.down, 3);
    }

    public void FireAimed() 
    {
        Vector2 firePoint = rigidbody2d.position + Vector2.down * 0.2f;
        GameObject projectileObject1 = Instantiate(bullet, firePoint, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet, firePoint, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        GameObject projectileObject3 = Instantiate(bullet, firePoint, Quaternion.identity);
        EnemyBullet projectile3 = projectileObject3.GetComponent<EnemyBullet>();
        Vector2 target = gameCtr.player.position;
        Vector2 target2 = target + 3.0f * Vector2.right;
        Vector2 target3 = target + 3.0f * Vector2.left;
        Vector2 dir = target - firePoint;
        Vector2 dir2 = target2 - firePoint;
        Vector2 dir3 = target3 - firePoint;
        projectile1.Launch(dir, 2);
        projectile2.Launch(dir2, 2);
        projectile3.Launch(dir3, 2);
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
