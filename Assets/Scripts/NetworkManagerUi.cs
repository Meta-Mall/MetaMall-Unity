using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUi : MonoBehaviour
{
    [SerializeField] Button client;
    [SerializeField] Button server;
    [SerializeField] Button host;

    private void Awake() {

        server.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
