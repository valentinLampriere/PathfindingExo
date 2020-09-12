using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOrigin : MonoBehaviour {
    public static int width = 25;
    public static int height = 15;
    public static float cellSize = 1;
    public static Vector3 origin;

    void Start() {
        origin = gameObject.transform.position;
    }
}
