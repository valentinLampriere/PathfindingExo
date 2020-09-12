using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar {
    private Grid<PathNode> grid;

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private List<PathNode> openList;
    private List<PathNode> closeList;

    public Astar() {
        grid = new Grid<PathNode>(GridOrigin.width, GridOrigin.height, GridOrigin.cellSize, GridOrigin.origin, (Grid<PathNode> grid, int x, int y, bool o) => new PathNode(grid, x, y, o));
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

        while (openList.Count > 0) {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode) {
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
        return null;
    }

    private int calcDistance(PathNode n1, PathNode n2) {
        int xDist = Mathf.Abs(n1.x - n2.x);
        int yDist = Mathf.Abs(n1.y - n2.y);
        int tileDist = Mathf.Abs(xDist - yDist);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_COST * tileDist;
    }

    private PathNode GetLowestFCostNode(List<PathNode> nodes) {
        PathNode lowestFCostNode = nodes[0];
        for (int i = 1; i < nodes.Count; i++) {
            if(nodes[i].fCost < lowestFCostNode.fCost) {
                lowestFCostNode = nodes[i];
            }
        }
        return lowestFCostNode;
    }

    private List<PathNode> CalculatePath(PathNode endNode) {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while(currentNode.previousNode != null) {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }
        path.Reverse();
        return path;
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
}
