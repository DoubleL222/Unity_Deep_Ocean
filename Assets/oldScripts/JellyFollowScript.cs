using UnityEngine;
using System.Collections;

public class JellyFollowScript : MonoBehaviour {

	public Transform targetTransform;
	Vector3 specificVector;
	public float smooth;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		specificVector = new Vector3(targetTransform.transform.position.x, targetTransform.transform.position.y, transform.position.z);
		Vector3 nextPos = Vector3.Lerp(transform.position, specificVector, smooth * Time.fixedDeltaTime);
		transform.position = nextPos;
	}
}
