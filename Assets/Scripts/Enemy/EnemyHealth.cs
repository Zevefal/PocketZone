using System;
using UnityEngine;

public class EnemyHealth : Health
{
	public event Action OnEnemyDie;
	
	private void Start()
	{
		_currentHealth = _maxHealth;
		
		HealthChanged?.Invoke(_currentHealth);
	}
	private void Update()
	{
		if(Input.GetMouseButtonDown(1))
		{
			TakeDamage(10);
		}
	}
	
	public override void TakeDamage(int damage)
	{
		_currentHealth -= damage;
		
		float healthPercent = _currentHealth/_maxHealth;
		HealthChanged?.Invoke(healthPercent);

		if(_currentHealth <= 0)
		{
			Die();
		}
	}
	
	private void Die()
	{
		HealthChanged?.Invoke(0);
		OnEnemyDie?.Invoke();
		Destroy(gameObject);
	}
}
