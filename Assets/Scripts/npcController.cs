using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour {
    public GameObject player;
    float speed = 10f;
    Vector2 movement = new Vector2(1, 0);
    SpriteRenderer sprite;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    private Astar pathfindingAstar;

    /*IEnumerator Start() {
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
    }*/
    void Start() {
        pathfindingAstar = new Astar();
    }
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            int npcX, npcY;
            int playerX, playerY;
            pathfindingAstar.getGrid().getCoordAtPosition(player.transform.position, out playerX, out playerY);
            pathfindingAstar.getGrid().getCoordAtPosition(gameObject.transform.position, out npcX, out npcY);
            List<PathNode> path = pathfindingAstar.FindPath(npcX, npcY, playerX, playerY);
            for (int i = 0; i < path.Count - 1; i++) {
                Debug.DrawLine(new Vector3(path[i].x, path[i].y) * GridOrigin.cellSize + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * GridOrigin.cellSize + Vector3.one * 5f, Color.green, 100f);
            }
        }
    }
}
