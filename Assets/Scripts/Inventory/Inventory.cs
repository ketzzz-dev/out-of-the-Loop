using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public Item heldItem;
	[Header("UI Elements")]
	[SerializeField] private Image heldItemImage;

	private void Start() {
		
	}

	public void ChangeHeldItem(Item item) {
		heldItem = item;
		heldItemImage.sprite = heldItem.itemSprite;
	}

	public void ClearHeldItem() {
		heldItemImage.sprite = null;
		heldItem = null;
	}
}
