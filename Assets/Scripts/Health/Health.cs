using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
	[SerializeField] protected float _maxHealth;
	
	protected float _currentHealth;
	
	public Action<float> HealthChanged;
	
	public abstract void TakeDamage(int damage);
}
