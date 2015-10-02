using UnityEngine;
using System.Collections;

public class ScreenRecorder : MonoBehaviour {
    public string path;
    private int frameNum;
	// Use this for initialization
	void Start () {
        frameNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Application.CaptureScreenshot(path+"Frame" + frameNum.ToString("D3") + ".png");
        frameNum++;
    }
}
