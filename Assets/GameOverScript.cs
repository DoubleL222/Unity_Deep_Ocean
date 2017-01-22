using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

	bool listening = false;
	float gameEndedTime;
	public Text timeText;
	public Text restartText;
	bool quittingMode = false;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable()
	{
		gameEndedTime = Time.time;
		listening = true;
	}
	void OnDisable()
	{
		listening = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (listening)
		{
			float timeDiff = Time.time - gameEndedTime;
			float timeLeft = 5f - timeDiff;
			int tleft = (int)timeLeft;
			if (tleft < 0)
			{
				timeText.text = "";
				quittingMode = true;
				restartText.text = "Quit";
			}
			else
			{
				timeText.text = "NO?. . . . ." + tleft.ToString();
			}
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0))
			{
				if (quittingMode)
				{
					Application.Quit();
				}
				else
				{
					UnityEngine.SceneManagement.SceneManager.LoadScene(0);
				}
			}
		}
	}
}
