using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TypingAction : PlayActions {
    bool isStarted;
    public StringField displayText;
    public StringField matchID;

    public AudioSource audioSource;
    public StringField ServerIP;
    public string word;

    int TypingScore;
    int index;
    string showText;

    public override void Begin () {
        base.Begin ();
        //uiBox.SetActive(true);
        StartCoroutine (SetUpWord ());
        Debug.Log ("Running...");
    }

    public override bool IsRunning () {
        if (Input.inputString.Length > 0) TypeLetter (Input.inputString[0]);

        if (isStarted && index >= word.Length) {
            runsOver++;
            if (runsOver == maxRuns) {
                return true;
            } else {
                StartCoroutine (SetUpWord ());
            }
        }

        return false;

    }

    public override void Done () {
        base.Done ();
    }

    private void TypeLetter (char alphabet) {
        Debug.Log ("Typing reached...");
        if (alphabet == word[index]) {
            showText += "<color=green>" + word[index] + "</color>";
            TypingScore++;
            index++;
            displayText.value = showText + word.Substring (index);
        } else {
            showText += "<color=red>" + word[index] + "</color>";
            audioSource.Play ();

            reset ();
        }
        Debug.Log (TypingScore);

    }

    public void reset () {
        index = 0;
        showText = "";
        TypingScore = 0;
        displayText.value = word;
        Debug.Log ("Reset");
    }

    private IEnumerator SetUpWord () {
        index = 0;
        showText = "";

        UnityWebRequest www = UnityWebRequest.Get ("http://" + ServerIP.value + "/question/" + matchID.value + "/" + 0. ToString ());
        yield return www.SendWebRequest ();
        Debug.Log (www.downloadHandler.text);

        string newWord = www.downloadHandler.text.Substring (1, www.downloadHandler.text.Length - 2);

        if (newWord == word) {
            JsonUtility.FromJson<Question> (www.downloadHandler.text);
            SetUpWord ();
            yield return null;
        } else word = newWord;
        isStarted = true;
        displayText.value = word;
    }

}

[System.Serializable]
public class Question {
    public string question;
}