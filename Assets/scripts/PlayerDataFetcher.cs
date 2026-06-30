using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public class PlayerDataFetcher : MonoBehaviour
{
    public string apiUrl = "https://api.jsonbin.io/v3/b/6686a992e41b4d34e40d06fa";

    public PlayerUIDisplay uiDisplay;

    public event Action OnRefreshRequested;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       OnRefreshRequested += FetchData;
       FetchData();
    }

    void OnDestroy()
    {
        OnRefreshRequested -= FetchData;
    }

    public void FetchData()
    {
        StartCoroutine(GetPlayerData());
    }

    public void TriggerRefresh()
    {
        OnRefreshRequested?.Invoke();
        Debug.Log("Data refreshed");
    }

    /* // Update is called once per frame
    void Update()
    {
        
    } */

    IEnumerator GetPlayerData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string json = webRequest.downloadHandler.text;
                json = json.Replace("\"private\"", "\"isPrivate\"");

                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                uiDisplay.ShowData(data);
                
            }
            else
            {
                Debug.Log("Error getting data: " + webRequest.error);
                uiDisplay.ShowError(webRequest.error);
            }
        }
    }
}