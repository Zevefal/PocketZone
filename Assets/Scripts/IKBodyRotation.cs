using UnityEngine;

public class IKBodyRotation : MonoBehaviour
{
	[SerializeField] private Joystick _joystick;
	public Transform aimTarget;
	public Transform body;
	public Transform weapon;

	private bool _isFacingRight = true;

	void Update()
	{
		// Направление к прицелу
		Vector2 direction = aimTarget.position - body.transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		// Поворот тела
		body.rotation = Quaternion.Euler(0, 0, angle + 90);

		// Поворот оружия
		weapon.rotation = Quaternion.Euler(0, 0, angle);
		
		Flip();
	}

	private void Flip()
	{
		if (_isFacingRight && _joystick.Horizontal < 0f || !_isFacingRight && _joystick.Horizontal > 0f)
		{
			_isFacingRight = !_isFacingRight;
			Vector3 localScale = transform.localScale;
			Vector3 weaponLocalScale = weapon.localScale;
			localScale.x *= -1f;
			weaponLocalScale.x *=-1f;
			weaponLocalScale.y *=-1f;
			transform.localScale = localScale;
			weapon.localScale = weaponLocalScale;
		}
	}
}