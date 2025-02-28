using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public int numCoins { get; private set; }

    public void coinCollected()
    {
        numCoins++;
    }
}
