using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakingAction : MonoBehaviour {
    private Vector3 AccelerationShaking;

    private float ShackLimition = 20;
    private float ShackPoint;

    public UnityEvent Event;

    // Update is called once per frame
    void Update () {
        AccelerationShaking = Input.acceleration;
        ShackPoint += AccelerationShaking.magnitude;
        if (ShackPoint >= ShackLimition) {

            Event.Invoke ();

        }
    }
}