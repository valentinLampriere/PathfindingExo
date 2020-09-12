using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar {
    private Grid<PathNode> grid;

    private const int MOVE_STRAIGHT_LINE = 10;
    private const int MOVE_DIAGONAL_LINE = 14;

    private List<PathNode> openList;
    private List<PathNode> closeList;

    public Astar(int width, int height) {
        grid = new Grid<PathNode>(width, height, 10f, Vector3.zero);
    }

    private List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        PathNode startNode = grid.getGridObject(startX, startY);
        PathNode endNode = grid.getGridObject(endX, endY);

        openList = new List<PathNode> { startNode };
        closeList = new List<PathNode>();
        List<PathNode> pathToTarget = new List<PathNode>();

        for (int x = 0; x < grid.getWidth(); x++) {
            for (int y = 0; y < grid.getHeight(); y++) {
                PathNode node = grid.getGridObject(x, y);
                node.gCost = int.MaxValue;
                node.calcFCost();
                node.previousNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = calcHeuristicDistance(startNode, endNode);
        startNode.calcFCost();

        while(openList.Count > 0) {
            PathNode closestNode = openList[0];
            foreach (PathNode aNode in openList) {
                if (endNode == aNode) {
                    break;
                }
                aNode.gCost = aNode.previousNode.gCost + calcHeuristicDistance(aNode, aNode.previousNode);
                aNode.hCost = calcHeuristicDistance(aNode, endNode);
                aNode.calcFCost();
                if (aNode.fCost < closestNode.fCost) {
                    closestNode = aNode;
                }
            }
            openList.Remove(closestNode);
            closeList.Add(closestNode);
            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    PathNode neighbourNode = grid.getGridObject(closestNode.x + i, closestNode.y + j);
                    if (!openList.Contains(neighbourNode) && !closeList.Contains(neighbourNode) && !neighbourNode.isObstacle) {
                        openList.Add(neighbourNode);

                        neighbourNode.gCost = neighbourNode.previousNode.gCost + calcHeuristicDistance(neighbourNode, neighbourNode.previousNode);
                        neighbourNode.hCost = calcHeuristicDistance(neighbourNode, endNode);
                        neighbourNode.calcFCost();
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
    private int calcHeuristicDistance(PathNode n1, PathNode n2) {
        int xDist = Mathf.Abs(n1.x - n2.x);
        int yDist = Mathf.Abs(n1.y - n2.y);
        int tileDist = Mathf.Abs(xDist - yDist);
        return MOVE_DIAGONAL_LINE * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_LINE + tileDist;
    }
}
