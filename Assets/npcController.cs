using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{

    float speed = 3f;
    Vector2 movement = new Vector2(1, 0);
    SpriteRenderer sprite;
    Rigidbody2D rb;

    IEnumerator Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        yield return Move();
    }

    IEnumerator Move() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        yield return new WaitForSeconds(1);
    }
}
