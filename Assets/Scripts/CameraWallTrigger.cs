using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWallTrigger : MonoBehaviour
{
    private GameObject wall;
    

    private void OnCollisionEnter(Collision collision)
    {
        wall = collision.gameObject;
        Debug.Log("wall");
        collision.gameObject.GetComponent<MeshRenderer>().enabled = !collision.gameObject.GetComponent<MeshRenderer>().enabled;
    }

    private void Update()
    {
        if(wall != null)
        {
            if(transform.position.x > -4) 
            {
                wall.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                wall.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }


}
