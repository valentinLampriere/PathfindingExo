using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float speed = 3f;
    Vector2 movement;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    public static bool debugEnabled = false;


    void Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x < 0)
            sprite.flipX = true;
        else if (movement.x > 0)
            sprite.flipX = false;

        debugEnabled = Input.GetKey(KeyCode.Space;
    }
    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
