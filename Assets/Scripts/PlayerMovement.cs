using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private Joystick _joystick;
	[SerializeField] private Transform _player;
	[SerializeField] private float _playerSpeed = 3.0f;

	private void Update()
	{
		float moveX = _joystick.Horizontal;
		float moveY = _joystick.Vertical;
		
		Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
		_player.transform.Translate(moveDirection * _playerSpeed * Time.deltaTime);
	}
}
