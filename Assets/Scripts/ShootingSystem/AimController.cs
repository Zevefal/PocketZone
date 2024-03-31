using UnityEngine;

public class AimController : MonoBehaviour
{
	[SerializeField] private Joystick _joystick;
	[SerializeField] private Transform _player;
	[SerializeField] private float _radius = 3f;
	
	private Vector2 _center;
	private float _angle;
	
	public float Radius => _radius;

	private void Update()
	{
		float inputX = _joystick.Horizontal;
		float inputY = _joystick.Vertical;
		_center = _player.position;

		Vector2 inputVector = new Vector2(inputX, inputY).normalized;
		
		if(inputVector != Vector2.zero)
		{
			_angle = Mathf.Atan2(inputVector.x, inputVector.y);
			transform.position = _center + new Vector2(Mathf.Sin(_angle)*_radius, Mathf.Cos(_angle)*_radius);
		}
	}
}
