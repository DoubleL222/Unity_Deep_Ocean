using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{

	public float minCameraHeight = 0;
	public float maxCameraHeight = 39;
	private Vector3 velocity = Vector3.zero;
	public Transform target;

	public float smooth = 3f;
	private Vector3 specificVector;
	void Start()
	{

	}

	void FixedUpdate () {

		specificVector = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
		Vector3 nextPos = Vector3.Lerp(transform.position, specificVector, smooth * Time.fixedDeltaTime);
		if (nextPos.y <= minCameraHeight)
		{
			nextPos.y = minCameraHeight;
		}
		else if(nextPos.y >= maxCameraHeight)
		{
			nextPos.y = maxCameraHeight;
		}
		transform.position = nextPos;
	}
}
