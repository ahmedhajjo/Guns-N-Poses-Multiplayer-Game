using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

[Serializable]
public class SockectManager : MonoBehaviour {

    static SocketIOComponent socket;

    public NetWorkManage NetworkManage;

    public StringField ID;

    public StringField MatchID;

    public findMatchScript findMatch;

    public SceneManage theGameScene;

    public RoundManager Rm;
    private int player;
    private bool isJoined;
    private bool isFinished;

    private int Score;

    private int HealthPlayer;

    public GameEvent onWin;
    public GameEvent onLose;

    void Start () {
        NetworkManage.sockectManager = this;

        socket = GetComponent<SocketIOComponent> ();
        socket.On ("returnID", OnReturnID);
        socket.On ("matchOver", MatchClosed);
        socket.On ("gameStarted", GameStarted);
        socket.On ("stageFinishMsg", StageComplete);
        socket.On ("scoreRound", RoundsScore);

        DontDestroyOnLoad (this);
        Score = 0;
        Cursor.visible = false;
    }

    void GameStarted (SocketIOEvent e) {
        Debug.Log ("Start");
        theGameScene.PermitGameplay ();
    }

    public void JoinedGame () {
        if (!isJoined) socket.Emit ("joinedGame");
        isJoined = true;
    }

    public void StageComplete (SocketIOEvent e) {
        string WinnerID = e.data["id"].ToString ();
        WinnerID = WinnerID.Substring (1, WinnerID.Length - 2);
        string players = ID.value;

        if (players == WinnerID) {
            Debug.Log ("You Finished the round");
            onWin.Raise ();

        } else {
            Debug.Log ("Other finished the round first");
            onLose.Raise ();
        }
        Debug.Log ("RoundFinished");

    }

    // Add Score Manager for Rounds  
    public void RoundsScore (SocketIOEvent e) {

        string WinnerID = e.data["id"].ToString ();
        WinnerID = WinnerID.Substring (1, WinnerID.Length - 2);
        string players = ID.value;

        if (players == WinnerID) {

            Debug.Log ("YOUWinRound");

            Debug.Log (Score);
        } else {
            Debug.Log ("YouLostRound");

        }
        Rm.GoNextRound ();
        Debug.Log ("Go Next Round");
        Debug.Log (Score);

    }

    public void SendMsgOnFinish () {
        JSONObject data = new JSONObject ();
        data.AddField ("id", ID.value);
        data.AddField ("matchID", MatchID.value);
        socket.Emit ("FinishedTask", data);
        socket.Emit ("ShootPlayer", data);

    }

    void OnReturnID (SocketIOEvent e) {
        Debug.Log ("Id" + e.data);
        ID.value = e.data["id"].ToString ();
        ID.value = ID.value.Substring (1, ID.value.Length - 2);
        findMatch.OnJoinGame ();
    }

    void OnRegistered (SocketIOEvent e) {
        Debug.Log ("Registered id :" + e.data);
    }

    void MatchClosed (SocketIOEvent e) {

        Debug.Log ("Match over called..." + e.data["id"].ToString ());

        string loseID = e.data["id"].ToString ();
        loseID = loseID.Substring (1, loseID.Length - 2);
        string players = ID.value;

        if (players == loseID) {
            Debug.Log ("You LOST!");

        } else {
            Debug.Log ("YOU WON");
        }

        Debug.Log ("Lobby Closed" + e.data);

    }

    public float GetFloatFromJson (JSONObject data, string key) {
        return float.Parse (data[key].ToString ().Replace ("\"", ""));
    }

}