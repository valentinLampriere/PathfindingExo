using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar {
    private Grid<PathNode> grid;

    private const int MOVE_STRAIGHT_LINE = 10;
    private const int MOVE_DIAGONAL_LINE = 14;

    private List<PathNode> openList;
    private List<PathNode> closeList;

    public Astar() {
        //grid = new Grid<PathNode>(GridOrigin.width, GridOrigin.height, GridOrigin.cellSize, GridOrigin.origin, (Grid<PathNode> g, int x, int y, bool o) => new PathNode(g, x, y, o));
        grid = new Grid<PathNode>(GridOrigin.width, GridOrigin.height, GridOrigin.cellSize, GridOrigin.origin, (Grid<PathNode> grid, int x, int y) => new PathNode(grid, x, y));
    }

    public Grid<PathNode> getGrid() {
        return this.grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        PathNode startNode = grid.getGridObject(startX, startY);
        PathNode endNode = grid.getGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closeList = new List<PathNode>();
        List<PathNode> pathToTarget = new List<PathNode>();

        for (int x = 0; x < grid.getWidth(); x++) {
            for (int y = 0; y < grid.getHeight(); y++) {
                PathNode node = grid.getGridObject(x, y);
                if (node != null) {
                    node.gCost = int.MaxValue;
                    node.calcFCost();
                    node.previousNode = null;
                }
            }
        }

        startNode.gCost = 0;
        startNode.hCost = calcDistance(startNode, endNode);
        startNode.calcFCost();

        while(openList.Count > 0) {
            PathNode closestNode = openList[0];
            foreach (PathNode aNode in openList) {
                aNode.gCost = aNode.previousNode.gCost + calcDistance(aNode, aNode.previousNode);
                aNode.hCost = calcDistance(aNode, endNode);
                aNode.calcFCost();
                if (aNode.fCost < closestNode.fCost) {
                    closestNode = aNode;
                }
            }
            if (closestNode == endNode) {
                break;
            }
            openList.Remove(closestNode);
            closeList.Add(closestNode);
            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    if (i == j && i == 0) continue;

                    PathNode neighbourNode = grid.getGridObject(closestNode.x + i, closestNode.y + j);
                    if (!openList.Contains(neighbourNode) && !closeList.Contains(neighbourNode) && !neighbourNode.isObstacle) {

                        if (neighbourNode.gCost + calcDistance(closestNode, neighbourNode) < neighbourNode.gCost) {
                            neighbourNode.previousNode = closestNode;
                            neighbourNode.gCost = neighbourNode.gCost + calcDistance(closestNode, neighbourNode);
                            neighbourNode.hCost = calcDistance(neighbourNode, endNode);
                            neighbourNode.calcFCost();
                        }
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        pathToTarget.Add(endNode);
        while (pathToTarget[pathToTarget.Count -1] != startNode) {
            pathToTarget.Add(pathToTarget[pathToTarget.Count - 1].previousNode);
        }
        return pathToTarget;
    }
    private int calcDistance(PathNode n1, PathNode n2) {
        int xDist = Mathf.Abs(n1.x - n2.x);
        int yDist = Mathf.Abs(n1.y - n2.y);
        int tileDist = Mathf.Abs(xDist - yDist);
        return MOVE_DIAGONAL_LINE * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_LINE + tileDist;
    }
}
