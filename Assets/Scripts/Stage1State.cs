using UnityEngine;

public class Stage1State : MonoBehaviour
{
	public static Stage1State instance { get; private set; }

	[SerializeField] private Transform[] sublevelDestinations;
	[SerializeField] private TraversalTrigger mainTrigger;
	private void Start() {
		if (instance != null && instance != this) {
			Destroy(this);
		}
		else {
			instance = this;
		}
	}

	public void SetSubLevelDest(int index) {
		mainTrigger.SetNewDestination(sublevelDestinations[index]);
	}
}
