using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShootButtonClick : MonoBehaviour
{	
	private Button _button;
	
	public Action IsButtonClicked;
	
	private void OnEnable()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(OnShootButtonClick);
	}
	
	private void OnDisable()
	{
		_button.onClick.RemoveListener(OnShootButtonClick);
	}
	
	private void OnShootButtonClick()
	{
		IsButtonClicked?.Invoke();
	}
}
