using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour {

    public static AnimationController instance;
    public Image tutorial;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleTutorialImage()
    {
        tutorial.enabled = !tutorial.enabled;
    }

    public void HideTutorialImage()
    {
        tutorial.enabled = false;
    }
}
