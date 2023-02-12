using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Minimap : MonoBehaviour {
	// Start is called before the first frame update

	UIManager UI;
	public GameObject minimapTexture;
	public GameObject minimapMask;
	public GameObject fader;
	bool isFullscreen = false;

	void Start() {
		UI = UIManager.Instance;
	}

	// Update is called once per frame
	void Update() {
		Vector3 targetPos = UI.minimapFollowTarget.transform.position;
		transform.position = new Vector3(targetPos.x, targetPos.y + UI.minimapHeight, targetPos.z);

		if (UI.minimapRotate) {
			Quaternion targetRot = UI.MainCamera.transform.rotation;

			transform.rotation = Quaternion.Euler(90, targetRot.eulerAngles.y, 0);
		}

		if (!isFullscreen && Keyboard.current[Key.M].isPressed) {
			FullscreenMap();
		}

		if (isFullscreen && (Keyboard.current[Key.Escape].isPressed)) {
			CloseFullscreenMap();
		}
	}
	public void FullscreenMap() {
		minimapMask.GetComponent<Image>().enabled = false;
		isFullscreen = true;

		RectTransform rTransform = minimapMask.GetComponent<RectTransform>();
		RectTransform rTransformTexture = minimapTexture.GetComponent<RectTransform>();
		Utils.SetPivot(rTransform, new Vector2(0.5f, 0.5f));

		Vector3 pos = rTransform.localPosition;
		rTransform.anchorMin = new Vector2(0.5f, 0.5f);
		rTransform.anchorMax = new Vector2(0.5f, 0.5f);
		rTransform.localPosition = pos;

		StartCoroutine(Utils.Lerp2DPosition(rTransform, rTransform.anchoredPosition, new Vector2(0, 0), 0.5f));
		StartCoroutine(Utils.LerpImageAlpha(fader.GetComponent<Image>(), 0, 0.5f, 0.5f));
		StartCoroutine(Utils.LerpRectSize(rTransformTexture, rTransformTexture.sizeDelta, new Vector2(400, 400), 0.5f));
	}

	public void CloseFullscreenMap() {
		minimapTexture.GetComponentInParent<Image>(true).enabled = true;
		isFullscreen = false;

		RectTransform rTransform = minimapMask.GetComponent<RectTransform>();
		RectTransform rTransformTexture = minimapTexture.GetComponent<RectTransform>();
		Utils.SetPivot(rTransform, new Vector2(0, 0));

		Vector3 pos = rTransform.localPosition;
		rTransform.anchorMin = new Vector2(0, 0);
		rTransform.anchorMax = new Vector2(0, 0);
		rTransform.localPosition = pos;

 		StartCoroutine(Utils.LerpRectSize(rTransformTexture, rTransformTexture.sizeDelta, new Vector2(100, 100), 0.5f));
		StartCoroutine(Utils.Lerp2DPosition(rTransform, rTransform.anchoredPosition, new Vector2(20, 20), 0.5f));
		StartCoroutine(Utils.LerpImageAlpha(fader.GetComponent<Image>(), 0.5f, 0, 0.5f));
	}

	
}