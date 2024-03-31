using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
	public class ItemCell : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] private Image _itemImage;
		[SerializeField] TMP_Text _amountText;
		[SerializeField] Image _borderImage;

		public event Action<ItemCell> OnItemClicked, OnRightMouseClick;

		private void Awake()
		{
			ResetData();
			Deselect();
		}

		public void ResetData()
		{
			_itemImage.gameObject.SetActive(false);
		}

		public void Deselect()
		{
			_borderImage.enabled = false;
		}

		public void Select()
		{
			_borderImage.enabled = true;
		}

		public void SetData(Sprite sprite, int amount)
		{
			_itemImage.gameObject.SetActive(true);
			_itemImage.sprite = sprite;
			_amountText.text = amount.ToString();
			
			if(amount > 1)
			{
				_amountText.enabled = true;
				_amountText.text = amount.ToString();
			}
			else
			{
				_amountText.enabled = false;	
			}
		}

	   public void OnPointerClick(PointerEventData pointerData)
		{
			if (pointerData.button == PointerEventData.InputButton.Right)
			{
				OnRightMouseClick?.Invoke(this);
			}
			else
			{
				OnItemClicked?.Invoke(this);
			}
		}
	}
}