using UnityEngine;
using System.Collections;

public static class UtilityScripts
{
	public static float maxy = Screen.height / 4f;
	public static float miny = -Screen.height / 4f;
	public static bool GameStarted = true;
	public static Color clickColor;
	public static float Remap(this float value, float from1, float to1, float from2, float to2)
	{
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}


}
