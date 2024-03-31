using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private PlayerHealth _player;
	[SerializeField] private float _speed;
	[SerializeField] private float _detectionRadius;
	[SerializeField] private int _attackDamage = 5;
	[SerializeField] private float _attackDistance = 1.5f;
	[SerializeField] private float _timeBetweenAttack = 2f;
	
	private float _nextAttackTime = 0f;
	
	private bool _isFacingRight = true;
	
	private void Start()
	{
		_player = FindObjectOfType<PlayerHealth>();
	}
	
	private void Update()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
		
		if(distanceToPlayer <= _detectionRadius)
		{
			Vector3 directionMove = (_player.transform.position - transform.position).normalized;
			Flip(directionMove);
			
			if(distanceToPlayer > _attackDistance)
			{
				transform.Translate(directionMove * _speed * Time.deltaTime);
			}
			else 
			{
				if(Time.time >= _nextAttackTime)
				{
					_player.TakeDamage(_attackDamage);
					_nextAttackTime = Time.time + 1f/_timeBetweenAttack;
				}
			}
		}
	}
	
	private void Flip(Vector3 direction)
	{
		if(_isFacingRight && direction.x < 0f || !_isFacingRight && direction.x > 0f)
		{
			_isFacingRight = !_isFacingRight;
			Vector3 localScale = transform.localScale;
			localScale.x *= -1f;
			transform.localScale = localScale;
		}
	}	
}
