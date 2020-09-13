using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridOrigin : MonoBehaviour {
    
    public Tilemap wallTilemap;
    public static int width;
    public static int height;
    public static float cellSize = 1;
    public static Tilemap collisionTilemap;
    public static Vector3 origin;
    public static bool[,] collision;
    //public static Grid<PathNode> grid;

    void Start() {
        BoundsInt bounds = wallTilemap.cellBounds;
        TileBase[] allTiles = wallTilemap.GetTilesBlock(bounds);
        
        // Setting width and height depending on Tilemap size
        width = bounds.size.x;
        height = bounds.size.y;

        collision = new bool[width, height];

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                collision[x, y] = tile != null;
            }
        }

        collisionTilemap = wallTilemap;
        origin = gameObject.transform.position;

        //grid = new Grid<PathNode>(width, height, cellSize, origin, (Grid<PathNode> grid, int x, int y, bool o) => new PathNode(grid, x, y, o));
    }
}
