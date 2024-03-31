using UnityEngine;
using Inventory.Model;

public class Item : MonoBehaviour
{
	[field: SerializeField] public ItemScriptableObject InventoryItem {get; private set;}
	[field: SerializeField] public int Amount {get; set;} = 1;
	
	// [SerializeField] private AudioSource _audioSource;
	// [SerializeField] private float _duration = 0.3f;
	
	 private void Start()
	 {
		GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
	 }
	 
	 public void DestroyItem()
	 {
	 	Destroy(gameObject);
	 }
}
