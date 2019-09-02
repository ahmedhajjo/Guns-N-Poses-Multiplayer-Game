using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickingAction : PlayActions {
    //public FloatField Field;
    public int clickingTimes;
    public Button[] buttons;
    public Image[] colorSequenceImages;
    //public ButtonFeild ButtonFeild;

    public int[] sequenceOrder;

    int index;
    string showText;

    bool isDone;

    public AudioSource audioSource;

    public override void Begin () {
        base.Begin ();

        sequenceOrder = new int[buttons.Length];
        for (int i = 0; i < sequenceOrder.Length; i++) {
            sequenceOrder[i] = i;
        }
        Cursor.visible = true;
        SetupButtons ();
    }

    public override bool IsRunning () {
        if (isDone) return true;
        return false;
    }

    public override void Done () {
        base.Done ();

    }

    public void SetupButtons () {
        for (int i = 0; i < colorSequenceImages.Length; i++) {
            colorSequenceImages[i].color = Random.ColorHSV ();
        }

        for (int i = 0; i < sequenceOrder.Length; i++) {
            int point = Random.Range (0, sequenceOrder.Length);
            int num = sequenceOrder[point];
            int num2 = sequenceOrder[i];
            sequenceOrder[i] = num;
            sequenceOrder[point] = num2;
        }

        for (int i = 0; i < buttons.Length; i++) {
            buttons[sequenceOrder[i]].gameObject.GetComponent<Image> ().color = colorSequenceImages[i].color;
        }
    }

    public void clickMe (int Clicker) {
        if (sequenceOrder[index] == Clicker) {
            index++;
            if (index == sequenceOrder.Length) isDone = true;
        } else {
            SetupButtons ();
            audioSource.Play ();
            index = 0;
        }
    }
}