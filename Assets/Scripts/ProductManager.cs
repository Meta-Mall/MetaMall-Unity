using System.Runtime.InteropServices;
using UnityEngine;

public class ProductManager : MonoBehaviour {

	[DllImport("__Internal")]
	private static extern void EmitJSEvent(string eventName, string arg1, string arg2, string arg3);

	[SerializeField]
	public ProductModel product;

	void Start() {
		
	}

	void Update() {

	}

	public void OpenProductLink() {
        EmitJSEvent("OpenLink", product.order_link, null, null);
	}

}
