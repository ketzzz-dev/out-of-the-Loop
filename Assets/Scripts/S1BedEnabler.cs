using UnityEngine;

public class S1BedEnabler : MonoBehaviour
{
	[SerializeField] private Bed bed;

	private void OnTriggerEnter(Collider other) {
		bed.enabled = true;
	}
}
