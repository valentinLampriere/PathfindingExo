using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour
{
    public GameObject player;
    float speed = 10f;
    Vector2 movement = new Vector2(1, 0);
    SpriteRenderer sprite;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    IEnumerator Start() {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        yield return Move();
    }

    IEnumerator Move() {
        while (!circleCollider.IsTouching(player.GetComponent<CircleCollider2D>())) {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            yield return new WaitForSeconds(1);
        }
    }
}
