using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITexSetter : MonoBehaviour
{
    Text textUI;
    public StringField text;
    void Start()
    {
        textUI = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = text.value;
    }
}
