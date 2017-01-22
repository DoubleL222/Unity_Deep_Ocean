using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

	public static GuiManager _instance = null;
	public static GuiManager instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<GuiManager>();
			return _instance;
		}
	}

	public GameObject gameOverPanel;

	public void ShowGameOverPanel()
	{
		gameOverPanel.SetActive(true);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
