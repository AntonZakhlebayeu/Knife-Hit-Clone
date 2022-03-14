using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
	public static int score = 0;
	public static int stage = 1;
	public static int apple;
	public static int woodLevelIndex = 0;

	public static bool _isGameStarted = false;
	public static bool _isGameWon = false;

	public Transform spawnWoodPosition;

	public static Variables Instance;

	private void Awake()
	{
		Instance = this;

		apple = DataManager.GetAmountOfApples();
	}
}
