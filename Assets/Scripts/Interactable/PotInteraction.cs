using UnityEngine;

public class PotInteraction : MonoBehaviour
{
    public Transform player;
    public float HeightOffset = 0.6f;
    public float interactionDistance = 2.0f;
    public bool hasFlower = false;
    private SpriteRenderer childSprite;
    void Awake()
    {
        childSprite = GetComponentInChildren<SpriteRenderer>(true);
    }


    void Update()
    {
        // Check if the player is within interaction distance of the pot.
        if (Vector3.Distance(transform.position, player.position) <= interactionDistance)
        {
            // Check for a left mouse click and if the player is holding an item.
            if (Input.GetMouseButtonDown(0) && Inventory.instance.heldItem != null)
            {
                // Verify the held item is the flower.
                if (Inventory.instance.heldItem.GetType() == typeof(flower_item))
                {
                    PlaceFlower();
                }
            }
        }
    }

    void PlaceFlower()
    {
        // Get the held flower from the inventory.
        flower_item flower = (flower_item)Inventory.instance.heldItem;

        Vector3 newPosition = transform.position + new Vector3(0, HeightOffset, 0);
        flower.transform.position = newPosition;
        
        childSprite.gameObject.SetActive(true);
        //flower.gameObject.SetActive(true);
        Inventory.instance.ClearHeldItem();
        hasFlower = true;
        //Debug.Log("Flower Placed SL1");

    }
}