using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
	[SerializeField] private Inventory inventory;
	[SerializeField] private GameObject blankItem;
	[SerializeField] private Transform potTransform;
	[SerializeField] private float dropDistance = 2f;

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
    // Check if no item is held, and exit early.
    if (inventory.heldItem == null)
    {
        return;
    }

    Vector3 dropPosition;
    Quaternion dropRotation = inventory.heldItem.transform.rotation;

    // Check if the held item is a "Flower" AND the player is close to the pot.
    bool isFlowerAndNearPot = inventory.heldItem.CompareTag("flower") && 
                              potTransform != null &&
                              Vector3.Distance(transform.position, potTransform.position) <= dropDistance;

    if (isFlowerAndNearPot)
    {
        // Drop the flower on top of the pot.
        dropPosition = potTransform.position + new Vector3(0, 0.5f, 0);
    }
    else
    {
        // Set the drop position to be in front of the player (normal drop).
        dropPosition = transform.position;
    }

    // Make the held item visible and set its position and rotation.
    inventory.heldItem.gameObject.SetActive(true);
    inventory.heldItem.transform.SetPositionAndRotation(dropPosition, dropRotation);

    // Clear the held item from the inventory.
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
		if (collider.CompareTag("Item") || collider.CompareTag("flower"))
		{
			currentHoveredItem = collider.GetComponent<Item>();
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		// Check for both tags here as well
		if (collider.CompareTag("Item") || collider.CompareTag("flower"))
		{
			if (collider.GetComponent<Item>() == currentHoveredItem)
			{
				currentHoveredItem = null;
			}
		}
	}
}
