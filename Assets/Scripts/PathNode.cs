using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private Grid<PathNode> grid;
    public int x, y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool isObstacle;

    public PathNode previousNode;

    public PathNode(Grid<PathNode> grid,  int x, int y, bool isObstacle) {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.isObstacle = isObstacle;
    }

    public int calcFCost() {
        fCost = gCost + hCost;
        return fCost;
    }
}
