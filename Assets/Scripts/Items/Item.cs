using MADCUP.STM;
using UnityEngine;

public enum ItemType {
	None,
	Placeholder,
	Placeholder2,
	// Rest of the other items here
}

public class Item : MonoBehaviour {
	public ItemType itemType;
	public Sprite itemSprite;
	public bool initalized;
	[SerializeField] private SpriteMesh spriteMesh;

	private void Start() {
		if (itemType != ItemType.None) {
			spriteMesh.sprite = itemSprite;
			initalized = true;
		}
	}

	// This is used for dropped items.
	public void Initialize(Sprite sprite, ItemType type) {
		initalized = true;
		itemSprite = sprite;
		itemType = type;

		spriteMesh.sprite = sprite;
	}
}
