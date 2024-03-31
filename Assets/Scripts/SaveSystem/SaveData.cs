using System.Collections.Generic;
using Inventory.Model;

[System.Serializable]
public class SaveData
{	
	public float PlayerHealth;
	public List<InventoryItem> Items;
	
	public float PlayerPositionX;
	public float PlayerPositionY;
	
	
	public SaveData(float health, List<InventoryItem> items, float playerPositionX, float playerPositionY)
	{
		PlayerHealth = health;
		PlayerPositionX = playerPositionX;
		PlayerPositionY = playerPositionY;
		Items = items;
	}
}