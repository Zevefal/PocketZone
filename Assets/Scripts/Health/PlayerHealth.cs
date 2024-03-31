using UnityEngine;

public class PlayerHealth : Health
{
	public float CurrentHealth => _currentHealth;
	public float MaxHealth => _maxHealth;
	
	private void Start()
	{
		_currentHealth = _maxHealth;

		HealthChanged?.Invoke(_currentHealth);
	}

	public override void TakeDamage(int damage)
	{
		_currentHealth -= damage;

		float healthPercent = _currentHealth / _maxHealth;
		HealthChanged?.Invoke(healthPercent);

		if (_currentHealth <= 0)
		{
			Destroy(gameObject);
		}
	}

	public void Heal(int amount)
	{
		_currentHealth += amount;

		if (_currentHealth >= _maxHealth)
			_currentHealth = _maxHealth;
		
		float healthPercent = _currentHealth/_maxHealth;
		HealthChanged?.Invoke(healthPercent);
	}
	
	public void SetHealth(float value)
	{
		_currentHealth = value;
		HealthChanged?.Invoke(_currentHealth);
	}
}