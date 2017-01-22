using UnityEngine;
using System.Collections;

public class ClickListener : MonoBehaviour {
	public SinDotMaster sindotmaster;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (SinDotMaster.instance.getRoundRunning())
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				sindotmaster.ClickDetected();
			}
		}
	}
}
