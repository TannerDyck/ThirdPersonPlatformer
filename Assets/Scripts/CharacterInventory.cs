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
