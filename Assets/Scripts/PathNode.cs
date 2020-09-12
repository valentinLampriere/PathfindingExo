using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

    private Grid<PathNode> grid;
    int x, y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode previousNode;

    public PathNode(Grid<PathNode> grid,  int x, int y) {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public int calcFCost() {
        fCost = gCost + hCost;
        return fCost;
    }
}
