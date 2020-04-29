using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPlane : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody2D rigidbody2d;
    public int maxHealth = 10;
    public float speed = 5.0f;
    public int health { get { return currentHealth;}}
    int currentHealth;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject boom;
    public float AttackSpeed;
    float AttackTimer = 0.0f;
    bool canAttack = true;
    public int firelevel;
    Animator animator;
    bool isdestroy = false;
    public float boomtimer;
    public AudioClip clip;
    public GameObject ingameMenu;
    public int numboom;
    void Start()
    {
        currentHealth = maxHealth;
        rigidbody2d = GetComponent<Rigidbody2D>(); 
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isdestroy) {  
            boomtimer -= Time.deltaTime;
            if (boomtimer < 0) {
                ingameMenu.SetActive(true);
                if (Input.GetKey(KeyCode.R)){
                    SceneManager.LoadScene("Game");
                }
            }
            return;
        }
        
        if (currentHealth <= 0) {
            isdestroy = true;
            rigidbody2d.simulated = false;
            animator.SetBool("isdead", true);
            return;
        }
        if (!canAttack){
            if (AttackTimer < 0) {
                canAttack = true;
            }   else {
                AttackTimer -= Time.deltaTime;
            }
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector2 move = new Vector2(horizontal, vertical);
        Vector2 position = rigidbody2d.position;

        position = position + move * speed *Time.deltaTime;
        
        rigidbody2d.MovePosition(position);

        if (isInvincible){
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0) {
                isInvincible = false;
                animator.SetBool("ishurt", false);
            }
        }
        if(Input.GetKey(KeyCode.Z) && canAttack)
        {
            Launch();
            canAttack = false;
            AttackTimer = AttackSpeed;
            audioSource.PlayOneShot(clip);
        }
        if (Input.GetKeyDown(KeyCode.X) && numboom > 0){
            numboom--;
            Life.instance.SetBoom(numboom);
            Boomlaunch();
        }
        if (Input.GetKey(KeyCode.Q)){
             Application.Quit();
        }
    }

    void Launch(){
        if (firelevel == 1) {
            GameObject projectileObject = Instantiate(bullet, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);
            bullet1 projectile = projectileObject.GetComponent<bullet1>();
            projectile.Launch(Vector2.up, 200);
        }
        
        if (firelevel == 2) {
            Vector2 newpos1 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.right * 0.1f;
            Vector2 newpos2 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.left * 0.1f;
            GameObject projectileObject2 = Instantiate(bullet, newpos1, Quaternion.identity);
            bullet1 projectile2 = projectileObject2.GetComponent<bullet1>();
            projectile2.Launch(Vector2.up, 200);
            GameObject projectileObject3 = Instantiate(bullet, newpos2, Quaternion.identity);
            bullet1 projectile3 = projectileObject3.GetComponent<bullet1>();
            projectile3.Launch(Vector2.up, 200);
        }
        if (firelevel == 3) {
            GameObject projectileObject = Instantiate(bullet, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);
            bullet1 projectile = projectileObject.GetComponent<bullet1>();
            projectile.Launch(Vector2.up, 200);
            Vector2 newpos1 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.right * 0.1f;
            Vector2 newpos2 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.left * 0.1f;
            GameObject projectileObject2 = Instantiate(bullet, newpos1, Quaternion.identity);
            bullet1 projectile2 = projectileObject2.GetComponent<bullet1>();
            projectile2.Launch(Vector2.up, 200);
            GameObject projectileObject3 = Instantiate(bullet, newpos2, Quaternion.identity);
            bullet1 projectile3 = projectileObject3.GetComponent<bullet1>();
            projectile3.Launch(Vector2.up, 200);
        }
        if (firelevel == 4) {
            GameObject projectileObject = Instantiate(bullet2, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);
            bullet1 projectile = projectileObject.GetComponent<bullet1>();
            projectile.Launch(Vector2.up, 200);
            Vector2 newpos1 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.right * 0.1f;
            Vector2 newpos2 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.left * 0.1f;
            GameObject projectileObject2 = Instantiate(bullet, newpos1, Quaternion.identity);
            bullet1 projectile2 = projectileObject2.GetComponent<bullet1>();
            projectile2.Launch(Vector2.up, 200);
            GameObject projectileObject3 = Instantiate(bullet, newpos2, Quaternion.identity);
            bullet1 projectile3 = projectileObject3.GetComponent<bullet1>();
            projectile3.Launch(Vector2.up, 200);
        }
        if (firelevel == 5) {
            GameObject projectileObject = Instantiate(bullet2, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);
            bullet1 projectile = projectileObject.GetComponent<bullet1>();
            projectile.Launch(Vector2.up, 200);
            Vector2 newpos1 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.right * 0.1f;
            Vector2 newpos2 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.left * 0.1f;
            GameObject projectileObject2 = Instantiate(bullet2, newpos1, Quaternion.identity);
            bullet1 projectile2 = projectileObject2.GetComponent<bullet1>();
            projectile2.Launch(Vector2.up, 200);
            GameObject projectileObject3 = Instantiate(bullet2, newpos2, Quaternion.identity);
            bullet1 projectile3 = projectileObject3.GetComponent<bullet1>();
            projectile3.Launch(Vector2.up, 200);
        }
        if (firelevel == 6) {
            GameObject projectileObject = Instantiate(bullet2, rigidbody2d.position + Vector2.up * 0.2f, Quaternion.identity);
            bullet1 projectile = projectileObject.GetComponent<bullet1>();
            projectile.Launch(Vector2.up, 200);
            Vector2 newpos1 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.right * 0.1f;
            Vector2 newpos2 = rigidbody2d.position + Vector2.up * 0.2f + Vector2.left * 0.1f;
            GameObject projectileObject2 = Instantiate(bullet2, newpos1, Quaternion.identity);
            bullet1 projectile2 = projectileObject2.GetComponent<bullet1>();
            projectile2.Launch(Vector2.up, 200);
            GameObject projectileObject3 = Instantiate(bullet2, newpos2, Quaternion.identity);
            bullet1 projectile3 = projectileObject3.GetComponent<bullet1>();
            projectile3.Launch(Vector2.up, 200);
            GameObject projectileObject4 = Instantiate(bullet2, newpos1+ Vector2.right*0.1f, Quaternion.identity);
            bullet1 projectile4 = projectileObject4.GetComponent<bullet1>();
            projectile4.Launch(Vector2.up+Vector2.right*0.1f, 200);
            GameObject projectileObject5 = Instantiate(bullet2, newpos2+Vector2.left*0.1f, Quaternion.identity);
            bullet1 projectile5 = projectileObject5.GetComponent<bullet1>();
            projectile5.Launch(Vector2.up+Vector2.left*0.1f, 200);
        }

    }

    void Boomlaunch(){
        isInvincible = true;
        invincibleTimer = timeInvincible;
        GameObject boomobj = Instantiate(boom, rigidbody2d.position + Vector2.up * 2.5f, Quaternion.identity);
        Boom boom1 = boomobj.GetComponent<Boom>();
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0) {
            if (isInvincible) {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            if (currentHealth > 1)
            animator.SetBool("ishurt", true);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Life.instance.SetValue(currentHealth);
    }
     public void PlaySound(AudioClip clip)
{
    audioSource.PlayOneShot(clip);
}

}
