using UnityEngine;
using System.Collections;

public class TrailSpawner : MonoBehaviour {
	float spawnFrequency = 1.0f;
	float lastSpawnTime = float.MinValue;
    public ParticleSystem p;

	// Use this for initialization
	void Start () {
		if (p == null)
			p = GetComponent<ParticleSystem>();
	}

	public void startEmitting()
	{
		p.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
