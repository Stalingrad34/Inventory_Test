using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;


public class Connector : MonoBehaviour
{
    [SerializeField] private string url = default;
    [SerializeField] private string auth = default;


    public void SendPost(int id, string eventName)
    {
        StartCoroutine(Connect());
        
        IEnumerator Connect()
        {
            WWWForm form = new WWWForm();
            form.AddField("auth", auth);
            form.AddField("id", id);
            form.AddField("event", eventName);
        
            UnityWebRequest post = UnityWebRequest.Post(url, form);
            yield return post.SendWebRequest();
        
        }
        
    }
    
    
}