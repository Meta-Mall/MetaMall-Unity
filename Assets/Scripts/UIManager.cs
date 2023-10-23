using System.Runtime.InteropServices;
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


    [DllImport("__Internal")]
    private static extern void EmitJSEvent(string eventName, string arg1, string arg2, string arg3);


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

    public void GetCursorInfo() {
        EmitJSEvent("GetCursorInfo_Returned", Cursor.lockState.ToString(), null, null);
    }
}
