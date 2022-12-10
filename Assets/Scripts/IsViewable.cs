using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsViewable : MonoBehaviour, ILookReceiver {

	bool viewMode = false;
	bool isLookedAt = false;

	Vector3 initialSwipePos;

	GameObject player = null;
	GameObject playerCam = null;
	GameObject viewableCam = null;

	// Start is called before the first frame update
	void Start() {
		Debug.Log("Lallu");

		player = GameObject.FindGameObjectWithTag("Player");
		playerCam = GameObject.FindGameObjectWithTag("PlayerCam");
		viewableCam = GameObject.FindGameObjectWithTag("ProductViewCamera");
	}

	// Update is called once per frame
	void Update() {
		if(isLookedAt) {
			if (!viewMode && Keyboard.current[Key.Space].isPressed) {
				EnableViewMode();	
			}
		}

		if(viewMode) {
			////InputSystem.Mouse
			//if(Input.GetMouseButtonDown(0)) {
			//	initialSwipePos = Input.mousePosition;
			//}
			//else if (Input.GetMouseButton(0)) {
			//	Vector3 deltaPos = initialSwipePos - Input.mousePosition;
			//	transform.rotation *= Quaternion.Euler(deltaPos);

			//}
			
			transform.Rotate(0, 3, 0);
			if (Keyboard.current[Key.Escape].isPressed) {
				DisableViewMode();
			}
		}
	}

	void EnableViewMode() {

		viewMode = true;

		playerCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
		player.GetComponent<PlayerInput>().enabled = false;

		viewableCam.GetComponent<CinemachineVirtualCamera>().LookAt = gameObject.transform;
		viewableCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
		viewableCam.transform.SetPositionAndRotation(playerCam.transform.position, playerCam.transform.rotation);

		Vector3 camPosition = gameObject.transform.position + new Vector3(2f, 0, 0);
		viewableCam.transform.position = Vector3.Lerp(viewableCam.transform.position, camPosition, 2f);
	}

	void DisableViewMode() {
		
		viewMode = false;

		player.GetComponent<PlayerInput>().enabled = true;

		playerCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
		viewableCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
	}

	public void LookingAt() { isLookedAt = true; }
	public void NotLookingAt() { isLookedAt = false; }
}
