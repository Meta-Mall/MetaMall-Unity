using UnityEngine;

public class ShopsManager : MonoBehaviour {

	public GameObject[] floors;

	struct productModel {
		string name;
		string price;
		string store;
		string description;
		string order_link;
		string model_id;
		string model;
	}

	void Start() {
		Javascript.Emit("RequestStores", null, null, null);
	}

	public void ReceiveFloor0(string data) {
		ReceiveStores(data, 0);
	}

	public void ReceiveFloor1(string data) {
		ReceiveStores(data, 1);
	}

	public void ReceiveStores(string data, int floor) {
		string[] storesStr = data.Split(";;");

		for (int i = 0; i < storesStr.Length; i++) {
			Store store = JsonUtility.FromJson<Store>(storesStr[i]);
			GameObject shopObj = floors[floor].transform.GetChild(i).gameObject;
			shopObj.GetComponent<Shop>().storeModel = store;
			shopObj.GetComponent<Shop>().LoadModels();
		}
	}
}
