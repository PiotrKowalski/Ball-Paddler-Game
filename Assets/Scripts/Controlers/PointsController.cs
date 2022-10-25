using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public Point[] points;

    // Start is called before the first frame update
    void Start()
    {
        points = GetComponentsInChildren<Point>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
