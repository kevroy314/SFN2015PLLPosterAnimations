using UnityEngine;
using System.Collections.Generic;

public class RearrangeToCircle : MonoBehaviour {
    private Transform[] children;
    private Vector3[] points;
    public float radius = 30f;
    public float offsetX = 10f;
    public float offsetZ = 10f;

	// Use this for initialization
	void Start () {
        List<Transform> objs = new List<Transform>();
	    foreach(Transform child in transform)
            objs.Add(child);
        children = objs.ToArray();
        points = new Vector3[children.Length];
        for (int i = 0; i < points.Length; i++)
        {
            float theta = ((Mathf.PI * 2) / points.Length);
            float angle = (theta * i);
            points[i] = new Vector3(radius * Mathf.Cos(angle) + offsetX, 0, radius * Mathf.Sin(angle) + offsetZ);
        }

        for (int i = 0; i < children.Length; i++)
            iTween.MoveTo(children[i].gameObject, points[i], 3f);
	}
}
