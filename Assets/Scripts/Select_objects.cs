using StarterAssets;
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
    Player_control controls;
    Vector2 move;

    private void Awake()
    {
        controls = new Player_control();
        controls.Player.Select.performed += ctx => select();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void select()
    {
        if(current_go)
        {
            Debug.Log("dkajfakldjfa");
        }
    }

    private void FixedUpdate()
    {
        //Vector3 movement = new Vector3(move.x, 0, move.y) * speed * Time.deltaTime;
        //transform.Translate(movement, Space.World);

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
            } 
        }
      
        
    }

}
