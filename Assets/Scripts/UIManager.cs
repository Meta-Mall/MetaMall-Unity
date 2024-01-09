using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour {

    //Singleton Method
    public static UIManager Instance { get; private set; }
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }




    // GLOBAL VARIABLES FOR UI SETTINGS
    public GameObject HUD;
    public GameObject CharacterCustomizeHUD;
    public GameObject ProductHUD;
    public GameObject tooltip;
    public GameObject MainCamera;
    public GameObject minimapFollowTarget;
    public bool minimapRotate = true;
    public float minimapHeight = 10f;

    //Product UI
    ProductManager OpenedProduct = null;


    public void ShowTooltip(string text, GameObject showAbove) {
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);
        tooltip.transform.position = showAbove.transform.position + new Vector3(0, 0.5f, 0);
    }

    public void HideTooltip() {
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
        tooltip.transform.position = new Vector3(-100, -100, -100);
    }

    public void HideHUD() { HUD.SetActive(false); }

    public void ShowHUD() { HUD.SetActive(true); }

    public void ShowCharacterCustomization() { CharacterCustomizeHUD.SetActive(true); }
    public void HideCharacterCustomization() { CharacterCustomizeHUD.SetActive(false); }

	public void ShowProductUI(ProductManager product) {
        ProductHUD.SetActive(true);
        OpenedProduct = product;
		Transform hud = ProductHUD.transform.GetChild(0);
		hud.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = OpenedProduct.product.name;
		hud.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = "Price: " + OpenedProduct.product.price.ToString();
		hud.transform.Find("Scroll View/Viewport/Content/DescriptionText").GetComponent<TextMeshProUGUI>().text = OpenedProduct.product.description;
	}

	public void HideProductUI() {
        ProductHUD.SetActive(false);
		OpenedProduct = null;
	}

	public void OpenProductLink() { OpenedProduct.OpenProductLink(); }

    public void LockCursor() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
        Javascript.Emit("CursorMode", Cursor.lockState.ToString(), null, null);
	}

	public void UnlockCursor() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
        Javascript.Emit("CursorMode", Cursor.lockState.ToString(), null, null);
	}

	public void GetCursorInfo() {
        Javascript.Emit("GetCursorInfo_Returned", Cursor.lockState.ToString(), null, null);
    }

}
