using UnityEngine;
using UnityEngine.UI;

public class DrawHealth : MonoBehaviour
{
	[SerializeField] private Health _health;
	[SerializeField] private Slider _healthBar;
	
	private void OnEnable()
	{
		_health.HealthChanged += UpdateHealthbar;
	}
	
	private void OnDestroy()
	{
		_health.HealthChanged -= UpdateHealthbar;
	}
	
	private void UpdateHealthbar(float health)
	{
		_healthBar.value = health;
	}
}
