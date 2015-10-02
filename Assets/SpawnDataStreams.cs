using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class SpawnDataStreams : MonoBehaviour {
    public float radius = 40f;
    public int items = 16;
    private Vector3[] points;
    public float offsetX = 10f;
    public float offsetZ = 10f;
    public Material lineMaterial;
    public Color lineColor;
    public float lineThickness;
    public GameObject prototyeObject;
    public LineRenderer[] lines;
    public int numPoints = 200;
    private int currentIndex;
    private float t;
    public float deltaT;
    private float[][] X;
    private float[][] Y;
    // Use this for initialization
    void Start () {
        points = new Vector3[items];
        for (int i = 0; i < points.Length; i++)
        {
            float theta = ((Mathf.PI * 2) / points.Length);
            float angle = (theta * i);
            points[i] = new Vector3(radius * Mathf.Cos(angle) + offsetX, 0, radius * Mathf.Sin(angle) + offsetZ);
        }
        lines = new LineRenderer[items];
        
        for (int i = 0; i < points.Length; i++) {
            GameObject stream = new GameObject();
            stream.name = "Stream";
            lines[i] = stream.AddComponent<LineRenderer>();
            lines[i].SetVertexCount(1);
            lines[i].useLightProbes = false;
            Shader s = Shader.Find("Unlit/Color");
            Material m = new Material(s);
            int r, g, b;
            float iF = (float)i;
            float l = (float)points.Length;
            HsvToRgb(Mathf.Lerp(0f, 360f, iF/l), 0.5f, 0.5f, out r, out g, out b);
            m.color = new Color(r / 255f, g / 255f, b / 255f);
            lines[i].material = m;
            lines[i].SetWidth(lineThickness, lineThickness);
            lines[i].SetColors(lineColor, lineColor);
            lines[i].SetPosition(0, points[i]);
        }
        currentIndex = 1;
        t = 0;

        X = csvToFloatArray(@"C:\Users\Kevin\Documents\GitHub\SFN2015PLLPosterAnimations\phase_file_out.csv");
        Y = csvToFloatArray(@"C:\Users\Kevin\Documents\GitHub\SFN2015PLLPosterAnimations\voltage_file_out.csv");
	}

    void Update()
    {
        currentIndex++;
        currentIndex %= X.Length;
        for (int i = 0; i < points.Length; i++)
        {
            lines[i].SetVertexCount(currentIndex);
            lines[i].SetPosition(currentIndex - 1, new Vector3(X[currentIndex][i]*5 + points[i].x, t + points[i].y, Y[currentIndex][i] * 5 + points[i].z));
        }
        t += deltaT;
    }

    private float[][] csvToFloatArray(string filename)
    {
        StreamReader reader = new StreamReader(filename);
        string file = reader.ReadToEnd();
        string[] lines = file.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        float[][] data = new float[lines.Length][];
        for(int i = 0; i < lines.Length; i++)
        {
            List<float> dat = new List<float>();
            string[] items = lines[i].Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);
            for(int j = 0; j < items.Length; j++)
                dat.Add(float.Parse(items[j]));
            data[i] = dat.ToArray();
        }
        return data;
    }

    /// <summary>
    /// Convert HSV to RGB
    /// h is from 0-360
    /// s,v values are 0-1
    /// r,g,b values are 0-255
    /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
    /// </summary>
    void HsvToRgb(float h, float S, float V, out int r, out int g, out int b)
    {
        // ######################################################################
        // T. Nathan Mundhenk
        // mundhenk@usc.edu
        // C/C++ Macro HSV to RGB

        float H = h;
        while (H < 0) { H += 360; };
        while (H >= 360) { H -= 360; };
        float R, G, B;
        if (V <= 0)
        { R = G = B = 0; }
        else if (S <= 0)
        {
            R = G = B = V;
        }
        else
        {
            float hf = H / 60.0f;
            int i = (int)Mathf.Floor(hf);
            float f = hf - i;
            float pv = V * (1 - S);
            float qv = V * (1 - S * f);
            float tv = V * (1 - S * (1 - f));
            switch (i)
            {

                // Red is the dominant color

                case 0:
                    R = V;
                    G = tv;
                    B = pv;
                    break;

                // Green is the dominant color

                case 1:
                    R = qv;
                    G = V;
                    B = pv;
                    break;
                case 2:
                    R = pv;
                    G = V;
                    B = tv;
                    break;

                // Blue is the dominant color

                case 3:
                    R = pv;
                    G = qv;
                    B = V;
                    break;
                case 4:
                    R = tv;
                    G = pv;
                    B = V;
                    break;

                // Red is the dominant color

                case 5:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                case 6:
                    R = V;
                    G = tv;
                    B = pv;
                    break;
                case -1:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // The color is not defined, we should throw an error.

                default:
                    //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                    R = G = B = V; // Just pretend its black/white
                    break;
            }
        }
        r = Clamp((int)(R * 255.0));
        g = Clamp((int)(G * 255.0));
        b = Clamp((int)(B * 255.0));
    }

    /// <summary>
    /// Clamp a value to 0-255
    /// </summary>
    int Clamp(int i)
    {
        if (i < 0) return 0;
        if (i > 255) return 255;
        return i;
    }
}
