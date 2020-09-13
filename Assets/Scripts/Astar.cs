using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : Pathfinding {

    private List<PathNode> openList;
    private List<PathNode> closeList;

    public Astar() : base() {}
    
    public override List<PathNode> FindPath(int startX, int startY, int endX, int endY) {

        int complexity = 0;
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

        while (openList.Count > 0) {
            PathNode currentNode = getClosestNode(openList);
            complexity++;
            if (currentNode == endNode) {
                Debug.Log("A* : " + complexity);
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);
            foreach (PathNode neighbour in GetNeighboursList(currentNode)) {
                if (closeList.Contains(neighbour))
                    continue;
                if (neighbour.isObstacle) {
                    closeList.Add(neighbour);
                    continue;
                }
                int gCostHeuristic = currentNode.gCost + calcDistance(currentNode, neighbour);
                if (gCostHeuristic < neighbour.gCost) {
                    neighbour.previousNode = currentNode;
                    neighbour.gCost = gCostHeuristic;
                    neighbour.hCost = calcDistance(neighbour, endNode);
                    neighbour.calcFCost();

                    if (!openList.Contains(neighbour)) {
                        openList.Add(neighbour);
                    }
                }
            }
        }
        Debug.Log("A* : " + complexity);
        return null;
    }
}
