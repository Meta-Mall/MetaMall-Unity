using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ModelManager : MonoBehaviour
{
    struct productModel {
        string name;
        string price;
        string store;
        string description;
        string order_link;
        string model_id;
        string model;
    }

    // Start is called before the first frame update
    void Start()
    {
        getAllProducts();
    }

    // Update is called once per frame
    void Update()
    {
        

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

    async void getAllProducts() {
        var s = gameObject.name;
        var products = await FetchData("http://localhost:5000/product/getStoreProducts/2");
        
        Debug.Log(products);
        
        foreach ( var product in products ) {
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(s.ToString()));
            var loadedObj = new OBJLoader().Load(textStream);

            loadedObj.transform.SetParent(gameObject.transform);
            loadedObj.AddComponent<MeshRenderer>();
            loadedObj.AddComponent<MeshCollider>();
        }
    }

    async void addProduct() {

        var s = await FetchData("http://localhost:5000/product/vendor");

        Debug.Log(s);
        //create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(s.ToString()));
        var loadedObj = new OBJLoader().Load(textStream);

        loadedObj.transform.SetParent(gameObject.transform);
        loadedObj.AddComponent<MeshRenderer>();
        loadedObj.AddComponent<MeshCollider>();

    }
}
