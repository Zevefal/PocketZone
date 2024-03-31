using Inventory.Model;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
	[SerializeField] private InventoryScriptableObject _inventory;
	[SerializeField] private Weapon _weapon;
	[SerializeField] private Transform _shootingPoint;
	[SerializeField] private Transform _crosshair;
	[SerializeField] private Button _shootButton;

	private void OnEnable()
	{
		_shootButton.onClick.AddListener(Shooting);
	}
	
	private void OnDisable()
	{
		_shootButton.onClick.RemoveListener(Shooting);
	}
	
	private void Shooting()
	{
		Vector2 shootingDirection = (_crosshair.position - _shootingPoint.position).normalized;
		
		int itemIndex = _inventory.FindItemInInvenory(_weapon.Ammo);
		InventoryItem inventoryItem = _inventory.GetItemAt(itemIndex);

		if (inventoryItem.IsEmpty)
		{
			return;
		}
		else
		{
			Shoot(shootingDirection, itemIndex);
		}
	}
	
	public void Shoot(Vector2 direction, int itemIndex)
	{
		_inventory.RemoveItem(itemIndex, 1);
		_weapon.Shoot(_shootingPoint, direction);
	}
}
