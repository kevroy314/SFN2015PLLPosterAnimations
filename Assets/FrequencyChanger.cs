using UnityEngine;
using System.Collections;

public class FrequencyChanger : MonoBehaviour {

    public SineWaveMaker sineWaveScript;
    public float frequencyDeviationPerUnitTime;
    private float frequency;
    private float startFrequency;
    public GameObject objectToColorChange;
    public Color lowColor;
    public Color highColor;
    // Use this for initialization
    void Start () {
        startFrequency = sineWaveScript.frequency;
	}
	
	// Update is called once per frame
	void Update () {
        if (sineWaveScript.Time >= 0)
        {
            frequency = startFrequency + Mathf.Sin(sineWaveScript.Time * frequencyDeviationPerUnitTime);
            sineWaveScript.frequency = frequency;
            objectToColorChange.GetComponent<Renderer>().material.color = Color.Lerp(lowColor, highColor, frequency / (Mathf.PI * 2));
        }
    }
}
