using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManage : MonoBehaviour {
    [SerializeField] GameObject gameStartButton;

    [SerializeField] GameObject quitButton;

    [SerializeField] Text counterText;
    [SerializeField] Text messageText;

    public void GameMessageLobby (string msg) {
        messageText.text = msg;
        messageText.gameObject.SetActive (true);
    }

    public void PermitGameplay () {
        StartCoroutine (StartCountDown ());
    }

    IEnumerator StartCountDown () {
        counterText.gameObject.SetActive (true);
        gameStartButton.SetActive (false);
        messageText.gameObject.SetActive (false);
        quitButton.gameObject.SetActive (false);
        int maxCount = 5;
        while (maxCount >= 0) {
            counterText.text = maxCount.ToString ();
            yield return new WaitForSeconds (1);
            maxCount--;
        }

        OnStartGame ();
    }

    public void OnStartGame () {
        SceneManager.LoadScene (1);
        Cursor.visible = false;

    }

}