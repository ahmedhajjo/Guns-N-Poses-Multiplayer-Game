using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour {
    public NetWorkManage netManager;

    public GameEventListner beginEvent;
    public GameEventListner endEvent;
    [SerializeField] PlayActions[] allActionSet;
    int lastActionID = -1;
    int currentActionID;

    public AnimatorManage animator;
    void Start () { }

    void Update () {
        if (currentActionID < allActionSet.Length) {
            if (lastActionID != currentActionID) {
                InitializeAction ();
            }
            if (allActionSet[currentActionID].IsRunning ()) {
                GoNextRound ();
            }
        } else {
            Debug.Log ("Game Rounds Over.....");
            netManager.sockectManager.SendMsgOnFinish ();

        }
    }

    void InitializeAction () {
        lastActionID = currentActionID;
        allActionSet[currentActionID].Begin ();
        beginEvent.ev = allActionSet[currentActionID].onBegin;
        endEvent.ev = allActionSet[currentActionID].onEnd;
        beginEvent.ev = allActionSet[currentActionID].onBegin;
    }

    public void GoNextRound () {
        allActionSet[currentActionID].Done ();
        currentActionID++;

    }

}