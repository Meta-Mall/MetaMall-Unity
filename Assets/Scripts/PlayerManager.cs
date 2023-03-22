using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	struct loginInfo {
		public string userJSON;
		public string type;
	}

	public Sprite guestAvatar;
	public GameObject HUD;
	GameObject playerAvatar;
	GameObject playerAlias;

	User user;
	User User {
		get => user;
		set {
			user = value;
			if(user is Customer customer) {
				Debug.Log("is cust");
				playerAlias.GetComponent<TextMeshProUGUI>().text = customer.userName;
				SetAvatar(customer.photoURL);
			}
			else if (user is Vendor vendor) {
				Debug.Log("is vend");
				playerAlias.GetComponent<TextMeshProUGUI>().text = vendor.Address;
				playerAvatar.GetComponent<Image>().sprite = guestAvatar;
			}
			else {
				Debug.Log("is nothing");
			}
		}
	}

	void Start() {
		Transform playerHUD = HUD.transform.Find("PlayerAvatar");
		playerAvatar = playerHUD.Find("PlayerMask").GetChild(0).gameObject;
		playerAlias = playerHUD.Find("PlayerAlias").gameObject;
		User = new Customer();

		SetAvatar("https://e7.pngegg.com/pngimages/799/987/png-clipart-computer-icons-avatar-icon-design-avatar-heroes-computer-wallpaper-thumbnail.png");
	}


	public void UserLoggedIn(string loginInfoJSON) {
		Debug.Log(loginInfoJSON);
		loginInfo info = JsonUtility.FromJson<loginInfo>(loginInfoJSON);
		if (info.type == "customer") {
			User = JsonUtility.FromJson<Customer>(info.userJSON);
		}
		else {
			User = JsonUtility.FromJson<Vendor>(info.userJSON);
		}
	}

	public void UserLoggedOut() {
		User = new Customer();
	}

	async void SetAvatar(string avatarURL) {
		if (string.IsNullOrWhiteSpace(avatarURL)) {
			playerAvatar.GetComponent<Image>().sprite = guestAvatar;
		}
		else {
			Texture2D pic = await Utils.GetTextureFromURL(avatarURL);
			playerAvatar.GetComponent<Image>().sprite = Utils.CreateSprite(pic);
		}
	}
}