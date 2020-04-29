using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float hurt;
    public float timer;
    void Update(){
        timer -= Time.deltaTime;
        if (timer < 0) {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        Enemy enemy = other.GetComponent<Enemy>();
        EnemyShipType1 ship2 = other.GetComponent<EnemyShipType1>();
        EnemyPlane_01 plane1 = other.GetComponent<EnemyPlane_01>();
        EnemyBoss boss = other.GetComponent<EnemyBoss>();
        EnemyPlane_02 plane2 = other.GetComponent<EnemyPlane_02>();
        if (enemy != null) {
            enemy.ChangeHealth(hurt);   
        }
        if (ship2 != null) {
            ship2.ChangeHealth(hurt); 
        }
        if (plane1 != null) {
            plane1.ChangeHealth(hurt);
        }
        if (plane2 != null) {
            plane2.ChangeHealth(hurt);
        }
        if (boss != null) {
            boss.ChangeHealth(hurt);
        }
        
    }
}
