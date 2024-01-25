using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


public class NetworkServer : MonoBehaviour
{
    NetworkManager m_NetworkManager;
    // Start is called before the first frame update
    void Start()
    {
        m_NetworkManager = this.GetComponent<NetworkManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerToNetwork() {

#if UNITY_WEBGL || UNITY_ANDROID
        m_NetworkManager.StartClient();
#endif
    }
}
