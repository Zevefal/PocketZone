using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Vector3 _offset;
	[SerializeField] private float _damping;
	[SerializeField] private Transform _target;
	
	private Vector3 velocity = Vector3.zero;
	
	private void FixedUpdate()
	{
		Vector3 targetPosition = _target.position + _offset;
		targetPosition.z = transform.position.z;
		
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _damping);
	}
}
