using UnityEngine;

public class ItemGrabber : MonoBehaviour {
	private Item currentHoveredItem;
	[SerializeField] private Inventory inventory;
	[SerializeField] private GameObject blankItem;

	public void PickupItem(Item item) {
		print($"pickedup {item}");

		if (!inventory.heldItem) {
			inventory.ChangeHeldItem(item);
		}
		else {
			DropItem();

			inventory.ChangeHeldItem(item);
		}

		item.gameObject.SetActive(false);
	}

	public void DropItem() {
		inventory.heldItem.gameObject.SetActive(true);
		inventory.heldItem.transform.SetPositionAndRotation(transform.position, transform.rotation);

		inventory.ClearHeldItem();

		print("dropped item!");
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
			PickupItem(currentHoveredItem);
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			DropItem();
		}
	}

	private void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Item")) {
			print("test! Item here!");
			print(collider);

			currentHoveredItem = collider.GetComponent<Item>();
		}
	}

	private void OnTriggerExit(Collider collider) {
		if (collider.GetComponent<Item>() == currentHoveredItem) {
			currentHoveredItem = null;
			print("Item gone!");
		}
	}
}
