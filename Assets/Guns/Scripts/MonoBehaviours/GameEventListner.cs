using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListner : MonoBehaviour {

    public GameEvent ev;
    public UnityEvent onRaise;

    private void OnEnable () {
        ev.onRaise.AddListener (this.OnRaise);
    }

    private void OnDisable () {
        ev.onRaise.RemoveListener (this.OnRaise);
    }
    public void OnRaise () {
        onRaise.Invoke ();
    }
}