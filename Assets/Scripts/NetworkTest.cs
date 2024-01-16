using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkTest : NetworkBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) {
            var movH = Input.GetAxis("Horizontal");
            var movV = Input.GetAxis("Vertical");
            transform.position = transform.position + new Vector3(movH * 0.1f, movV * 0.1f, 0);
        }
    }
}
