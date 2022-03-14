using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	public List<AssetShopItem> ShopItems;
	[SerializeField] public ShopCell _shopCellTemplate;
	[SerializeField] public ShopCell _shopCellReceivedTemplate;
	[SerializeField] public ShopCell _shopCellChosenTemplate;
	[SerializeField] private Transform _container;

	public static Shop Instance { get; private set; }

	private void Awake()
	{
		Instance = this;


		if (DataManager.GetFirstEnter())
		{
			DataManager.SetFirstEnter();

			DataManager.SaveShopCondition(ShopItems);
			Debug.Log("Saved");
		}
		else
		{
			DataManager.GetShopCondition(ref ShopItems);
		}

		Shop.ReturnChosenSkin();
	}

	public void Render(List<AssetShopItem> shopItems)
	{
		int index = 0;

		foreach (Transform child in _container)
		{
			Destroy(child.gameObject);
		}

		shopItems.ForEach(shopItem =>
		{
			ShopCell cell = null;

			if (!shopItem.IsReceived && !shopItem.IsChosen) cell = Instantiate(_shopCellTemplate, _container);
			else if (shopItem.IsReceived && !shopItem.IsChosen) cell = Instantiate(_shopCellReceivedTemplate, _container);
			else if (shopItem.IsReceived && shopItem.IsChosen) cell = Instantiate(_shopCellChosenTemplate, _container);
			else Debug.Log("Invalid!");


			cell.Render(shopItem, index);
			index++;
		});
	}

	public static void ChooseShopItem(AssetShopItem Item, int index)
	{
		{
			if (Item.IsReceived == true)
			{
				UpdatePlayerSkin(Item);
				UpdateChooseStatus(Item);
			}
		}
	}

	public static void GetNewSkin(int index)
	{
		if (!Instance.ShopItems[index].IsReceived)
		{
			Instance.ShopItems[index].IsReceived = true;
			DataManager.SaveShopCondition(Shop.Instance.ShopItems);
		}
	}

	public static void UpdatePlayerSkin(AssetShopItem Item)
	{
		GameController.Instance.knifeObject.GetComponent<SpriteRenderer>().sprite = Item.knifeTexture;
		GameController.Instance.DestroyKnife();
		GameController.Instance.SpawnKnifeSWithoutDelay();
	}

	public static void UpdateChooseStatus(AssetShopItem ChosenItem)
	{
		ChosenItem.IsChosen = true;
		foreach (AssetShopItem Item in Instance.ShopItems)
		{
			if (Item != ChosenItem && Item.IsChosen == true)
			{
				Item.IsChosen = false;
			}
		}
		DataManager.SaveShopCondition(Instance.ShopItems);
		Instance.Render(Instance.ShopItems);
	}


	public static void ReturnChosenSkin()
	{
		foreach (AssetShopItem Item in Instance.ShopItems)
		{
			if (Item.IsChosen == true)
			{
				GameController.Instance.knifeObject.GetComponent<SpriteRenderer>().sprite = Item.knifeTexture;
				break;
			}
		}
	}
}
