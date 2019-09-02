using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class findMatchScript : MonoBehaviour {
    public StringField matchID;
    public StringField playerServerID;
    public StringField ServerIP;
    public SceneManage theGameScene;
    public string PlayerId;
    public bool connect = false;
    public void OnJoinGame () {
        matchID.value = null;
        if (!connect) StartCoroutine (CreateMatch ());
    }

    [System.Serializable]
    class PlayerData {
        public string lobbyId;
        public string playerId;
    }
    public void Start () {
#if !UNITY_EDITOR
        PlayerId = SystemInfo.deviceUniqueIdentifier;
#endif

    }

    private IEnumerator CreateMatch () {
        theGameScene.GameMessageLobby ("Looking For CowBoy");
        //connect = true;
        WWWForm form = new WWWForm ();
        form.AddField ("id", playerServerID.value);
        UnityWebRequest www = UnityWebRequest.Post ("http://" + ServerIP.value + "/CreateMatch/", form);

        yield return www.SendWebRequest ();
        var retrivedData = JsonUtility.FromJson<PlayerData> (www.downloadHandler.text);

        //matchID.value = www.downloadHandler.text.Substring(1, www.downloadHandler.text.Length-2);//dataDictionary.GetRandomWords();
        matchID.value = retrivedData.lobbyId;

        Debug.Log ("Match ID : " + retrivedData.lobbyId + " Player ID : " + retrivedData.playerId);

        //    if (matchID.value != null) StartCoroutine(IAmReady(matchID.value));
        //else theGameScene.GameMessageLobby("Check the connection...");

    }

    private IEnumerator IAmReady (string id) {

        WWWForm sendForm = new WWWForm ();
        sendForm.AddField ("id", id);
        UnityWebRequest www = UnityWebRequest.Post ("http://" + ServerIP.value + "/Ready/", sendForm);

        yield return www.SendWebRequest ();

        Debug.Log ("The ready check result : " + www.downloadHandler.text);

        if (www.downloadHandler.text != null) StartCoroutine (PersistantStartCheck (id));
        else theGameScene.GameMessageLobby ("issues with connection...");
    }

    private IEnumerator PersistantStartCheck (string id) {
        theGameScene.GameMessageLobby ("Searching for lobbies...");
        bool stop = false;
        int maxLoop = 1000;
        while (!stop) {
            UnityWebRequest www = UnityWebRequest.Get ("http://" + ServerIP.value + "/IsLobbyReady/" + matchID.value);

            yield return www.SendWebRequest ();

            Debug.Log ("The return Data is : " + www.downloadHandler.text);
            string returnData = www.downloadHandler.text;
            if (returnData == "active") {
                Debug.Log ("Lobby Started...");
                stop = true;
                theGameScene.PermitGameplay ();
                StopCoroutine ("PersistantStartCheck");
            }

            yield return new WaitForSeconds (0.5f);
            maxLoop--;
            if (maxLoop < 0) stop = true;
        }

    }

}