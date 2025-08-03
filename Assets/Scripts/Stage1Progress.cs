using UnityEngine;

public class Stage1Progress : MonoBehaviour
{
	[SerializeField] private int newSublevelIndex = 0;
	private void OnTriggerEnter(Collider other) {
		Stage1State.instance.SetSubLevelDest(newSublevelIndex);
	}
}
