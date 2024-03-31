using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
	public InventoryItem[] Items;
	public Dictionary<InventoryItem, int> GetID = new Dictionary<InventoryItem, int>();

}
