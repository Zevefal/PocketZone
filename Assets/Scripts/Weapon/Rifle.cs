using UnityEngine;

public class Rifle : Weapon
{
	public override void Shoot(Transform shootingPoint, Vector2 shootingDirection)
	{
		var bullet = Instantiate(_bullet, shootingPoint.position, Quaternion.identity);
		float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
		bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		bullet.MoveBullet(shootingDirection);
	}
}
