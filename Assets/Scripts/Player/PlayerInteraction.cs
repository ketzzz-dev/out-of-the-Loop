using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private LayerMask interactableLayer;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !DialogueManager.instance.isActive)
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out RaycastHit hit, 25f, interactableLayer))
        {
			print(hit.transform); 
			print(transform);
			print(transform.position);
			print(hit.transform.position);
            float distance = Vector3.Distance(hit.transform.position, transform.position);
			print("atempt t spleep");

            if (distance > interactionRange)
			{
				print(distance);
				print("toofar");
				return;
            }

            Interactable interactable = hit.collider.GetComponent<Interactable>();

            interactable?.Interact();
        }
    }
}