using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopItem
{
    public Sprite knifeTexture { get; }
	public bool IsReceived { get; set; }
	public bool IsChosen { get; set; }
	public int Index { get; }
}