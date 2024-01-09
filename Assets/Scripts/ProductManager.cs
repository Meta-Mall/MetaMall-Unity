using System.Runtime.InteropServices;
using UnityEngine;

public class ProductManager : MonoBehaviour {

	[SerializeField]
	public ProductModel product;

	void Start() {
		
	}

	void Update() {

	}

	public void OpenProductLink() {
        Javascript.Emit("OpenLink", product.order_link, null, null);
	}

}
