using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI coinText;

    void Start()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    public void updateCoinText(CharacterInventory characterInventory) 
    {
        coinText.text = characterInventory.numCoins.ToString() + " / 9";
    }
}
