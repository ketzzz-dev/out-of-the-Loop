using System.Collections;
using UnityEngine;

public class TraversalTrigger : MonoBehaviour
{
	[SerializeField] private Transform newDestPos;
	private void OnTriggerEnter(Collider other) {
		if (newDestPos != null) {
			StartCoroutine(TraverseRoom(other.gameObject));
		}
		else {
			Debug.LogWarning("No set destination! Set one up!");
		}
	}

	private IEnumerator TraverseRoom(GameObject player) {
		PlayerMovement.instance.freezeMovement = true;

		FadeManager.instance.StartFadeIn(new Color(0, 0, 0), 2f);

		yield return new WaitForSecondsRealtime(2.5f);

		player.transform.position = newDestPos.position;

		PlayerMovement.instance.freezeMovement = false;
		FadeManager.instance.StartFadeOut(new Color(0, 0, 0), 2f);
	}

	public void SetNewDestination(Transform newDest) {
		newDestPos = newDest;
	}
}
