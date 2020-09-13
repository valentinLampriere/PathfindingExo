using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPC : MonoBehaviour {
    
    public bool isDijkstra;
    public List<Sprite> sprites;

    Pathfinding pathfinding;

    GameObject player;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    List<PathNode> path;

    Text debugText; 

    IEnumerator Start() {
        if (isDijkstra) {
            pathfinding = new Dijkstra();
        } else {
            pathfinding = new Astar();
        }
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count - 1)];

        yield return Move();
    }
    IEnumerator Move() {
        while (true) {
            int npcX, npcY;
            int playerX, playerY;
            pathfinding.getGrid().getCoordAtPosition(player.transform.position, out playerX, out playerY);
            pathfinding.getGrid().getCoordAtPosition(gameObject.transform.position, out npcX, out npcY);
            path = pathfinding.FindPath(npcX, npcY, playerX, playerY);

            if (path != null)
                rb.MovePosition(pathfinding.getGrid().getPosAtCoord(path[1].x, path[1].y) + Vector3.one * GridOrigin.cellSize / 2);
            
            yield return new WaitForSeconds(1);
        }
    }
    void Update() {
        if (Input.GetKey(KeyCode.Space) && path != null) {
            Color c = Color.blue;
            if (isDijkstra)
                c = Color.green;
            for (int i = 1; i < path.Count - 1; i++) {
                DrawLine(GridOrigin.origin + new Vector3(path[i].x, path[i].y) * GridOrigin.cellSize + Vector3.one * GridOrigin.cellSize / 2, GridOrigin.origin + new Vector3(path[i + 1].x, path[i + 1].y) * GridOrigin.cellSize + Vector3.one * GridOrigin.cellSize / 2, c, 0.05f);
            }
        }
    }

    /* Taken from https://answers.unity.com/questions/8338/how-to-draw-a-line-using-script.html */
    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f) {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.material.color = color;
        lr.sortingOrder = 10;
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }


}
