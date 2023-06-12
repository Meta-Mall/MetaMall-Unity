using Dummiesman;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Shop : MonoBehaviour {

	public Store storeModel;

	void Start() {

	}

	void Update() {

	}

	public async void LoadModels() {
		foreach(ProductModel p in storeModel.products_list) {
			var res = await FetchData("http://localhost:5000/model/" + p.model_id);

			var textStream = new MemoryStream(Encoding.UTF8.GetBytes(res.ToString()));
			var loadedObj = new OBJLoader().Load(textStream);

			loadedObj.transform.SetParent(gameObject.transform);
			loadedObj.AddComponent<MeshRenderer>();
			loadedObj.AddComponent<MeshCollider>();
			loadedObj.transform.localPosition = new Vector3(0, 0, 0);
			loadedObj.transform.localPosition = new Vector3(p.position.x, p.position.y, p.position.z);
		}
	}


	async Task<string> FetchData(string uri) {

		UnityWebRequest webRequest = UnityWebRequest.Get(uri);
		await webRequest.SendWebRequest();

		switch (webRequest.result) {
			case UnityWebRequest.Result.ConnectionError:
				return webRequest.error;
			case UnityWebRequest.Result.DataProcessingError:
				Debug.LogError(": Error: " + webRequest.error);
				return webRequest.error;
			case UnityWebRequest.Result.ProtocolError:
				Debug.LogError(": HTTP Error: " + webRequest.error);
				return webRequest.error;
			case UnityWebRequest.Result.Success:
				Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
				return webRequest.downloadHandler.text;
		}
		return webRequest.downloadHandler.text;
	}
}
