using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchLine : MonoBehaviour
{
    public LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Defines points and renders line
    public void RenderLine(Vector3 startPos, Vector3 endPos)
    {
        lr.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPos;
        points[1] = endPos;

        lr.SetPositions(points);
    }
    
    // Stops rendering line
    public void EndLine()
    {
        lr.positionCount = 0;
    }
    
}
