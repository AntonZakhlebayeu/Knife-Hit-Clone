using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopItem")]
public class AssetShopItem : ScriptableObject, IShopItem
{
	public bool IsReceived
	{
		get
		{
			return _isReceived;
		}
		set
		{
			_isReceived = value;
		}
	}
	public bool IsChosen
	{
		get
		{
			return _isChosen;
		}
		set
		{
			_isChosen = value;
		}
	}

    [SerializeField] public Sprite knifeTexture => _knifeTexture;

	[SerializeField] public int Index => _index;

	[SerializeField] private bool _isReceived;
	[SerializeField] private bool _isChosen;
	[SerializeField] private int _index;
    [SerializeField] private Sprite _knifeTexture;
}
