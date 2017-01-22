using UnityEngine;
using System.Collections;

public class oilScript : MonoBehaviour {

	public Transform followObject;
	public float currentSpeed;
	public float normalSpeed = 1;
	public float boostedSpeedK = 3;
	public float slowedSpeed = 0.5f;
	public float maxDistanceToPlayer = 200f;
	public float minDistance = 20f;
	bool started = false;
	// Use this for initialization
	void Start ()
	{
		followObject = GameObject.FindGameObjectWithTag("PlayerPlank").transform;
		boostedSpeedK = normalSpeed * boostedSpeedK;
		slowedSpeed = normalSpeed * slowedSpeed;
		currentSpeed = normalSpeed;
		StartFlowing();
	}

	public void StartFlowing()
	{
		started = true;
	}
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + Vector3.right * Time.deltaTime * currentSpeed;
		float dis = Mathf.Abs(followObject.position.x - transform.position.x);
		//Debug.Log("Distance :" + dis);
		if (dis >= maxDistanceToPlayer)
		{
			Debug.Log("BOOSTING OIL");
			currentSpeed = boostedSpeedK;
		}
		else if (dis <= minDistance)
		{
			Debug.Log("SLOWING OIL OIL");
			currentSpeed = normalSpeed;
		}
	}

}
