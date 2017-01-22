using UnityEngine;
using System.Collections;

public class PlayerObject
{
	public float lastTapTime;
	float currentFrequency;

	public void logTap(float t)
	{
		currentFrequency = t - lastTapTime;
		lastTapTime = t;
	}

	public PlayerObject()
	{
		lastTapTime = float.MinValue;
	}
}

public class PlankMoverScript : MonoBehaviour {

	public float clickRate = 1.0f;

	PlayerObject playerOne;
	PlayerObject playerTwo;

	public Vector2 overlapBoxSize;
	public LayerMask lm;
	public Collider2D col;
	public int allCrates = 3;
	public float forceMultiplier = 5f;
	Rigidbody2D rbd;
	public Transform jellyRight;
	public Transform jellyLeft;
	// Use this for initialization
	private int frameCounter;
	public GameObject overlapBox;
	public bool drawGizmos = true;
	void Start () {
		frameCounter = 0;
		rbd = GetComponent<Rigidbody2D>();
		playerOne = new PlayerObject();
		playerTwo = new PlayerObject();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (Time.time > playerOne.lastTapTime + clickRate)
			{
				rbd.AddForceAtPosition(transform.up * forceMultiplier, jellyLeft.position, ForceMode2D.Impulse);
				playerOne.logTap(Time.time);
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (Time.time > playerTwo.lastTapTime + clickRate)
			{
				rbd.AddForceAtPosition(transform.up * forceMultiplier, jellyRight.position, ForceMode2D.Impulse);
				playerTwo.logTap(Time.time);
			}
		}
		if (Time.time % 3 > 2)
		{
			CheckForCratesOnPlank();
		}
	}

	public void CheckForCratesOnPlank()
	{
		Collider2D[] foundColliders = Physics2D.OverlapBoxAll(overlapBox.transform.position, overlapBoxSize, 0, lm);
		int boxCount = foundColliders.Length;
		//Debug.Log("box count " + boxCount);
	}
	void OnDrawGizmos()
	{
		if(drawGizmos)
			Gizmos.DrawCube(overlapBox.transform.position, overlapBoxSize);
	}

}
