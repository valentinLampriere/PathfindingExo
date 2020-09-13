using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NPC : MonoBehaviour {
    
    public bool isDijkstra;

    Pathfinding pathfinding;

    GameObject player;
    Rigidbody2D rb;
    List<PathNode> path;

    IEnumerator Start() {

        if (isDijkstra)
            pathfinding = new Dijkstra();
        else
            pathfinding = new Astar();

        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        yield return Move();
    }
    IEnumerator Move() {
        while (true) {
            int npcX, npcY;
            int playerX, playerY;
            pathfinding.getGrid().getCoordAtPosition(player.transform.position, out playerX, out playerY);
            pathfinding.getGrid().getCoordAtPosition(gameObject.transform.position, out npcX, out npcY);
            path = pathfinding.FindPath(npcX, npcY, playerX, playerY);

            rb.MovePosition(pathfinding.getGrid().getPosAtCoord(path[1].x, path[1].y) + Vector3.one * GridOrigin.cellSize / 2);
            
            yield return new WaitForSeconds(1);
        }
    }
    private void OnMouseOver() {
        for (int i = 0; i < path.Count - 1; i++) {
            Debug.DrawLine(GridOrigin.origin + new Vector3(path[i].x, path[i].y) * GridOrigin.cellSize + Vector3.one * GridOrigin.cellSize / 2, GridOrigin.origin + new Vector3(path[i + 1].x, path[i + 1].y) * GridOrigin.cellSize + Vector3.one * GridOrigin.cellSize / 2, Color.blue, 1f);
        }
    }
}
