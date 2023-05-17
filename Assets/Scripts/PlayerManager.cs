using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class PlayerManager : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern void EmitJSEvent(string eventName, string arg1, string arg2, string arg3);
	
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
		Transform playerHUD = HUD.transform.Find("PlayerAvatar");
		playerAvatar = playerHUD.Find("PlayerMask").GetChild(0).gameObject;
		playerAlias = playerHUD.Find("PlayerAlias").gameObject;
		User = new Customer();

#if UNITY_WEBGL == true && UNITY_EDITOR == false
		EmitJSEvent("PrintSomething", "hello", "lallu", null);
#endif
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
