using System.Collections;
using System.Collections.Generic;
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


    public GameObject tooltip;

    public void ShowTooltip(string text, GameObject showAbove) {
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText(text);
        tooltip.transform.position = showAbove.transform.position + new Vector3(0, 0.5f, 0);
    }

    public void HideTooltip() {
        tooltip.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
        tooltip.transform.position = new Vector3(-100, -100, -100);
    }

    void Start() {
        
    }
}
