using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceTowards : MonoBehaviour {

	public string facingTowardsTag = "MainCamera";
	GameObject facingTowards;

	// Start is called before the fir0st frame update
	void Start() {
		facingTowards = GameObject.FindGameObjectWithTag(facingTowardsTag);
	}

	// Update is called once per frame
	void Update() {
		if(facingTowards != null) {
			gameObject.transform.rotation = Quaternion.Euler(facingTowards.transform.rotation.eulerAngles);
		}
	}
}
