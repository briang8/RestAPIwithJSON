using UnityEngine;
using TMPro;

public class PlayerUIDisplay : MonoBehaviour
{

    public TMP_Text playerInfoText;
    public Transform inventoryContent;
    public GameObject inventoryItemPrefab;

    public void ShowData(PlayerData data)
    {
        PlayerRecord record = data.record;

        playerInfoText.text = "Name: " + record.playerName + "\n" +
            "Level: " + record.level + "\n" +
            "Health: " + record.health + "\n" +
            "Position: (" + record.position.x + ", " + record.position.y + ", " + record.position.z + ")";

        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in record.inventory)
        {
            GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContent);
            TMP_Text itemText = newItem.GetComponent<TMP_Text>();
            itemText.text = item.itemName + " x" + item.quantity + " (weight: " + item.weight + ")";
        }
    }


    /*// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    } */


}
