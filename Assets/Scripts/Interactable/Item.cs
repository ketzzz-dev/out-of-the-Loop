public class Item : Interactable
{
	public override void Interact()
	{
		Inventory.instance.SetHeldItem(this);
		gameObject.SetActive(false);
	}
}
	