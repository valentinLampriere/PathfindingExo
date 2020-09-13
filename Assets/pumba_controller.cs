using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pumba_controller : MonoBehaviour {
    public GameObject player;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    IEnumerator Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        yield return Move();
    }

    IEnumerator Move() {

        yield return new WaitForSeconds(1);
    }
}
