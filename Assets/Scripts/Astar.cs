using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar {
    private Grid<PathNode> grid;

    private List<PathNode> openList;
    private List<PathNode> closeList;

    public Astar(int width, int height) {
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero);
    }

    private List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        PathNode startNode = grid.getGridObject(startX, startY);

        openList = new List<PathNode>(startNode);
        closeList = new List<PathNode>();
    }
}
