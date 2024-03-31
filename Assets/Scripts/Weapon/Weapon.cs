using UnityEngine;
using Inventory.Model;

public abstract class Weapon : MonoBehaviour
{
	[SerializeField] protected Bullet _bullet;
	[SerializeField] protected ItemScriptableObject _ammo;

	public ItemScriptableObject Ammo => _ammo;

	public abstract void Shoot(Transform shootingPoint, Vector2 shootingDirection);
}
