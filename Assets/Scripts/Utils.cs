using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Utils {
	
	public static void SetPivot(RectTransform rectTransform, Vector2 pivot) {
		Vector3 deltaPosition = rectTransform.pivot - pivot;
		deltaPosition.Scale(rectTransform.rect.size);
		deltaPosition.Scale(rectTransform.localScale);
		deltaPosition = rectTransform.rotation * deltaPosition;

		rectTransform.pivot = pivot;
		rectTransform.localPosition -= deltaPosition;
	}

	public static IEnumerator LerpScale(Transform transform, float currentScale, float endValue, float duration) {
		float time = 0;
		float startValue = currentScale;
		Vector3 startScale = transform.localScale;
		while (time < duration) {
			currentScale = Mathf.Lerp(startValue, endValue, time / duration);
			transform.localScale = startScale * currentScale;
			time += Time.deltaTime;
			yield return null;
		}

		transform.localScale = startScale * endValue;
	}

	public static IEnumerator LerpImageAlpha(Image image, float current, float target, float duration) {
		float time = 0;
		float startValue = current;
		float startScale = image.color.a;
		while (time < duration) {
			current = Mathf.Lerp(startValue, target, time / duration);
			image.color = new Color(image.color.r, image.color.g, image.color.b, current);
			time += Time.deltaTime;
			yield return null;
		}

		image.color = new Color(image.color.r, image.color.g, image.color.b, target);
	}

	public static IEnumerator Lerp2DPosition(RectTransform rect, Vector2 startPosition, Vector2 targetPosition, float duration) {
		float time = 0;
		while (time < duration) {
			rect.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		rect.anchoredPosition = targetPosition;
	}


	public static IEnumerator LerpRectSize(RectTransform rect, Vector2 start, Vector2 target, float duration) {
		float time = 0;
		while (time < duration) {
			rect.sizeDelta = Vector2.Lerp(start, target, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		rect.sizeDelta = target;
	}

	public static IEnumerator Lerp3DPosition(Transform transform, Vector3 startPosition, Vector3 targetPosition, float duration) {
		float time = 0;
		while (time < duration) {
			transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		transform.position = targetPosition;
	}

}
