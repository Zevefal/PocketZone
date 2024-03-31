using System;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Edible Item")]
public class EdibleitemScriptableObject : ItemScriptableObject, IDestroyableItem, IItemAction
{
	[SerializeField] private List<ModifierData> _modifiersData = new List<ModifierData>();
	
	public string ActionName => "Consume";
	public AudioClip actionSFX {get; private set;}
	
	public bool PerformAction(GameObject character)
	{
		foreach (ModifierData data in _modifiersData)
		{
			data.StatModifier.AffectCharacter(character, data.Value);
		}
		
		return true;
	}
}

public interface IDestroyableItem
{
	
}

public interface IItemAction
{
	public string ActionName {get;}
	public AudioClip actionSFX {get;}
	bool PerformAction(GameObject character);
}

[Serializable]
public class ModifierData
{
	public CharacterStatModifier StatModifier;
	public float Value;
}
