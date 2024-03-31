using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
	public class InventoryUI : MonoBehaviour
	{
		[SerializeField] private RectTransform _contentPanel;
		[SerializeField] private ItemCell _itemSlotTemplate;
		[SerializeField] private ItemCellDescription _itemCellDescription;
		[SerializeField] private ItemActionPanel _actionPanel;

		private List<ItemCell> _listOfItemCells = new List<ItemCell>();

		public event Action<int> OnDescriptionRequested, OnItemActionRequested;

		private void Awake()
		{
			_itemCellDescription.ResetDescription();
		}

		public void InitializeInventory(int inventorySize)
		{
			for (int i = 0; i < inventorySize; i++)
			{
				ItemCell itemCell = Instantiate(_itemSlotTemplate, _contentPanel);
				itemCell.transform.SetParent(_contentPanel);
				_listOfItemCells.Add(itemCell);
				itemCell.OnItemClicked += HandleItemSelection;
				itemCell.OnItemClicked += HandleShowItemActions;
			}
		}

		public void UpdateData(int itemIndex, Sprite itemImage, int itemAmount)
		{
			if (_listOfItemCells.Count > itemIndex)
			{
				_listOfItemCells[itemIndex].SetData(itemImage, itemAmount);
			}
		}

		public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string itemDescription)
		{
			_itemCellDescription.SetDescription(itemImage, name, itemDescription);
			DeselectAllItems();
			_listOfItemCells[itemIndex].Select();
		}

		public void ShowPanel()
		{
			gameObject.SetActive(true);
			ResetSelection();
		}

		public void ResetAllItems()
		{
			foreach (var item in _listOfItemCells)
			{
				item.ResetData();
				item.Deselect();
			}
		}

		public void ResetSelection()
		{
			_itemCellDescription.ResetDescription();
			DeselectAllItems();
		}

		public void AddAction(string ActionName, Action performAction)
		{
			_actionPanel.AddButton(ActionName, performAction);
		}
		
		public void ShowItemAction()
		{
			_actionPanel.Toggle(true);
		}
		
		public void HidePanel()
		{
			_actionPanel.Toggle(false);
			gameObject.SetActive(false);
			ResetSelection();
		}

		private void HandleItemSelection(ItemCell cell)
		{
			int index = _listOfItemCells.IndexOf(cell);

			if (index == -1)
				return;

			OnDescriptionRequested?.Invoke(index);
		}
		
		private void HandleShowItemActions(ItemCell itemCell)
		{
			int index = _listOfItemCells.IndexOf(itemCell);

			if (index == -1)
				return;
				
			OnItemActionRequested?.Invoke(index);
		}

		private void DeselectAllItems()
		{
			foreach (ItemCell item in _listOfItemCells)
			{
				item.Deselect();
			}
			
			_actionPanel.Toggle(false);
		}
	}
}
