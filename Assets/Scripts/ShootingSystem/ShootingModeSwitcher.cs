using UnityEngine;
using UnityEngine.UI;

public class ShootingModeSwitcher : MonoBehaviour
{
	[SerializeField] private PlayerShooting _playerShooting;
	[SerializeField] private AutomaticShooting _automaticShooting;
	[SerializeField] private Toggle _shootingSwitcher;

	private void OnEnable()
	{
		_shootingSwitcher.onValueChanged.AddListener(ModeSwitch);
	}
	
	private void OnDisable()
	{
		_shootingSwitcher.onValueChanged.RemoveListener(ModeSwitch);
	}

	private void ModeSwitch(bool value)
	{
		if (value == true)
		{
			_automaticShooting.enabled = true;
			_playerShooting.enabled = false;
		}
		else
		{
			_automaticShooting.enabled = false;
			_playerShooting.enabled = true;
		}
	}
}
