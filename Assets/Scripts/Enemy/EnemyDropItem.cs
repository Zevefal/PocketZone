using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
	[SerializeField] private EnemyHealth _enemyHealth;
	[SerializeField] private GameObject[] _items;
	
	private void OnEnable()
	{
		_enemyHealth.OnEnemyDie += DropItem;
	}
	
	private void OnDisable()
	{
		_enemyHealth.OnEnemyDie -= DropItem;
	}	
	
	private void DropItem()
	{
		int randomItem = Random.Range(0, _items.Length);
		
		Instantiate(_items[randomItem],transform.position,Quaternion.identity);
	}
}
