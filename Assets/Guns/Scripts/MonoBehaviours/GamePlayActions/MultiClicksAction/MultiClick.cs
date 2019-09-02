using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiClick : PlayActions {

    public Text pressLeft;
    public StringField msg;
    public int clickingTimes;

    int index;
    string showText;
    public float endTime;

    public override void Begin () {
        base.Begin ();
        Cursor.visible = true;
        maxRuns = 5;
        msg.value = "Click the button " + maxRuns.ToString () + " Times";
    }

    public override bool IsRunning () {
        if (runsOver == maxRuns) return true;

        return false;
    }

    public override void Done () {
        base.Done ();

    }
    public void clickMe (int Clicker) {
        runsOver++;
        pressLeft.text = (maxRuns - runsOver).ToString () + " more press to go !";

    }
}