using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerControl : NetworkBehaviour {


    
    //Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    [SerializeField]
    private float speed = 0.5f;

    void Update() {

        if (!IsOwner) return;

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(xInput, 0, yInput).normalized;
        transform.Translate(speed * Time.deltaTime * moveDirection);
    }
}
