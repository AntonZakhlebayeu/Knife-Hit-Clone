using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public static GameUI Instance { get; private set; }

	[Header("Knife Count Display")]
	[SerializeField]
	private GameObject panelKnives;
	[SerializeField]
	private GameObject iconKnife;
	[SerializeField]
	private Color usedKnifeIconColor;

	[Header("MainMenuUI")]
	[SerializeField]
	private GameObject MainMenu;
	[SerializeField]
	private Text scoreTextMainMenu;
	[SerializeField]
	private Text stageTextMainMenu;
	[SerializeField]
	private Text appleTextMainMenu;

	[Header("GameUI")]
	[SerializeField]
	private GameObject Game;
	[SerializeField]
	private Text scoreTextGame;
	[SerializeField]
	private Text stageTextGame;
	[SerializeField]
	private Text appleTextGame;

	[Header("GameOverUI")]
	[SerializeField]
	private GameObject GameOver;
	[SerializeField]
	private Text scoreTextGameOver;
	[SerializeField]
	private Text stageTextGameOver;
	[SerializeField]
	private Text appleTextGameOver;
	[SerializeField]
	private GameObject restartButton;
	[SerializeField]
	private GameObject homeButton;

	[Header("Shop")]
	[SerializeField]
	private GameObject ShopTemplate;


	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		LaunchMainMenu();
	}

	public void ShowRestartButton()
	{
		restartButton.SetActive(true);
	}

	public void SetInitialDisplayedKnifeCount(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Instantiate(iconKnife, panelKnives.transform);
		}
	}

	public void DestroyDisplayedKnifeCount()
	{
		foreach (Transform child in panelKnives.transform)
		{
			Destroy(child.gameObject);
		}
	}
	public int knifeIconIndexToChange = 0;

	public void DecrementDisplayedKnifeCount()
	{
		panelKnives.transform.GetChild(knifeIconIndexToChange++).GetComponent<Image>().color = usedKnifeIconColor;
	}

	public static void UpdateScoreText(int score)
	{
		Instance.scoreTextGame.text = score.ToString();
	}

	public static void UpdateStageText(int stage)
	{
		Instance.stageTextGame.text = stage.ToString();
	}

	public static void UpdateAppleText(int apple)
	{
		Instance.appleTextGame.text = apple.ToString();
	}

	[SerializeField]
	private static void OnclickRestartButton()
	{
		GameController.Instance.RestartGame();
	}

	[SerializeField]
	public static void OnclickShopButton()
	{
		Shop.Instance.Render(Shop.Instance.ShopItems);

		Instance.MainMenu.SetActive(false);
		Instance.Game.SetActive(false);
		Instance.GameOver.SetActive(false);
		Instance.ShopTemplate.SetActive(true);

		GameController.Instance.SpawnKnifeSWithoutDelay();
	}

	public static void OnClickBackButton()
	{
		GameController.Instance.DestroyKnife();
		LaunchMainMenu();
	}


	public static void LaunchMainMenu()
	{
		Variables._isGameStarted = false;

		Instance.MainMenu.SetActive(true);
		Instance.Game.SetActive(false);
		Instance.GameOver.SetActive(false);
		Instance.ShopTemplate.SetActive(false);

		Instance.scoreTextMainMenu.text = DataManager.ReturnHighScore().ToString();
		Instance.stageTextMainMenu.text = DataManager.ReturnHighStage().ToString();
		Instance.appleTextMainMenu.text = Variables.apple.ToString();
	}

	public static void OnClickGameButton()
	{
		GameController.Instance.StartGame();
		LaunchGame();
	}

	public static void LaunchGame()
	{
		Instance.MainMenu.SetActive(false);
		Instance.Game.SetActive(true);
		Instance.GameOver.SetActive(false);
		Instance.ShopTemplate.SetActive(false);

		Instance.appleTextGame.text = Variables.apple.ToString();
	}

	public static void LaunchGameOver()
	{
		Variables._isGameStarted = false;

		Instance.MainMenu.SetActive(false);
		Instance.Game.SetActive(false);
		Instance.GameOver.SetActive(true);
		Instance.ShopTemplate.SetActive(false);

		Instance.scoreTextGameOver.text = Variables.score.ToString();
		Instance.stageTextGameOver.text = Variables.stage.ToString();
		Instance.appleTextGameOver.text = Variables.apple.ToString();

		NotificationManager.SendNotification();

		GameController.Instance.DestroyKnife();
	}

	public static void OnClickHomeButton()
	{
		GameController.Instance.RestartGame();
		LaunchMainMenu();
	}

	public static void OnClickRestartButton()
	{
		GameController.Instance.RestartGame();
		//TODO
		GameController.Instance.SpawnKnifeSWithoutDelay();
		Wood.CreateWood();
		LaunchGame();
	}
}
