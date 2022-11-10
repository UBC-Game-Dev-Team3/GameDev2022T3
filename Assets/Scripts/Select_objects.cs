using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_objects : MonoBehaviour
{
    public Camera cm;
    public float view_range = 15f;
    private GameObject current_go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit_info;
        
        //keeps track of any interactive objects that have been selected
        if(current_go) 
        {
            current_go.GetComponent<Outline>().enabled = false;
            current_go= null;
        }

        if(Physics.Raycast(cm.transform.position, cm.transform.forward, out hit_info, view_range))
        {
            if ((hit_info.collider.tag == "Interactive"))
            {
                //Debug.Log(hit_info.collider.name);
                hit_info.collider.gameObject.GetComponent<Outline>().enabled = true;
                current_go = hit_info.collider.gameObject;
            }
        }
      
        
    }
}
