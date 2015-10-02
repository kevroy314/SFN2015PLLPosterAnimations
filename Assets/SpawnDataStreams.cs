using UnityEngine;
using System.Collections;

public class SpawnDataStreams : MonoBehaviour {
    public float radius = 40f;
    public int items = 16;
    private Vector3[] points;
    public float offsetX = 10f;
    public float offsetZ = 10f;
    public GameObject prototyeObject;
    // Use this for initialization
    void Start () {
        points = new Vector3[items];
        for (int i = 0; i < points.Length; i++)
        {
            float theta = ((Mathf.PI * 2) / points.Length);
            float angle = (theta * i);
            points[i] = new Vector3(radius * Mathf.Cos(angle) + offsetX, 0, radius * Mathf.Sin(angle) + offsetZ);
        }

        for (int i = 0; i < points.Length; i++) {
            GameObject stream = new GameObject();
            SineWaveMaker wave = stream.AddComponent<SineWaveMaker>();
            stream.transform.position = points[i];
            stream.transform.rotation = Quaternion.Euler(0, 0, 90);
            wave.deltaT = 0.5f;
            wave.frequency = 5;
            wave.phase = 0;
            wave.numberOfObjects = 60;
            wave.objectOffset = 0.5f;
            wave.prototypeObject = prototyeObject;
            wave.amplitude = 0.25f;
            wave.loop = true;
            wave.maxLength = 25f;
            wave.globalStopTime = 10000;
            wave.noiseFactor = 0.5f;
            wave.startTimeDelay = 0f;
        }

	}
}
