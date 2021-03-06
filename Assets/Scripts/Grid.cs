﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Grid<T> {
    private int height;
    private int width;
    private float tileSize;
    private Vector3 origin;
    private T[,] gridArray;

    public Grid(int width, int height, float tileSize, Vector3 origin, Func<Grid<T>, int, int, bool, T> createGridObject) {
        this.height = height;
        this.width = width;
        this.tileSize = tileSize;
        this.origin = origin;
        gridArray = new T[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {

                gridArray[x, y] = createGridObject(this, x, y, GridOrigin.collision[x, y]);
            }
        }
    }
    public Vector3 getPosAtCoord(int x, int y) {
        return origin + new Vector3(x, y) * tileSize;
    }

    public void getCoordAtPosition(Vector3 position, out int x, out int y) {
        x = Mathf.FloorToInt((position - origin).x / tileSize);
        y = Mathf.FloorToInt((position - origin).y / tileSize);
    }

    public void setValue(int x, int y, T value) {
        if (x > 0 && y > 0 && x < this.width && y < this.height)
            gridArray[x, y] = value;
    }

    public T getGridObject(int x, int y) {
        if (x >= 0 && y >= 0 && x < this.width && y < this.height) {
            return gridArray[x, y];
        } else
            return default(T);
    }

    public int getWidth() {
        return this.width;
    }

    public int getHeight() {
        return this.height;
    }

    public void displayGrid() {
        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                if (GridOrigin.collision[x, y]) {
                    Debug.DrawLine(getPosAtCoord(x, y), getPosAtCoord(x + 1, y + 1), Color.red, 0.05f);
                    Debug.DrawLine(getPosAtCoord(x + 1, y), getPosAtCoord(x, y + 1), Color.red, 0.05f);
                }
                Debug.DrawLine(getPosAtCoord(x, y), getPosAtCoord(x, y + 1), Color.black, 0.05f);
                Debug.DrawLine(getPosAtCoord(x, y), getPosAtCoord(x + 1, y), Color.black, 0.05f);
                Debug.DrawLine(getPosAtCoord(x + 1, y), getPosAtCoord(x + 1, y + 1), Color.black, 0.05f);
                Debug.DrawLine(getPosAtCoord(x, y + 1), getPosAtCoord(x + 1, y + 1), Color.black, 0.05f);
            }
        }
        Debug.DrawLine(getPosAtCoord(0, height), getPosAtCoord(width, height), Color.black, 0.05f);
        Debug.DrawLine(getPosAtCoord(width, 0), getPosAtCoord(width, height), Color.black, 0.05f);
    }
}
