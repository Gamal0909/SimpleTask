using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ServerCommunication : MonoBehaviour
    {
        private string serverUrl = "https://wadahub.manerai.com/api/inventory/status";
        private string authToken = "Bearer kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

        public void SendItemToServer(ItemData itemData, string eventType)
        {
            Debug.Log($"{itemData} :{eventType}");
            StartCoroutine(SendRequest(eventType, itemData.itemId));
        }

        private IEnumerator SendRequest(string eventType, int id)
        {
            // Ensure the correct format of the JSON object being sent
            string json = JsonUtility.ToJson(new { eventType, id });
            Debug.Log($"Sending JSON: {json}");

            UnityWebRequest request = new UnityWebRequest(serverUrl, "POST");
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(json); // Use UTF8 encoding for better compatibility
            request.uploadHandler = new UploadHandlerRaw(buffer);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", authToken);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request successful");
            }
            else
            {
                // Log more details to help debug
                Debug.LogError($"Request failed: {request.error}");
                Debug.LogError($"Response: {request.downloadHandler.text}");
            }
        }

    }
}