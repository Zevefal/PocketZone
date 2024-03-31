using Inventory.Model;
using UnityEngine;

public class AutomaticShooting : MonoBehaviour
{
	[SerializeField] private InventoryScriptableObject _inventoryData;
	[SerializeField] private EnemyDetection _enemyDetection;
	[SerializeField] private Transform _player;
	[SerializeField] private Transform _crosshair;
	[SerializeField] private float _radius = 4f;
	[SerializeField] private Weapon _weapon;
	[SerializeField] private Transform firePoint;
	[SerializeField] private float _shootingCooldown = 1f;
	
	private float _lastShootTime;

	private void Update()
	{
		Transform detectedEnemy = _enemyDetection.GetDetectedEnemy();

		if (detectedEnemy != null)
		{
			Vector2 direction = detectedEnemy.position - _player.position;

			MoveAim(direction);
			FindAmmoToShooting(direction);
		}
	}

	private void FindAmmoToShooting(Vector2 direction)
	{
		int itemIndex = _inventoryData.FindItemInInvenory(_weapon.Ammo);
		InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);

		if (inventoryItem.IsEmpty)
		{
			return;
		}
		else
		{
			FireAtEnemy(direction, itemIndex);
		}
	}

	private void MoveAim(Vector2 direction)
	{
		float angle = Mathf.Atan2(direction.y, direction.x);
		Vector2 targetPosition = new Vector2(_player.position.x + Mathf.Cos(angle) * _radius, _player.position.y + Mathf.Sin(angle) * _radius);
		_crosshair.transform.position = new Vector3(targetPosition.x, targetPosition.y, 0);
	}

	private void FireAtEnemy(Vector2 direction, int itemIndex)
	{
		if (Time.time -_lastShootTime > _shootingCooldown)
		{
			_weapon.Shoot(firePoint, direction);
			_inventoryData.RemoveItem(itemIndex, 1);
			_lastShootTime = Time.time;
		}
	}
}
