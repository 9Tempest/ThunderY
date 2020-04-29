using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCollective : MonoBehaviour
{
    public AudioClip collectedClip;
    Rigidbody2D rigidbody2d;

    void Awake(){
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        MainPlane controller = other.GetComponent<MainPlane>();
        if (controller != null){
            if (controller.firelevel < 7) {
            controller.firelevel++;
            }
            controller.PlaySound(collectedClip);
            Destroy(gameObject);
        }
    }
    public void Launch(Vector2 direction, float speed){
        rigidbody2d.velocity = direction.normalized * speed;
    }
}
