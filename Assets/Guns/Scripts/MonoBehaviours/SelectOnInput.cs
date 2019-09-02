using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SelectOnInput : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject selectObject;

    private bool Buttonselect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetAxisRaw("Vertical" )!=0 && Buttonselect == false){
           eventSystem.SetSelectedGameObject(selectObject);
           Buttonselect = true;
       } 
    }


     private void OnDisable() {

        Buttonselect = false;
        
    }
}
