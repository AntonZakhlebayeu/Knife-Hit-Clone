using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DataManager
{
	public static void SetHighScore(int score)
	{
		if (score > ReturnHighScore())
		{
			PlayerPrefs.SetInt("HighScore", score);
			PlayerPrefs.Save();
		}
	}

	public static int ReturnHighScore()
	{
		return PlayerPrefs.GetInt("HighScore", 0);
	}

	public static void SaveHighStage(int stage)
	{
		if (stage > ReturnHighStage())
		{
			PlayerPrefs.SetInt("HighStage", stage);
			PlayerPrefs.Save();
		}
	}

	public static int ReturnHighStage()
	{
		return PlayerPrefs.GetInt("HighStage", 0);
	}

	public static void SetAmountOfApples(int apple)
	{
		PlayerPrefs.SetInt("AmountOfApples", apple);
		PlayerPrefs.Save();
	}

	public static int GetAmountOfApples()
	{
		return PlayerPrefs.GetInt("AmountOfApples", 0);
	}

	public static void SaveUserDate(string TodayDate)
	{
		PlayerPrefs.SetString("Date", TodayDate);
		PlayerPrefs.Save();
	}

	public static DateTime GetUserDate()
	{
		string DefaultDate = DateTime.Now.ToString();
		return DateTime.Parse(PlayerPrefs.GetString("Date", DefaultDate));
	}

	public static void SetFirstEnter()
	{
		PlayerPrefs.SetInt("FirstEnter", 0);
		PlayerPrefs.Save();
	}

	public static bool GetFirstEnter()
	{
		if ((PlayerPrefs.GetInt("FirstEnter", 1)) == 0) return false;
		else return true;
	}

	public static void SaveShopCondition(List<AssetShopItem> Items)
	{
		string IsReceiveds = "";
		string IsChosens = "";
		for (int k = 0; k < Items.Count; k++)
		{
			IsReceiveds += ((Items[k].IsReceived) ? "true" : "false") + "\n";
		}
		for (int k = 0; k < Items.Count; k++)
		{
			IsChosens += ((Items[k].IsChosen) ? "true" : "false") + "\n";
		}
		PlayerPrefs.SetString("SavedIsReceiveds", IsReceiveds);
		PlayerPrefs.SetString("SavedIsChosens", IsChosens);
	}

	public static void GetShopCondition(ref List<AssetShopItem> Items)
	{
		string IsReceiveds = PlayerPrefs.GetString("SavedIsReceiveds");
		string IsChosens = PlayerPrefs.GetString("SavedIsChosens");

		string[] IsReceivedsArray = IsReceiveds.Split('\n');
		for (int i = 0; i < Items.Count; i++)
		{
			Items[i].IsReceived = ((IsReceivedsArray[i] == "true") ? true : false);
		}

		string[] IsChosensArray = IsChosens.Split('\n');
		for (int i = 0; i < Items.Count; i++)
		{
			Items[i].IsChosen = ((IsChosensArray[i] == "true") ? true : false);
		}
	}
}
