using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    //Singleton Method
    public static UIManager Instance { get; private set; }
    void Awake () {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }




    // GLOBAL VARIABLES FOR UI SETTINGS
    public GameObject tooltip;
    public GameObject MainCamera;
    public GameObject minimapFollowTarget;
    public bool minimapRotate = true;
    public float minimapHeight = 10f;


    public void ShowTooltip(string text, GameObject showAbove) {
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);
        tooltip.transform.position = showAbove.transform.position + new Vector3(0, 0.5f, 0);
    }

    public void HideTooltip() {
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
        tooltip.transform.position = new Vector3(-100, -100, -100);
    }
}
