using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Modifiers/Health")]
public class CharacterHealthModifier : CharacterStatModifier
{
	public override void AffectCharacter(GameObject character, float value)
	{
		PlayerHealth health = character.GetComponent<PlayerHealth>();

		if (health != null)
			health.Heal((int)value);
	}

}
