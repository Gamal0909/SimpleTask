using UnityEngine;

namespace DefaultNamespace
{
    public class ServerCommunication : MonoBehaviour
    {
        private string serverUrl = "https://wadahub.manerai.com/api/inventory/status";
        private string authToken = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";

        public void SendItemToServer(ItemData itemData, string eventType)
        {
            string json = JsonUtility.ToJson(new { itemId =itemData.itemId,eventType });
            StartCoroutine(PostRequest(serverUrl, json));
        }

        private string PostRequest(string s, string json)
        {
            throw new System.NotImplementedException();
        }
    }
}