using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private GameObject _enemyTemplate;
	[SerializeField] private Transform[] _spawnPoints;
	[SerializeField] private float _waitTimeToSpawn;
	
	private void Start()
	{
		StartCoroutine(SpawnEnemy());
	}
	
	private IEnumerator SpawnEnemy()
	{
		var timeWait = new WaitForSeconds(_waitTimeToSpawn);
		int randomPoint = Random.Range(0,_spawnPoints.Length);
		
		Instantiate(_enemyTemplate, _spawnPoints[randomPoint].position, Quaternion.identity);
		
		yield return timeWait;
		
		StartCoroutine(SpawnEnemy());
	}
}
