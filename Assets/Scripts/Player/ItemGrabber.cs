using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private GameObject blankItem;

	private Item currentHoveredItem;

	public void PickupItem(Item item)
	{
		if (!inventory.heldItem)
		{
			inventory.ChangeHeldItem(item);
		}
		else
		{
			DropItem();

			inventory.ChangeHeldItem(item);
		}

		item.gameObject.SetActive(false);
	}

	public void DropItem()
	{
		inventory.heldItem.gameObject.SetActive(true);
		inventory.heldItem.transform.SetPositionAndRotation(transform.position, transform.rotation);

		inventory.ClearHeldItem();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			PickupItem(currentHoveredItem);
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			DropItem();
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.CompareTag("Item"))
		{
			currentHoveredItem = collider.GetComponent<Item>();
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (collider.GetComponent<Item>() == currentHoveredItem)
		{
			currentHoveredItem = null;
		}
	}
}
