using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet1 : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Animator animator;
    public float hurt = 1.0f;
    public float timer = 1.0f;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force){
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other){
        Enemy enemy = other.collider.GetComponent<Enemy>();
        EnemyShipType1 ship2 = other.collider.GetComponent<EnemyShipType1>();
        EnemyPlane_01 plane1 = other.collider.GetComponent<EnemyPlane_01>();
        EnemyPlane_02 plane2 = other.collider.GetComponent<EnemyPlane_02>();
        EnemyBoss plane3 = other.collider.GetComponent<EnemyBoss>();
        if (enemy != null) {
            enemy.ChangeHealth(hurt);
            Debug.Log("enemy health" + enemy.health);    
        }
        if (ship2 != null) {
            ship2.ChangeHealth(hurt);
            Debug.Log("ship2 health" + ship2.health);    
        }
        if (plane1 != null) {
            plane1.ChangeHealth(hurt);
        }
        if (plane2 != null) {
            plane2.ChangeHealth(hurt);
        }
        if (plane3 != null) {
            plane3.ChangeHealth(hurt);
        }
        animator.SetBool("ishit", true);
        rigidbody2d.simulated = false;
        timer = 0.5f;
        
    }
}
