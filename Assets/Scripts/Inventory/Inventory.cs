using MADCUP.STM;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	public static Inventory instance { get; private set; }

	[Header("UI Elements")]
	[SerializeField] private Image heldItemImage;

	public Item heldItem { get; private set; }

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
	}

	public void SetHeldItem(Item item)
	{
		heldItem = item;

		SpriteMesh spriteMesh = item.GetComponent<SpriteMesh>();

		heldItemImage.sprite = spriteMesh.sprite;
		heldItemImage.enabled = true;
	}

	public void ClearHeldItem()
	{
		heldItem = null;
		heldItemImage.sprite = null;
		heldItemImage.enabled = false;
	}
}
