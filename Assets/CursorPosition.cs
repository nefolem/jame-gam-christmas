using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPosition : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        transform.position= Input.mousePosition;
    }
    public Vector3 GetMouseTargetDirection(Transform objectTransform)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            return new Vector3(hit.point.x - objectTransform.position.x, 0f, hit.point.z - objectTransform.position.z).normalized;
        }


        return Vector3.zero;
    }
}
