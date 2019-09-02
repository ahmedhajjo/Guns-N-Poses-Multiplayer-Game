using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Quizzess
{
    MSQAction, ClickingAction,Typing
}
public class Dispatcher : MonoBehaviour
{
    GameEventListner MCQ;
    GameEventListner typing;
    GameEventListner ClickingAction;
    GameEventListner multiClicksAction;
    StringField matchId;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
    }

       void Update()
    {
        
    }
    void GetNextQuestion()
    {
        StartCoroutine(Req("http://127.0.0.1:4000/question/" + matchId.value));
    }

    IEnumerator Req(string uri)
    {
        UnityWebRequest www = UnityWebRequest.Get(uri);

        yield return www.SendWebRequest();
        Debug.Log(JsonUtility.FromJson<Question>(www.downloadHandler.text));
    }
    //public void Dispatch(game game)
    //{
    //    switch ()
    //    {
    //        default:
    //    }
    //}


    [System.Serializable]
    public class game {

        string data;

    }
}
