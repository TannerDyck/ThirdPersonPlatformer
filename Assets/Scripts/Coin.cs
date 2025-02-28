/*
This script was largely inspired by Ketra Games' YouTube video "How to Collect Items (Unity Tutorial)"
*/
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CharacterInventory characterInventory = other.GetComponent<CharacterInventory>();

        if (characterInventory != null) 
        {
            characterInventory.coinCollected();
            gameObject.SetActive(false);
        }
    }
}
