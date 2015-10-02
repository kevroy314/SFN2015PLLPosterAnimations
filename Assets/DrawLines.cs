using UnityEngine;
using System.Collections;

public class DrawLines : MonoBehaviour {
    public Material lineMaterial;
    public Color lineColor;
    public float lineThickness;
    public float radius = 40f;
    public int items = 16;
    private Vector3[] points;
    public float offsetX = 10f;
    public float offsetZ = 10f;
    // Use this for initialization
    void Start () {
        points = new Vector3[items];
        for (int i = 0; i < points.Length; i++)
        {
            float theta = ((Mathf.PI * 2) / points.Length);
            float angle = (theta * i);
            points[i] = new Vector3(radius * Mathf.Cos(angle) + offsetX, 0, radius * Mathf.Sin(angle) + offsetZ);
        }

        GameObject lines = new GameObject();
        lines.name = "Lines";
        LineRenderer line = lines.AddComponent<LineRenderer>();
        line.SetVertexCount(points.Length * points.Length * 2);
        line.useLightProbes = false;
        line.material = lineMaterial;
        line.material.color = new Color(line.material.color.r, line.material.color.g, line.material.color.b, 0f);
        line.SetWidth(lineThickness, lineThickness);
        line.SetColors(lineColor, lineColor);
        int lineIndex = 0;
        for (int i = 0; i < points.Length; i++)
        {
            for (int j = 0; j < points.Length; j++)
            {
                line.SetPosition(lineIndex, points[i]);
                line.SetPosition(lineIndex + 1, points[j]);
                lineIndex += 2;
            }
        }
    }
}
