using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkManagerUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartHost() {
        if (NetworkManager.Singleton.StartHost()) {
            debugText.text = "Host started";
            Debug.Log("Host started");
        }
        else {
            Debug.Log("Host failed to Start");
        }
    }

    public void StartClient() {
        if (NetworkManager.Singleton.StartClient()) {
            debugText.text = "Client started";
            Debug.Log("Client started");
        }
        else {
            Debug.Log("Client failed to Start");
        }
    }

    public void StartServer() {
        if (NetworkManager.Singleton.StartClient()) {
            debugText.text = "Server started";
            Debug.Log("Server started");
        }
        else {
            Debug.Log("Server failed to Start");
        }
    }

}
