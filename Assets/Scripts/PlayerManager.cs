using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class PlayerManager : MonoBehaviour {

	struct loginInfo {
		public string userJSON;
		public string type;
	}

	public Sprite guestAvatar;
	GameObject playerAvatar;
	GameObject playerAlias;

	User user;
	User User {
		get => user;
		set {
			user = value;
			if(user is Customer customer) {
				playerAlias.GetComponent<TextMeshProUGUI>().text = customer.userName;
				SetAvatar(customer.photoURL);
			}
			else if (user is Vendor vendor) {
				playerAlias.GetComponent<TextMeshProUGUI>().text = vendor.Address;
				playerAvatar.GetComponent<Image>().sprite = guestAvatar;
			}
		}
	}

	void Start() {
		Transform playerHUD = UIManager.Instance.HUD.transform.Find("PlayerAvatar");
		playerAvatar = playerHUD.Find("PlayerMask").GetChild(0).gameObject;
		playerAlias = playerHUD.Find("PlayerAlias").gameObject;
		User = new Customer();

		Javascript.Emit("PrintSomething", "hello", "lallu", null);
	}


	public void UserLoggedIn(string loginInfoJSON) {
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
