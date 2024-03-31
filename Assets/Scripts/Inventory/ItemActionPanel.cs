using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemActionPanel : MonoBehaviour
{
	[SerializeField] private GameObject _buttonTemplate;
	
	public void AddButton(string name, Action onClickAction)
	{
		GameObject button = Instantiate(_buttonTemplate, gameObject.transform);
		button.GetComponent<Button>().onClick.AddListener(()=>onClickAction());
		button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
	}
	
	public void Toggle(bool value)
	{
		if(value == true)
			RemoveOldButtons();
		
		gameObject.SetActive(value);
	}
	
	public void RemoveOldButtons()
	{
		foreach (Transform tranformChildObject in gameObject.transform)
		{
			Destroy(tranformChildObject.gameObject);
		}
	}
}
