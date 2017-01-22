using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SinDot : MonoBehaviour {

	Image myImg;
	RectTransform rt;
	public SinDot next = null;
	public SinDot prev = null;
	private Vector3 pos;
	float currValue = 0;
	float smooth= 4;
	Vector3 originalPos;
	public bool clickable = false;
	Color baseColor;
	Vector3 personalTarget;
	// Use this for initialization

	bool started = false;
	void Start ()
	{
		myImg = GetComponent<Image>();
		baseColor = myImg.color;
		rt = GetComponent<RectTransform>();
		originalPos = transform.position;
	}

	public void UpdateValue(float newValue, bool click, Color c)
	{
		if (!started)
			started = true;
		if (!myImg)
		{
			myImg = GetComponent<Image>();
		}
		if (next)
			next.UpdateValue(currValue, clickable, myImg.color);
		if (myImg)
		{
			myImg.color = c;
		}
		clickable = click;
		currValue = newValue;
	}

	// Update is called once per frame
	void Update ()
	{
		if (started)
		{
			if (!rt)
			{
				rt = GetComponent<RectTransform>();
			}
			float newHeight = UtilityScripts.Remap(currValue, -1, 1, UtilityScripts.miny, UtilityScripts.maxy);
			Vector2 destination = new Vector2(rt.anchoredPosition.x, newHeight);
			Vector2 nextPos = Vector2.Lerp(rt.anchoredPosition, destination, smooth * Time.deltaTime);
			rt.anchoredPosition = nextPos;
		}
	}
}
