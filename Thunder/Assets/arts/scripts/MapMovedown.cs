using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovedown : MonoBehaviour
{
    public static MapMovedown instance { get; private set; }
    public float speed;
    Rigidbody2D rigidbody2d;
    // Start is called before the first frame update
    void Awake(){
        instance = this;
    }
    void Start()
    {
        speed = 0.15f;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2d.position;
        position.y = position.y - speed * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
