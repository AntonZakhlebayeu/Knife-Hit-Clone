using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
	[SerializeField] public GameObject _iconField;
    [SerializeField] public Sprite _locked;
	[SerializeField] public int cellIndex;

	public static ShopCell Instance{ get; private set; }


	private void Awake()
	{
		Instance = this;
	}

	public void Render(IShopItem shopItem, int index)
	{
		if (shopItem.IsReceived) 
        {
			_iconField.GetComponent<Image>().sprite = shopItem.knifeTexture;
        }
        else
        {
            _iconField.GetComponent<Image>().sprite = _locked;
        }
		cellIndex = index;
	}

	public void OnClickShopCell()
	{
		var shopItem = ShopItemWrapper(cellIndex);
		Shop.ChooseShopItem(shopItem, cellIndex);
	}

	public static AssetShopItem ShopItemWrapper(int index)
	{
		return Shop.Instance.ShopItems[index];
	}

}
