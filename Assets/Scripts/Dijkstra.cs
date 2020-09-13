using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra {
    private Grid<PathNode> grid;

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public Dijkstra() {
        grid = new Grid<PathNode>(GridOrigin.width, GridOrigin.height, GridOrigin.cellSize, GridOrigin.origin, (Grid<PathNode> grid, int x, int y, bool o) => new PathNode(grid, x, y, o));
    }

    /*public List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        List<PathNode> exploredNodes = new List<PathNode>();
        PathNode startNode = grid.getGridObject(startX, startY);

        for (int x = 0; x < grid.getWidth(); x++) {
            for (int y = 0; y < grid.getHeight(); y++) {
                PathNode node = grid.getGridObject(x, y);
                if (node != null) {
                    node.distanceFromSource = int.MaxValue;
                    node.previousNode = null;
                    exploredNodes.Add(node);
                }
            }
        }
        startNode.gCost = 0;

        while (queue.Count > 0) {

        }

    }*/
}
