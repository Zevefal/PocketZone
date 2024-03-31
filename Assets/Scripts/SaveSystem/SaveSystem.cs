using System.Collections.Generic;
using System.IO;
using Inventory.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
	private const string SaveFilePath = "/savegame.dat";

	[SerializeField] private InventoryScriptableObject _inventory;
	[SerializeField] private PlayerHealth _playerHealth;

	private SaveData _saveData;

	public void SaveGame()
	{
		List<InventoryItem> itemList = _inventory.GetAllItems();
		_saveData = new SaveData(_playerHealth.CurrentHealth, itemList, _playerHealth.gameObject.transform.position.x, _playerHealth.transform.position.y);
		SaveData(_saveData);
		Debug.Log("Сохранили");
	}

	public void LoadGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		_saveData = LoadData();
		_playerHealth.SetHealth(_saveData.PlayerHealth);
		_inventory.Initialize();

		for (int i = 0; i < _saveData.Items.Count; i++)
		{
			_inventory.AddItem(_saveData.Items[i]);
		}
		
		_playerHealth.gameObject.transform.position = new Vector2(_saveData.PlayerPositionX, _saveData.PlayerPositionY);
		
		Debug.Log("Загрузили");
	}

	private void SaveData(SaveData saveData)
	{
		string jsonData = JsonUtility.ToJson(saveData);
		File.WriteAllText(SaveFilePath, jsonData);
	}

	private SaveData LoadData()
	{
		if (File.Exists(SaveFilePath))
		{
			string jsonData = File.ReadAllText(SaveFilePath);

			SaveData loadedData = JsonUtility.FromJson<SaveData>(jsonData);

			return loadedData;
		}
		else
		{
			return null;
		}
	}
}