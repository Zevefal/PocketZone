using Inventory.Model;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
	[SerializeField] private InventoryScriptableObject _inventoryData;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		Item item = other.GetComponent<Item>();
		
		if(item != null)
		{
			int reminder = _inventoryData.AddItem(item.InventoryItem, item.Amount);
			
			if(reminder == 0)
				item.DestroyItem();
			else
				item.Amount = reminder;
		}
	}
}
