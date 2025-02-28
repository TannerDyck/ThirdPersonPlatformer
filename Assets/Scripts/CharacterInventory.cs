/*
This script was largely inspired by Ketra Games' YouTube video "How to Collect Items (Unity Tutorial)"
*/
using UnityEngine;
using UnityEngine.Events;

public class CharacterInventory : MonoBehaviour
{
    public int numCoins { get; private set; }
    public UnityEvent<CharacterInventory> onCoinCollected;

    public void coinCollected()
    {
        numCoins++;
        onCoinCollected.Invoke(this);
    }
}
