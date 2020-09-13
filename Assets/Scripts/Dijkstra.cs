using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : Pathfinding {

    public Dijkstra() : base() {}

    public override List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        int complexity = 0;
        List <PathNode> queue = new List<PathNode>();
        PathNode startNode = grid.getGridObject(startX, startY);
        PathNode endNode = grid.getGridObject(endX, endY);

        for (int x = 0; x < grid.getWidth(); x++) {
            for (int y = 0; y < grid.getHeight(); y++) {
                PathNode node = grid.getGridObject(x, y);
                if (node != null) {
                    // gCost represents the distance from the node to the start node
                    node.fCost = int.MaxValue;
                    node.previousNode = null;
                    queue.Add(node);
                }
            }
        }
        startNode.fCost = 0;

        while (queue.Count > 0) {
            PathNode currentNode = getClosestNode(queue);
            queue.Remove(currentNode);
            foreach (PathNode neighbour in GetNeighboursList(currentNode)) {
                int newDist = currentNode.fCost + calcDistance(currentNode, neighbour);
                if (neighbour.isObstacle)
                    newDist = int.MaxValue;
                if (newDist < neighbour.fCost) {
                    neighbour.fCost = newDist;
                    neighbour.previousNode = currentNode;
                }
                complexity++;
                if (neighbour == endNode) {
                    Debug.Log("DIJKSTRA : " + complexity);
                    return CalculatePath(endNode);
                }
            }
        }
        Debug.Log("DIJKSTRA : " + complexity);
        return null;
    }
}
