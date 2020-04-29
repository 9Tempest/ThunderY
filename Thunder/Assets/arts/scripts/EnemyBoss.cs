using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private GameController gameCtr;
    // Start is called before the first frame update

    Rigidbody2D rigidbody2d;
    public float maxHealth;
    public float health { get { return currentHealth;}}
    float currentHealth;
    public ParticleSystem Smoke;
    public ParticleSystem Smoke2;
    public ParticleSystem Smoke3;
    public ParticleSystem Smoke4;
    public ParticleSystem Smoke5;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject explosion;
    public float attackgap1;
    public float attackgap2;
    public float attackgap3;
    public float attackgap4;
    public float breaktimer;
    float timer1 = 0;
    float timer2 = 0;
    float timer3 = 0;
    float timer4 = 0;
    public float speeddown;
    float boomtimer = 0.8f;
    bool isdestroy = false;
    public float downtimer;
    float hrztimer;
    public float idletimer;
    public float backtime;
    public float timebreak;
    public int score;
    public AudioClip[] Clip;
    bool isplayed = false;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        gameCtr = GameController.instance;
        breaktimer = timebreak;
        currentHealth = maxHealth;
        Smoke.Stop();
        Smoke2.Stop();
        Smoke3.Stop();
        Smoke4.Stop();
        Smoke5.Stop();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer1 = attackgap1;
        audioSource =GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;
        if (isdestroy) {
            
            boomtimer -= Time.deltaTime;
            if (boomtimer < 0) {
             Vector2 newpos1 = rigidbody2d.position + Vector2.down * 1f + Vector2.right * 1.3f;
            Vector2 newpos2 = rigidbody2d.position + Vector2.down * 0.5f + Vector2.left * 1.7f;
            GameObject projectileObject1 = Instantiate(explosion, newpos1, Quaternion.identity);
            Boom projectile1 = projectileObject1.GetComponent<Boom>();
            GameObject projectileObject2 = Instantiate(explosion, newpos2, Quaternion.identity);
            Boom projectile2 = projectileObject2.GetComponent<Boom>();
            Life.instance.SetScore(score);
            SummonEnemy.instance.iswin = true;
                Destroy(gameObject);
            }
            return;
        }
        
        if (currentHealth <= 0) {
            isdestroy = true;
            rigidbody2d.simulated = false;
            Smoke.Stop();
            
            return;
        }
        if (currentHealth <= 2000f) {
            if (backtime > 0) {
            position.y = position.y + Time.deltaTime * 0.2f;
            rigidbody2d.MovePosition(position);
            backtime -= Time.deltaTime;
            }
            if (timer4 < 0 && breaktimer > 0) {
                Firemode2();
                timer4 = attackgap4;
        }
            timer4 -= Time.deltaTime;
            breaktimer -= Time.deltaTime;
            if (breaktimer < -timebreak) {
                breaktimer = timebreak;
                PlaySound(1);
            }
            if (timer3 < 0) {
            Firemode1();
            timer3 = attackgap3;
        }
            timer3 -= Time.deltaTime;
            if (currentHealth <= 500) {
                if (timer2 < 0) {
            FireAimed();
            timer2 = attackgap2;
        }
            timer2 -= Time.deltaTime;
            }
            return;
        }
        
        if (downtimer > 0) { 
            if (timer1 < 0) {
            Fire_down();
            timer1 = attackgap1;
        }
        timer1 -= Time.deltaTime;
        position.y = position.y + Time.deltaTime * -speeddown;
        rigidbody2d.MovePosition(position);
        downtimer -= Time.deltaTime;
        return;
        }
        if (idletimer > 0) {
            idletimer -= Time.deltaTime;
            if (timer2 < 0) {
            FireAimed();
            timer2 = attackgap2;
        }
            timer2 -= Time.deltaTime;
            return;
            
        }
        if (!isplayed) {
            isplayed = true;
            PlaySound(0);
        }
        if (timer3 < 0) {
            Fire();
            timer3 = attackgap3;
        }
        timer3 -= Time.deltaTime;
    }

    public void ChangeHealth(float amount)
    {
        Debug.Log(currentHealth);
        if (currentHealth < maxHealth * 0.8) {
            Smoke.Play();
        }
        if (currentHealth < maxHealth * 0.6) {
            Smoke2.Play();
        }
        if (currentHealth < maxHealth * 0.5) {
            Smoke3.Play();
        }
        if (currentHealth < maxHealth * 0.3) {
            Smoke4.Play();
        }
        if (currentHealth < maxHealth * 0.1) {
            Smoke5.Play();
        }
        currentHealth = Mathf.Clamp(currentHealth - amount, -1, maxHealth);
    }

    public void Fire_down() {
        Vector2 newpos1 = rigidbody2d.position + Vector2.down * 1f + Vector2.right * 1.3f;
        Vector2 newpos2 = rigidbody2d.position + Vector2.down * 1f + Vector2.left * 1.3f;
        Vector2 newpos3 = rigidbody2d.position + Vector2.down * 0.5f + Vector2.right * 1.7f;
        Vector2 newpos4 = rigidbody2d.position + Vector2.down * 0.5f + Vector2.left * 1.7f;
        GameObject projectileObject1 = Instantiate(bullet3, newpos1, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet3, newpos2, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        GameObject projectileObject3 = Instantiate(bullet3, newpos3, Quaternion.identity);
        EnemyBullet projectile3 = projectileObject3.GetComponent<EnemyBullet>();
        GameObject projectileObject4 = Instantiate(bullet3, newpos4, Quaternion.identity);
        EnemyBullet projectile4 = projectileObject4.GetComponent<EnemyBullet>();
        projectile1.Launch(Vector2.down, 3);
        projectile2.Launch(Vector2.down, 3);
        projectile3.Launch(Vector2.down, 3);
        projectile4.Launch(Vector2.down, 3);
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
        GameObject projectileObject4 = Instantiate(bullet2, newpos1, Quaternion.identity);
        EnemyBullet projectile4 = projectileObject4.GetComponent<EnemyBullet>();
        GameObject projectileObject5 = Instantiate(bullet2, newpos2, Quaternion.identity);
        EnemyBullet projectile5 = projectileObject5.GetComponent<EnemyBullet>();
        projectile1.Launch(Vector2.down+Vector2.right*0.4f, 3);
        projectile2.Launch(Vector2.down+Vector2.left*0.4f, 3);
        projectile4.Launch(Vector2.down+Vector2.right*0.9f, 3);
        projectile5.Launch(Vector2.down+Vector2.left*0.9f, 3);
        projectile3.Launch(Vector2.down, 3);
        Vector2 firePoint1 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.right * 0.3f;
        Vector2 firePoint2 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.left * 0.3f;
        Vector2 target = gameCtr.player.position;
        Vector2 dir1 = target - firePoint1;
        Vector2 dir2 = target - firePoint2;
        GameObject projectileObject6 = Instantiate(bullet3, firePoint1, Quaternion.identity);
        EnemyBullet projectile6 = projectileObject6.GetComponent<EnemyBullet>();
        GameObject projectileObject7 = Instantiate(bullet3, firePoint2, Quaternion.identity);
        EnemyBullet projectile7 = projectileObject7.GetComponent<EnemyBullet>();
        projectile6.Launch(dir1, 2f);
        projectile7.Launch(dir2, 2f);
    }

    public void FireAimed() 
    {
        Vector2 firePoint1 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.right * 0.3f;
        Vector2 firePoint2 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.left * 0.3f;
        Vector2 firePoint3 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.right * 0.8f;
        Vector2 firePoint4 = rigidbody2d.position + Vector2.down * 0.2f + Vector2.left * 0.8f;
        GameObject projectileObject1 = Instantiate(bullet, firePoint1, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet, firePoint2, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        GameObject projectileObject3 = Instantiate(bullet, firePoint3, Quaternion.identity);
        EnemyBullet projectile3 = projectileObject3.GetComponent<EnemyBullet>();
        GameObject projectileObject4 = Instantiate(bullet, firePoint4, Quaternion.identity);
        EnemyBullet projectile4 = projectileObject4.GetComponent<EnemyBullet>();
        Vector2 target = gameCtr.player.position;
        Vector2 dir1 = target - firePoint1;
        Vector2 dir2 = target - firePoint2;
        Vector2 dir3 = target - firePoint3;
        Vector2 dir4 = target - firePoint4;
        projectile1.Launch(dir1, 2f);
        projectile2.Launch(dir2, 2f);
        projectile3.Launch(dir3, 2f);
        projectile4.Launch(dir4, 2f);
        Vector2 newpos1 = rigidbody2d.position + Vector2.down * 1f + Vector2.right * 1.3f;
        Vector2 newpos2 = rigidbody2d.position + Vector2.down * 1f + Vector2.left * 1.3f;
        Vector2 newpos3 = rigidbody2d.position + Vector2.down * 0.5f + Vector2.right * 1.7f;
        Vector2 newpos4 = rigidbody2d.position + Vector2.down * 0.5f + Vector2.left * 1.7f;
        GameObject projectileObject5 = Instantiate(bullet3, newpos1, Quaternion.identity);
        EnemyBullet projectile5 = projectileObject5.GetComponent<EnemyBullet>();
        GameObject projectileObject6 = Instantiate(bullet3, newpos2, Quaternion.identity);
        EnemyBullet projectile6 = projectileObject6.GetComponent<EnemyBullet>();
        GameObject projectileObject7 = Instantiate(bullet3, newpos3, Quaternion.identity);
        EnemyBullet projectile7 = projectileObject7.GetComponent<EnemyBullet>();
        GameObject projectileObject8 = Instantiate(bullet3, newpos4, Quaternion.identity);
        EnemyBullet projectile8 = projectileObject8.GetComponent<EnemyBullet>();
        projectile5.Launch(Vector2.down, 3);
        projectile6.Launch(Vector2.down, 3);
        projectile7.Launch(Vector2.down, 3);
        projectile8.Launch(Vector2.down, 3);
    }
    public void Firemode1(){
        Vector2 newpos1 = rigidbody2d.position + Vector2.down * 0.3f + Vector2.right * 0.1f;
        Vector2 newpos2 = rigidbody2d.position + Vector2.down * 0.3f + Vector2.left * 0.1f;
        Vector2 newpos3 = rigidbody2d.position + Vector2.down * 0.3f + Vector2.right * 0.2f;
        Vector2 newpos4 = rigidbody2d.position + Vector2.down * 0.3f + Vector2.left * 0.2f;
        GameObject projectileObject1 = Instantiate(bullet, newpos1, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet, newpos2, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        GameObject projectileObject4 = Instantiate(bullet, newpos1, Quaternion.identity);
        EnemyBullet projectile4 = projectileObject4.GetComponent<EnemyBullet>();
        GameObject projectileObject5 = Instantiate(bullet, newpos2, Quaternion.identity);
        EnemyBullet projectile5 = projectileObject5.GetComponent<EnemyBullet>();
        GameObject projectileObject6 = Instantiate(bullet, newpos3, Quaternion.identity);
        EnemyBullet projectile6 = projectileObject6.GetComponent<EnemyBullet>();
        GameObject projectileObject7 = Instantiate(bullet, newpos4, Quaternion.identity);
        EnemyBullet projectile7 = projectileObject7.GetComponent<EnemyBullet>();
        projectile1.Launch(Vector2.down+Vector2.right*0.2f, 4);
        projectile2.Launch(Vector2.down+Vector2.left*0.2f, 4);
        projectile4.Launch(Vector2.down+Vector2.right*0.6f, 4);
        projectile5.Launch(Vector2.down+Vector2.left*0.6f, 4);
        projectile6.Launch(Vector2.down+Vector2.right*0.9f, 4);
        projectile7.Launch(Vector2.down+Vector2.left*0.9f, 4);
    }
    public void Firemode2(){
        Vector2 newpos1 = rigidbody2d.position + Vector2.down * 0.3f +  Vector2.left * 0.1f;
        Vector2 newpos2 = rigidbody2d.position + Vector2.down * 0.3f +  Vector2.right * 0.1f;;
        GameObject projectileObject1 = Instantiate(bullet3, newpos1, Quaternion.identity);
        EnemyBullet projectile1 = projectileObject1.GetComponent<EnemyBullet>();
        GameObject projectileObject2 = Instantiate(bullet3, newpos2, Quaternion.identity);
        EnemyBullet projectile2 = projectileObject2.GetComponent<EnemyBullet>();
        projectile1.Launch(Vector2.down, 4);
        projectile2.Launch(Vector2.down, 4);
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
