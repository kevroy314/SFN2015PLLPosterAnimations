using UnityEngine;
using System.Collections;

public class PhaseChanger : MonoBehaviour {

    public SineWaveMaker sineWaveScript;
    public float phaseDeviationPerUnitTime;
    private float phase;
    public GameObject objectToColorChange;
    public Color lowColor;
    public Color highColor;
	// Use this for initialization
	void Start () {
        phase = sineWaveScript.phase;
	}
	
	// Update is called once per frame
	void Update() {
        if (sineWaveScript.Time >= 0)
        {
            phase += phaseDeviationPerUnitTime;
            phase %= Mathf.PI * 2;
            sineWaveScript.phase = Mathf.Sin(phase);
            objectToColorChange.GetComponent<Renderer>().material.color = Color.Lerp(lowColor, highColor, phase / (Mathf.PI * 2));
        }
	}
}
