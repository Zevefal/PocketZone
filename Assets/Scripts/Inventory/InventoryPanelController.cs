using UnityEngine;
using UnityEngine.UI;
using Inventory.UI;
using Inventory.Model;
using System.Collections.Generic;

namespace Inventory
{
	public class InventoryPanelController : MonoBehaviour
	{
		[SerializeField] private InventoryUI _inventory;
		[SerializeField] private InventoryScriptableObject _inventoryData;
		[SerializeField] private Button _inventoryButton;

		public List<InventoryItem> initialItems = new List<InventoryItem>();

		private void OnEnable()
		{
			_inventoryButton.onClick.AddListener(ShowInventory);
		}

		private void OnDisable()
		{
			_inventoryButton.onClick.RemoveListener(ShowInventory);

			_inventory.OnDescriptionRequested -= HandleDescriptionRequest;
			_inventory.OnItemActionRequested -= HandleItemActionRequest;
		}

		// public void SetInventoryActive()
		// {
		// 	_inventoryMenu.gameObject.GetComponent<InventoryController>().InitializeInvenory(_countOfInvenorySlots);

		// 	//_inventoryMenu.SetActive(!_inventoryMenu.activeSelf);

		// 	if (_inventoryMenu.activeSelf)
		// 	{
		// 		Time.timeScale = 0f;
		// 	}
		// 	else
		// 	{
		// 		Time.timeScale = 1f;
		// 	}
		// }

		private void Start()
		{
			PrepareUI();
			PrepareInventoryData();
		}

		public void ShowInventory()
		{
			if (!_inventory.gameObject.activeSelf)
			{
				_inventory.ShowPanel();
				Time.timeScale = 0;

				foreach (var item in _inventoryData.GetCurrentInventoryState())
				{
					_inventory.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Amount);
				}
			}
			else
			{
				_inventory.HidePanel();
				Time.timeScale = 1;
			}
		}

		public void PerformAction(int itemIndex)
		{
			InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);

			if (inventoryItem.IsEmpty)
			{
				return;
			}

			IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;

			if (destroyableItem != null)
				_inventoryData.RemoveItem(itemIndex, 1);

			IItemAction itemAction = inventoryItem.Item as IItemAction;

			if (itemAction != null)
			{
				itemAction.PerformAction(gameObject);

				if (_inventoryData.GetItemAt(itemIndex).IsEmpty)
					_inventory.ResetSelection();
			}

		}

		private void DropItem(int itemIndex, int amount)
		{
			_inventoryData.RemoveItem(itemIndex, amount);
			_inventory.ResetSelection();
		}

		private void PrepareInventoryData()
		{
			_inventoryData.Initialize();
			_inventoryData.OnInventoryUpdated += UpdateInventoryUI;

			foreach (InventoryItem item in initialItems)
			{
				if (item.IsEmpty)
					continue;

				_inventoryData.AddItem(item);
			}
		}

		private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
		{
			_inventory.ResetAllItems();

			foreach (var item in inventoryState)
			{
				_inventory.UpdateData(item.Key, item.Value.Item.ItemImage, item.Value.Amount);
			}
		}

		private void PrepareUI()
		{
			_inventory.InitializeInventory(_inventoryData.Size);
			_inventory.OnDescriptionRequested += HandleDescriptionRequest;
			_inventory.OnItemActionRequested += HandleItemActionRequest;
		}

		private void HandleDescriptionRequest(int itemIndex)
		{
			InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);

			if (inventoryItem.IsEmpty)
			{
				_inventory.ResetSelection();
				return;
			}

			ItemScriptableObject item = inventoryItem.Item;
			_inventory.UpdateDescription(itemIndex, item.ItemImage, item.Name, item.Description);
		}

		private void HandleItemActionRequest(int itemIndex)
		{
			InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);

			if (inventoryItem.IsEmpty)
				return;

			IItemAction itemAction = inventoryItem.Item as IItemAction;

			if (itemAction != null)
			{
				_inventory.ShowItemAction();
				_inventory.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
			}

			IDestroyableItem destroyableItem = inventoryItem.Item as IDestroyableItem;

			if (destroyableItem != null)
			{
				if(itemAction == null)
					_inventory.ShowItemAction();
					
				_inventory.AddAction("Remove", () => DropItem(itemIndex, inventoryItem.Amount));
			}
		}
	}
}
