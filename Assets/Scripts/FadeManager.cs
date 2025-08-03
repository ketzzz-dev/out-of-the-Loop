using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {
	public static FadeManager instance { get; private set; }

	[SerializeField] private GameObject fadeObject;

	private void Start() {
		if (instance != null && instance != this) {
			Destroy(this);
		}
		else {
			instance = this;
		}
	}

	public void StartFadeIn(Color color, float fadeTime) {
		print("starting fade in!");
		StartCoroutine(Fade(fadeTime, true, color));
	}

	public void StartFadeOut(Color color, float fadeTime) {
		print("starting fade out!");
		StartCoroutine(Fade(fadeTime, false, color));
	}

	private IEnumerator Fade(float fadeTime, bool fadeType, Color color) {
		Image image = fadeObject.GetComponent<Image>();
		float currentAlpha = image.color.a;
		// Fade in
		if (fadeType) {
			currentAlpha = 0;
			fadeObject.SetActive(true);
			while (currentAlpha < 1) {
				currentAlpha += 0.01f;
				image.color = new Color(color.r, color.g, color.b, currentAlpha);
				yield return new WaitForSecondsRealtime(fadeTime * Time.deltaTime);
			}
		}
		// Fade out
		else {
			currentAlpha = 1;
			while (currentAlpha > 0) {
				currentAlpha -= 0.01f;
				image.color = new Color(color.r, color.g, color.b, currentAlpha);
				yield return new WaitForSecondsRealtime(fadeTime * Time.deltaTime);
			}
			fadeObject.SetActive(false);
		}

		print("end fade!");
	}
}
