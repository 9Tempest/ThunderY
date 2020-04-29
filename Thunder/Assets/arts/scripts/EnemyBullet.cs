using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public int hurt;
    public float timer;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float speed){
        rigidbody2d.velocity = direction.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D other){
        MainPlane player = other.collider.GetComponent<MainPlane>();
        if (player != null) {
            player.ChangeHealth(hurt);
            Debug.Log("player health" + player.health);
        }
        Destroy(gameObject);
    }
}
