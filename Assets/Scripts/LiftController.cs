using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position == gameObject.transform.position) {
            Move(2);
        }
    }

	private void Move(int floor) {
		Player.transform.position = new Vector3(Player.transform.position.x, floor * 15, Player.transform.position.z);
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, floor*15, gameObject.transform.position.z);
	}

}
