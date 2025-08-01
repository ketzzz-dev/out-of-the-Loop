using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	[Header("UI Elements")]
	[SerializeField] private Image heldItemImage;

	public Item heldItem;

	public void ChangeHeldItem(Item item)
	{
		heldItem = item;
		heldItemImage.sprite = heldItem.itemSprite;
	}

	public void ClearHeldItem()
	{
		heldItemImage.sprite = null;
		heldItem = null;
	}
}
