using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTesting : MonoBehaviour {
    public int width = 10;
    public int height = 10;
    void Start() {
        Grid g = new Grid(width, height, 1f, gameObject.transform.position);
    }
}
