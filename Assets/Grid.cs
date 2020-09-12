using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {
    private int height;
    private int width;
    private int[,] gridArray;

    Grid(int w, int h) {
        height = h;
        width = w;
        gridArray = new int[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {

            }
        }
    }
}
