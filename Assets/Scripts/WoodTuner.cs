using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodTuner : MonoBehaviour
{
    public static WoodTuner Instance { get; private set; }

    [System.Serializable]
	public class WoodPrefab
	{
		public GameObject woodObject;
		public int knifeCount;
	}

	[Header("Wood Prefabs without apples")]
    public WoodPrefab[] woodPrefabsWithoutApples;

	[Header("Wood prefabs with apples")]
	public WoodPrefab[] woodPrefabsWithApples;

	[Header("Boss prefabs")]
	public WoodPrefab[] bossPrefabs;

    private void Awake()
	{
		Instance = this;
	}
}
