using UnityEngine;
using System.Collections;

public class SineWaveMaker : MonoBehaviour {
    public int numberOfObjects;
    public float frequency;
    public float phase;
    public float amplitude;
    public GameObject prototypeObject;
    private GameObject[] objs;
    private float t;
    public float deltaT;
    public float objectOffset;
    public bool loop;
    public float maxLength;
    public float startTimeDelay;
    public float noiseFactor;
    public float globalStopTime;
    private bool triggerFade;
	// Use this for initialization
	void Start () {
        objs = new GameObject[numberOfObjects];
        for (int i = 0; i < numberOfObjects; i++)
        {
            objs[i] = (GameObject)Instantiate(prototypeObject, transform.position, Quaternion.Euler(Vector3.zero));
            objs[i].transform.parent = transform;
            objs[i].SetActive(false);
        }
        t = -startTimeDelay;
        triggerFade = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (t > 0)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                float posX = (i * objectOffset - (objs.Length * objectOffset)) + t;
                if (posX <= 0) posX = 0;
                else
                {
                    objs[i].SetActive(true);
                    if (loop)
                        posX = posX % maxLength;
                    else
                        if (posX > maxLength)
                        objs[i].SetActive(false);
                    float posY = amplitude * Mathf.Sin(frequency * posX + phase);
                    posY += ((Random.value - 0.5f) * noiseFactor);
                    objs[i].transform.localPosition = new Vector3(posX, posY);
                }
            }
        }
        t += deltaT;

        if (t >= globalStopTime - 5 && !triggerFade)
        {
            triggerFade = true;
            for (int i = 0; i < objs.Length; i++)
                iTween.FadeTo(objs[i], 0f, 1);
        }

        if(t>= globalStopTime)
        {
            for(int i = 0; i < objs.Length; i++)
                DestroyImmediate(objs[i]);
            Destroy(gameObject);
        }
	}

    public float Time
    {
        get
        {
            return t;
        }
    }
}
