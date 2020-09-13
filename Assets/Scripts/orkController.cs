using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orkController : MonoBehaviour {
    public GameObject player;
    Rigidbody2D rb;
    CircleCollider2D circleCollider;

    private Astar pathfindingAstar;

    IEnumerator Start() {
        pathfindingAstar = new Astar();
        rb = gameObject.GetComponent<Rigidbody2D>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        yield return Move();
    }

    IEnumerator Move() {
        while (!circleCollider.IsTouching(player.GetComponent<CircleCollider2D>())) {
            int npcX, npcY;
            int playerX, playerY;
            pathfindingAstar.getGrid().getCoordAtPosition(player.transform.position, out playerX, out playerY);
            pathfindingAstar.getGrid().getCoordAtPosition(gameObject.transform.position, out npcX, out npcY);
            List<PathNode> path = pathfindingAstar.FindPath(npcX, npcY, playerX, playerY);

            rb.MovePosition(pathfindingAstar.getGrid().getPosAtCoord(path[1].x, path[1].y) + Vector3.one * GridOrigin.cellSize / 2);

            if (PlayerController.debugEnabled)
                for (int i = 0; i < path.Count - 1; i++) {
                    Debug.DrawLine(GridOrigin.origin + new Vector3(path[i].x, path[i].y) * GridOrigin.cellSize + Vector3.one * GridOrigin.cellSize / 2, GridOrigin.origin + new Vector3(path[i + 1].x, path[i + 1].y) * GridOrigin.cellSize + Vector3.one * GridOrigin.cellSize /2, Color.green, 1f);
                }
            yield return new WaitForSeconds(1);
        }
    }
}
