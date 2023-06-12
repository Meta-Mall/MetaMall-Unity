using Dummiesman;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ShopsManager : MonoBehaviour {

	public GameObject[] floors;

	[DllImport("__Internal")]
	private static extern void EmitJSEvent(string eventName, string arg1, string arg2, string arg3);

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
#if UNITY_WEBGL == true && UNITY_EDITOR == false
		EmitJSEvent("RequestStores", null, null, null);
#endif
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
