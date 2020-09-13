using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinding {
    protected Grid<PathNode> grid;

    protected const int MOVE_STRAIGHT_COST = 10;
    protected const int MOVE_DIAGONAL_COST = 14;

    public Pathfinding() {
        grid = new Grid<PathNode>(GridOrigin.width, GridOrigin.height, GridOrigin.cellSize, GridOrigin.origin, (Grid<PathNode> grid, int x, int y, bool o) => new PathNode(grid, x, y, o));
    }

    public abstract List<PathNode> FindPath(int startX, int startY, int endX, int endY);

    protected List<PathNode> CalculatePath(PathNode endNode) {
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

    public Grid<PathNode> getGrid() {
        return this.grid;
    }

    protected PathNode getClosestNode(List<PathNode> queue) {
        PathNode closestNode = queue[0];
        foreach (PathNode aNode in queue) {
            if (aNode.fCost < closestNode.fCost) {
                closestNode = aNode;
            }
        }
        return closestNode;
    }

    protected List<PathNode> GetNeighboursList(PathNode node) {
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
    protected PathNode GetNode(int x, int y) {
        return grid.getGridObject(x, y);
    }
    protected int calcDistance(PathNode n1, PathNode n2) {
        int xDist = Mathf.Abs(n1.x - n2.x);
        int yDist = Mathf.Abs(n1.y - n2.y);
        int tileDist = Mathf.Abs(xDist - yDist);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDist, yDist) + MOVE_STRAIGHT_COST * tileDist;
    }
}
