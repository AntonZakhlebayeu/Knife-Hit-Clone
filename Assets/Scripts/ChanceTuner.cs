using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChanceTuner")]
public class ChanceTuner : ScriptableObject
{
    [SerializeField]
    private float chance;

    public float GetChance()
    {
        return this.chance;
    }
}
