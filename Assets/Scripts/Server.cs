using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;

public class Server : MonoBehaviour {

    public static Server Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start() {
        //StartCoroutine(Upload());
    }

    void Update() {

    }

    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result) {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
    }

    IEnumerator Upload(string userName, string email, string password, string country, string city, string gender) {

        Customer customer = new(userName, email, password, country, city, gender);
        string obj = JsonUtility.ToJson(customer);
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:4000/customer/add", obj)) {

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
    }



}
