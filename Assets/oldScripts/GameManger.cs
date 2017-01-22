using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManger : MonoBehaviour {

	public List<crateInstance> allTheCrates = new List<crateInstance>();

	private static GameManger _instance;
	public static GameManger instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<GameManger>() as GameManger;
			}
			return _instance;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
