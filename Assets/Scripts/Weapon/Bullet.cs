using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rigidbody;
	[SerializeField] private int _damage;
	[SerializeField] private float _speed;
	
	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	private void Update()
	{
		Destroy(gameObject, 3f);
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
		{
			enemy.TakeDamage(_damage);
			Destroy(gameObject);
		}
	}
	
	public void MoveBullet(Vector2 direction)
	{
		_rigidbody.velocity = direction * _speed;
	}
}
