using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
	[SerializeField] private AimController _aim;
	[SerializeField] private LayerMask _enemyLayer;

	private Transform _detectedEnemy;

	private void Update()
	{
		DetectEnemyInRaidus();
	}

	public Transform GetDetectedEnemy()
	{
		return _detectedEnemy;
	}

	private void DetectEnemyInRaidus()
	{
		Collider2D[] hitColleders = Physics2D.OverlapCircleAll(transform.position, _aim.Radius, _enemyLayer);

		if (hitColleders.Length > 0)
		{
			_detectedEnemy = hitColleders[0].transform;
		}
		else
		{
			_detectedEnemy = null;
		}
	}
}
