using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class MCQAction : PlayActions
{

    public GameObject UIControleObject;
    public Text penaltyMessage;
    public StringField text;
    public int answer;
    bool wrongPressed;

    bool isCorrect;

    bool isDone;
    private int index;
    public string showText;

    public AudioSource audioSource;

    public StringField ServerIP;
    public StringField matchID;
    public override void Begin()
    {
        base.Begin();
        Cursor.visible = true;
        StartCoroutine(SetQuestion());
    }

    public override bool IsRunning()
    {
        if (isDone) return true;
            return false;
    }

    public override void Done()
    {
        base.Done();// test again
        //uiBox.SetActive(false);
    }

    private IEnumerator SetQuestion()
    {
        Debug.Log("Calling server for MCQ");
        UnityWebRequest www = UnityWebRequest.Get("http://" + ServerIP.value + "/question/" + matchID.value + "/"+ 1.ToString());
        yield return www.SendWebRequest();

         Debug.Log(www.downloadHandler.text);
        
        string dataString = www.downloadHandler.text;
        dataString = dataString.Substring(1, dataString.Length - 2);


        string[] parts = dataString.Split('|');

        text.value = parts[0];
        answer = int.Parse(parts[1]);
    }


    public void CheckAnswer(int buttonID)
    {
        if (!wrongPressed)
        {
            if (buttonID == answer)
            {
                text.value = "<color=green>" + text.value + "</color>";
                isDone = true;
            }


            else if (buttonID != answer)
            {
                wrongPressed = true;
                text.value = "<color=red>" + text.value + "</color>";
                penaltyMessage.gameObject.SetActive(true);
                StartCoroutine("PenaltyWait");
                audioSource.Play();
            }
        }
    }



    IEnumerator PenaltyWait()
    {
        UIControleObject.SetActive(false);
        int maxSec = 3;

        while(maxSec >= 0)
        {
            penaltyMessage.text = "Cow Boys Doesn't Lose! : " + maxSec;
            yield return new WaitForSeconds(1f);
            maxSec--;
        }
    
        penaltyMessage.gameObject.SetActive(false);
        isDone = true;
    }

}
