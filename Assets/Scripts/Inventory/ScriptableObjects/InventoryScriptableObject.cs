using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Model
{
	[CreateAssetMenu(menuName = "Inventory/Inventory")]
	public class InventoryScriptableObject : ScriptableObject
	{
		[SerializeField] private List<InventoryItem> _inventoryItems;
		[field: SerializeField] public int Size { get; private set; } = 10;

		public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

		public void Initialize()
		{
			_inventoryItems = new List<InventoryItem>();

			for (int i = 0; i < Size; i++)
			{
				_inventoryItems.Add(InventoryItem.GetEmptyItem());
			}
		}

		public int AddItem(ItemScriptableObject item, int amount)
		{
			if (item.IsStackable == false)
			{
				for (int i = 0; i < _inventoryItems.Count; i++)
				{
					while (amount > 0 && IsInvenotoryFull() == false)
					{
						amount -= AddItemToFreeSlot(item, 1);
					}

					InformAboutChange();
					return amount;
				}
			}

			amount = AddStackableItem(item, amount);
			InformAboutChange();
			return amount;
		}

		public void AddItem(InventoryItem item)
		{
			AddItem(item.Item, item.Amount);
		}

		public Dictionary<int, InventoryItem> GetCurrentInventoryState()
		{
			Dictionary<int, InventoryItem> returnValue = new Dictionary<int, InventoryItem>();

			for (int i = 0; i < _inventoryItems.Count; i++)
			{
				if (_inventoryItems[i].IsEmpty)
					continue;

				returnValue[i] = _inventoryItems[i];
			}

			return returnValue;
		}

		public int FindItemInInvenory(ItemScriptableObject item)
		{
			for (int i = 0; i < _inventoryItems.Count; i++)
			{
				if (_inventoryItems[i].Item == item)
				{
					return i;
				}
			}

			return 0;
		}

		public InventoryItem GetItemAt(int itemIndex)
		{
			return _inventoryItems[itemIndex];
		}

		public List<InventoryItem> GetAllItems()
		{
			List<InventoryItem> items = new List<InventoryItem>();

			// foreach (var item in _inventoryItems)
			// {
			// 	if (item.IsEmpty)
			// 		continue;
	
			// 	items.Add(item);
			// }
			
			for(int i = 0; i < _inventoryItems.Count; i++)
			{
				if(_inventoryItems[i].IsEmpty)
					continue;
				
				items.Add(_inventoryItems[i]);
			}

			return items;
		}

		public void RemoveItem(int itemIndex, int amountToRemove)
		{
			if (_inventoryItems.Count > itemIndex)
			{
				if (_inventoryItems[itemIndex].IsEmpty)
					return;

				int reminder = _inventoryItems[itemIndex].Amount - amountToRemove;

				if (reminder <= 0)
					_inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
				else
					_inventoryItems[itemIndex] = _inventoryItems[itemIndex].ChangeAmount(reminder);

				InformAboutChange();
			}
		}

		private int AddStackableItem(ItemScriptableObject item, int amount)
		{
			for (int i = 0; i < _inventoryItems.Count; i++)
			{
				if (_inventoryItems[i].IsEmpty)
					continue;

				if (_inventoryItems[i].Item.ID == item.ID)
				{
					int amontPossibleToTake = _inventoryItems[i].Item.MaxStackSize - _inventoryItems[i].Amount;

					if (amount > amontPossibleToTake)
					{
						_inventoryItems[i] = _inventoryItems[i].ChangeAmount(_inventoryItems[i].Item.MaxStackSize);
						amount -= amontPossibleToTake;
					}
					else
					{
						_inventoryItems[i] = _inventoryItems[i].ChangeAmount(_inventoryItems[i].Amount + amount);
						InformAboutChange();
						return 0;
					}
				}
			}

			while (amount > 0 && IsInvenotoryFull() == false)
			{
				int newAmount = Mathf.Clamp(amount, 0, item.MaxStackSize);
				amount -= newAmount;
				AddItemToFreeSlot(item, newAmount);
			}

			return amount;
		}

		private int AddItemToFreeSlot(ItemScriptableObject item, int amount)
		{
			InventoryItem newItem = new InventoryItem
			{
				Item = item,
				Amount = amount,
			};

			for (int i = 0; i < _inventoryItems.Count; i++)
			{
				if (_inventoryItems[i].IsEmpty)
				{
					_inventoryItems[i] = newItem;
					return amount;
				}
			}

			return 0;
		}

		private void InformAboutChange()
		{
			OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
		}

		private bool IsInvenotoryFull() => _inventoryItems.Where(item => item.IsEmpty).Any() == false;
	}

	[Serializable]
	public struct InventoryItem
	{
		public int Amount;
		public ItemScriptableObject Item;
		public bool IsEmpty => Item == null;

		public InventoryItem ChangeAmount(int newAmount)
		{
			return new InventoryItem
			{
				Item = this.Item,
				Amount = newAmount,
			};
		}

		public static InventoryItem GetEmptyItem()
			=> new InventoryItem
			{
				Item = null,
				Amount = 0,
			};
	}
}