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

    public Grid<PathNode> getGrid() {
        return this.grid;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
        List<PathNode> queue = new List<PathNode>();
        PathNode startNode = grid.getGridObject(startX, startY);
        PathNode endNode = grid.getGridObject(endX, endY);

        for (int x = 0; x < grid.getWidth(); x++) {
            for (int y = 0; y < grid.getHeight(); y++) {
                PathNode node = grid.getGridObject(x, y);
                if (node != null) {
                    // gCost represents the distance from the node to the start node
                    node.gCost = int.MaxValue;
                    node.previousNode = null;
                    queue.Add(node);
                }
            }
        }
        startNode.gCost = 0;

        while (queue.Count > 0) {
            PathNode currentNode = getClosestNode(queue);
            queue.Remove(currentNode);
            foreach (PathNode neighbour in GetNeighboursList(currentNode)) {
                int newDist = currentNode.gCost + calcDistance(currentNode, neighbour);
                if (newDist < neighbour.gCost) {
                    neighbour.gCost = newDist;
                    neighbour.previousNode = currentNode;
                }
            }
        }
        return CalculatePath(endNode);
    }
    private List<PathNode> CalculatePath(PathNode endNode) {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.previousNode != null) {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }
        path.Reverse();
        return path;
    }

    private PathNode getClosestNode(List<PathNode> queue) {
        PathNode closestNode = queue[0];
        foreach (PathNode aNode in queue) {
            if (aNode.gCost < closestNode.gCost) {
                closestNode = aNode;
            }
        }
        return closestNode;
    }
    private List<PathNode> GetNeighboursList(PathNode node) {
        List<PathNode> neighbours = new List<PathNode>();
        if (node.x - 1 >= 0) {
            neighbours.Add(GetNode(node.x - 1, node.y));
            if (node.y - 1 >= 0)
                neighbours.Add(GetNode(node.x - 1, node.y - 1));
            if (node.y + 1 < grid.getHeight())
                neighbours.Add(GetNode(node.x - 1, node.y + 1));
        }
        if (node.x + 1 < grid.getWidth()) {
            neighbours.Add(GetNode(node.x + 1, node.y));
            if (node.y - 1 >= 0)
                neighbours.Add(GetNode(node.x + 1, node.y - 1));
            if (node.y + 1 < grid.getHeight())
                neighbours.Add(GetNode(node.x + 1, node.y + 1));
        }
        if (node.y - 1 >= 0)
            neighbours.Add(GetNode(node.x, node.y - 1));
        if (node.y + 1 < grid.getHeight())
            neighbours.Add(GetNode(node.x, node.y + 1));
        return neighbours;
    }
    private PathNode GetNode(int x, int y) {
        return grid.getGridObject(x, y);
    }
    private int calcDistance(PathNode n1, PathNode n2) {
        int xDist = Mathf.Abs(n1.x - n2.x);
        int yDist = Mathf.Abs(n1.y - n2.y);
        int tileDist = Mathf.Abs(xDist - yDist);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_COST * tileDist;
    }
}
