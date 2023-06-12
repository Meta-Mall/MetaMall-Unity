using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCustomization : MonoBehaviour {

	public Vector3 playerCustomizeLocation;
	public Vector3 playerCustomizeRotation;
	public Vector3 spawnLocation;

	public CinemachineVirtualCamera CustomizationCamera;
	public CinemachineVirtualCamera PlayerFollowCamera;

	public GameObject[] characters;
	public Avatar[] characterAvatars;

	PlayerInput input;
	int selected = 0;
	int Selected {
		get => selected;
		set {
			characters[selected].SetActive(false);
			selected = value;
			characters[selected].SetActive(true);
			GetComponent<Animator>().avatar = characterAvatars[selected];
		}
	}

	void Start() {
		input = GetComponent<PlayerInput>();

		PlayerFollowCamera.enabled = false;
		CustomizationCamera.enabled = true;
		input.enabled = false;
		UIManager.Instance.HideHUD();
		UIManager.Instance.ShowCharacterCustomization();
		transform.SetLocalPositionAndRotation(playerCustomizeLocation, Quaternion.Euler(playerCustomizeRotation));

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	void Update() {
		if (Keyboard.current[Key.RightArrow].wasPressedThisFrame || Keyboard.current[Key.D].wasPressedThisFrame) {
			Selected = (Selected + 1) % characters.Length;
		}
		else if (Keyboard.current[Key.LeftArrow].wasPressedThisFrame || Keyboard.current[Key.A].wasPressedThisFrame) {
			Selected = (Selected + characters.Length - 1) % characters.Length;
		}

		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void SelectCharacterByIndex(int i) {
		Selected = i;
	}

	public void ChooseCharacter() {
		transform.SetLocalPositionAndRotation(spawnLocation, Quaternion.identity);
		UIManager.Instance.HideCharacterCustomization();
		UIManager.Instance.ShowHUD();
		PlayerFollowCamera.enabled = true;
		CustomizationCamera.enabled = false;
		input.enabled = true;

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		enabled = false;
	}
}
