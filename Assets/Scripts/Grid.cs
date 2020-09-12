using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T> {
    private int height;
    private int width;
    private float tileSize;
    private Vector3 origin;
    private T[,] gridArray;

    public Grid(int width, int height, float tileSize, Vector3 origin) {
        this.height = height;
        this.width = width;
        this.tileSize = tileSize;
        this.origin = origin;
        gridArray = new T[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                Debug.DrawLine(getPosAtCoord(x, y), getPosAtCoord(x, y + 1), Color.white, 100f);
                Debug.DrawLine(getPosAtCoord(x, y), getPosAtCoord(x + 1, y), Color.white, 100f);
                Debug.DrawLine(getPosAtCoord(x + 1, y), getPosAtCoord(x + 1, y + 1), Color.white, 100f);
                Debug.DrawLine(getPosAtCoord(x, y + 1), getPosAtCoord(x + 1, y + 1), Color.white, 100f);
            }
        }
        Debug.DrawLine(getPosAtCoord(0, height), getPosAtCoord(width, height), Color.white, 100f);
        Debug.DrawLine(getPosAtCoord(width, 0), getPosAtCoord(width, height), Color.white, 100f);
    }
    private Vector3 getPosAtCoord(int x, int y) {
        return origin + new Vector3(x, y) * tileSize;
    }

    private void getCoordAtPosition(Vector3 position, out int x, out int y) {
        x = Mathf.FloorToInt((position - origin).x / tileSize);
        y = Mathf.FloorToInt((position - origin).y / tileSize);
    }

    public void setValue(int x, int y, T value) {
        if (x > 0 && y > 0 && x < this.width && y < this.height)
            gridArray[x, y] = value;
    }

    public T getGridObject(int x, int y) {
        if (x > 0 && y > 0 && x < this.width && y < this.height)
            return gridArray[x, y];
        else
            return default(T);
    }

    public int getWidth() {
        return this.width;
    }

    public int getHeight() {
        return this.height;
    }
}
