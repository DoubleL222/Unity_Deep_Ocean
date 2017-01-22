using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SinDotMaster : MonoBehaviour {

	[Header("TextColors")]
	public Color badColor;
	public Color neutralColor;
	public Color goodColor;

	[Header("References")]
	public Text displayText;
	public Text multiplierText;

	[Header("Small images settings")]
	public float imagesWidth;
	public float imagesHeight;
	public float sinDotsDis;
	public Color markerC;


	[Header("Gemeral Game Settings")]
	public int difficulty = 0;
	float RoundDuration = 30;
	public float XK;
	public int sinDots;
	public int ClickerIndex;
	public float updateRate;

	[Header("Advanced Game Settings")]
	public int spawnedClickersRequieredForThrow = 20;

	[Header("Prefabs")]
	public GameObject sinDotPrefab;

	

	public static SinDotMaster _instance;
	public static SinDotMaster instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<SinDotMaster>();
			}
			return _instance;
		}
	}

	int spawnedClickers;
	int currentRoundPoints = 0;
	int bonusModifier = 1;
	float modifier = 0;
	float lastUpdate = float.MinValue;
	private List<SinDot> allSinDots;
	private SinDot firstSinDot;
	float currValue;
	bool curving = false;
	bool roundRunning = false;
	float roundStartedTime;
	bool goingStraight = false;

	//Duration in seconds

	// Use this for initialization
	void Start () {
		StartRound();
	}

	void ClearSinDots()
	{

	}

	public bool getRoundRunning()
	{
		return roundRunning;
	}

	public void StartRound()
	{
		roundRunning = true;
		DeleteSinDots();
		SpawnSinDots();
	}

	void StopRound()
	{
		goingStraight = true;
		curving = false;
		StartCoroutine(delayedRoundend());
	}


	void DeleteSinDots()
	{
		foreach (Transform t in transform)
		{
			Destroy(t.gameObject);
		}
	}

	public void ClickDetected()
	{
		SinDot currDot = allSinDots[ClickerIndex];
		int closestPoint = findClosestClick();
		if (closestPoint == -1)
		{
			currDot.gameObject.GetComponent<Image>().color = Color.red;
			currentRoundPoints -= 2;
			bonusModifier = 1;
		}
		else if (closestPoint == ClickerIndex + 1 || closestPoint == ClickerIndex - 1)
		{
			currDot.gameObject.GetComponent<Image>().color = Color.yellow;
			currentRoundPoints += (1*bonusModifier);
			bonusModifier = 1;
		}
		else if (closestPoint == ClickerIndex)
		{
			bonusModifier += 1;
			
			currDot.gameObject.GetComponent<Image>().color = Color.green;
			currentRoundPoints += (1 * bonusModifier);
		}
		CheckScore();
	}

	void CheckScore()
	{
		multiplierText.text = "X" + bonusModifier;
		if (bonusModifier > 1)
		{
			multiplierText.color = goodColor;
		}
		else
		{
			multiplierText.color = neutralColor;
		}
		float percentage = (float)(currentRoundPoints) / (float)(spawnedClickers);
		Debug.Log("Current points: " + currentRoundPoints + "; spawned clickers: " + spawnedClickers + "; Percentage " + percentage);
		string newScore = "OK";
		if (percentage < 0.2)
		{
			displayText.color = badColor;
			if (spawnedClickers > spawnedClickersRequieredForThrow)
			{
				StopRound();
				GameOver();
			}
			newScore = "Terrible";
		}
		else if (percentage < 0.4)
		{
			displayText.color = badColor;
			newScore = "Bad";
		}
		else if (percentage < 0.6)
		{
			displayText.color = neutralColor;
			newScore = "OK";
		}
		else if (percentage < 0.8)
		{
			displayText.color = neutralColor;
			newScore = "Decent";
		}
		else if (percentage < 1)
		{
			displayText.color = goodColor;
			newScore = "Good";
		}
		else if (percentage >= 1)
		{
			displayText.color = goodColor;
			newScore = "AMAZING";
		}
		percentage *= 100;
		displayText.text = (int)(percentage) + "%"; 
		//displayText.text = newScore;

	}

	int findClosestClick()
	{

		if (allSinDots[ClickerIndex].clickable)
		{
			return ClickerIndex;
		}
		if (allSinDots[ClickerIndex + 1].clickable)
		{
			allSinDots[ClickerIndex + 1].clickable = false;
			return ClickerIndex + 1;
		}
		if (allSinDots[ClickerIndex - 1].clickable)
		{
			allSinDots[ClickerIndex - 1].clickable = false;
			return ClickerIndex - 1;
		}
		return -1;
	}

	void SpawnSinDots()
	{
		allSinDots = new List<SinDot>();
		for (int i = 0; i < sinDots; i++)
		{
			GameObject go = Instantiate(sinDotPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
			RectTransform currRect = go.GetComponent<RectTransform>();
			currRect.sizeDelta = new Vector2(imagesWidth, imagesHeight);
			currRect.anchoredPosition = new Vector3(sinDotsDis, 0) * -(i + 1);
			SinDot currDot = go.GetComponent<SinDot>();
			allSinDots.Add(currDot);
			if (i == ClickerIndex)
			{
				GameObject gobj = Instantiate(sinDotPrefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
				RectTransform clickerRect = gobj.GetComponent<RectTransform>();
				gobj.GetComponent<Image>().color = markerC;
				gobj.name = "WTFOBJECZ";
				clickerRect.sizeDelta = new Vector2(imagesWidth, imagesHeight*50);
				clickerRect.anchoredPosition = new Vector3(sinDotsDis, 0) * -(i + 1);
			}
		}

		for (int i = 0; i < sinDots - 1; i++ )
		{
			allSinDots[i].next = allSinDots[i + 1];
			allSinDots[i + 1].prev = allSinDots[i];
		}
		firstSinDot = allSinDots[0];
		curving = true;
		roundStartedTime = Time.time;
	}

	IEnumerator delayedRoundend()
	{
		yield return new WaitForSeconds(1);
		goingStraight = false;
		yield return new WaitForSeconds(2);
		roundRunning = false;
		DeleteSinDots();
		yield return null;

	}
	// Update is called once per frame
	void Update ()
	{
		if (curving)
		{
			if (Time.time > roundStartedTime + RoundDuration)
			{
				StopRound();
			}
			if (lastUpdate + updateRate < Time.time)
			{
				bool clickable = decideIfClickable();

				lastUpdate = Time.time;
				float newVal = Mathf.Sin(Time.time * XK);
				if (clickable)
				{
					firstSinDot.UpdateValue(newVal, clickable, Color.blue);
				}
				else
				{
					firstSinDot.UpdateValue(newVal, clickable, Color.white);
				}
				if (allSinDots[ClickerIndex + 3].clickable)
				{
					Debug.Log("MISSED KEY");
					MissedKey();
					SetNewFrequency();
					allSinDots[ClickerIndex + 3].clickable = false;
					CheckScore();
				}
			}
		} else if (goingStraight)
		{
			firstSinDot.UpdateValue(0f, false, Color.white);
		}
	}

	void GameOver()
	{
		GuiManager.instance.ShowGameOverPanel();
	}

	void ChangeColorForAllDots(Color c)
	{
		for (int j = 0; j < allSinDots.Count; j++)
		{
			allSinDots[j].GetComponent<Image>().color = c;
		}
	}

	void MissedKey()
	{
		currentRoundPoints -= 2;
	}
	bool decideIfClickable()
	{
		float ran = Random.value;
		if (ran > 0.9 - modifier)
		{
			spawnedClickers++;
			modifier = 0;
			return true;
		}
		else
		{
			modifier += 0.01f+ (float)(0.01f * (float)(difficulty));
			return false;
		}

	}

	void SetNewFrequency()
	{
		float ran = Random.value;
		if (ran < (float)(0.1f * (float)difficulty))
		{
			float newFrequency = Random.Range(-5, 5);
			XK = newFrequency;
		}
	}
}
