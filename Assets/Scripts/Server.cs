using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Dummiesman;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class Server : MonoBehaviour {

    public static Server Instance { get; private set; }
    public GameObject pro;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start() {
        //StartCoroutine(Upload());
        getModel();
    }

    void Update() {
        
    }

    IEnumerator Upload(string userName, string email, string password, string country, string city, string gender) {

        Customer customer = new(userName, email, password, country, city, gender);
        string obj = JsonUtility.ToJson(customer);

        using UnityWebRequest www = UnityWebRequest.Post("http://localhost:4000/customer/add", obj);
        www.SetRequestHeader("content-type", "application/json");
        www.uploadHandler.contentType = "application/json";
        www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(obj));

        yield return www.SendWebRequest();
        //Debug.Log("result" + www.result);

        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log("upload error : " + www.error);
        }
        else {
            Debug.Log("Form upload complete!" + obj);
        }


    }

    async Task<string> FetchModel(string uri) {
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
    
    async void getModel() {
        
        var s = await FetchModel("http://localhost:4000/cloud/vendor");

        Debug.Log(s);
        //create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(s.ToString()));
        var loadedObj = new OBJLoader().Load(textStream);

        loadedObj.transform.SetParent(pro.transform);
        loadedObj.AddComponent<MeshRenderer>();
        loadedObj.AddComponent<MeshCollider>();

    }

}
