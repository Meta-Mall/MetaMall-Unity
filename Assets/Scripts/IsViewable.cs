using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsViewable : MonoBehaviour, ILookReceiver {

	bool viewMode = false;
	bool isLookedAt = false;
	bool isSwiping = false;
	Vector3 mousePos;

	GameObject player = null;
	GameObject playerCam = null;
	GameObject viewableCam = null;

	public float swipeSensivity = 2;
	public Vector3 idleRotationSpeed = new Vector3(0, 2, 0);


	// Start is called before the first frame update
	void Start() {
		Debug.Log("Lallu");

		player = GameObject.FindGameObjectWithTag("Player");
		playerCam = GameObject.FindGameObjectWithTag("PlayerCam");
		viewableCam = GameObject.FindGameObjectWithTag("ProductViewCamera");
	}

	// Update is called once per frame
	void Update() {

		if (isLookedAt) {
			if (!viewMode && Keyboard.current[Key.E].isPressed) {
				EnableViewMode();
			}
		}

		if (viewMode) {
			
			if (Input.GetMouseButtonDown(0)) {
				mousePos = Input.mousePosition;
				isSwiping = true;
			}
			else if (Input.GetMouseButton(0)) {
				Vector3 deltaMouse = (mousePos - Input.mousePosition) * swipeSensivity;
				Vector3 currentAngle = transform.eulerAngles;
				transform.eulerAngles = new Vector3(currentAngle.x, currentAngle.y + deltaMouse.x, currentAngle.z + deltaMouse.y);
				
				mousePos = Input.mousePosition;
			}
			else if (Input.GetMouseButtonUp(0)) {
				isSwiping = false;
			}

			if (!isSwiping) {
				transform.eulerAngles += idleRotationSpeed;
				transform.eulerAngles = new Vector3(
					Mathf.LerpAngle(transform.eulerAngles.x, 0, 0.1f),
					transform.eulerAngles.y,
					Mathf.LerpAngle(transform.eulerAngles.z, 0, 0.1f)
				);
			}


			if (Keyboard.current[Key.Escape].isPressed) {
				DisableViewMode();
			}

			
		}
	}

	void EnableViewMode() {

		viewMode = true;
		
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;

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
		
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		player.GetComponent<PlayerInput>().enabled = true;

		playerCam.GetComponent<CinemachineVirtualCamera>().enabled = true;
		viewableCam.GetComponent<CinemachineVirtualCamera>().enabled = false;
	}

	public void LookingAt() { isLookedAt = true; }
	public void NotLookingAt() { isLookedAt = false; }
}
