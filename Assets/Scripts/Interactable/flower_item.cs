public class flower_item : Item
{
	public override void Interact()
	{
		Inventory.instance.SetHeldItem(this);
		gameObject.SetActive(false);

		base.Interact();
	}
}
