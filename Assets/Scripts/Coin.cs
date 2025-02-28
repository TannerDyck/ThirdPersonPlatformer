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
