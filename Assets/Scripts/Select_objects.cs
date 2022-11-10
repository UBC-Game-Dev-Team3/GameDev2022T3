using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Select_objects : MonoBehaviour
{
    public Camera cm;
    public TextMeshProUGUI item_name;
    public float view_range = 15f;

    private GameObject current_go;

    private void FixedUpdate()
    {
        RaycastHit hit_info;

        //keeps track of any interactive objects that have been selected
        if (current_go)
        {
            item_name.text = "";
            current_go.GetComponent<Outline>().enabled = false;
            current_go = null;
        }

        if (Physics.Raycast(cm.transform.position, cm.transform.forward, out hit_info, view_range))
        {
            if ((hit_info.collider.tag == "Interactive"))
            {
                //Debug.Log(hit_info.collider.name);
                hit_info.collider.gameObject.GetComponent<Outline>().enabled = true;
                current_go = hit_info.collider.gameObject;

                item_name.text = hit_info.collider.gameObject.name; //display item name

                /* 
                 * Getting errors of using system input package vs Unityengine.input class
                if(Input.GetMouseButtonDown(0)) //select item with left click and do something
                {
                    Debug.Log(hit_info.collider.gameObject.name);
                }*/
            } 
        }
      
        
    }
}
