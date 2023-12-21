using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CursorPosition
{
    
    public static Vector3 GetMouseTargetDirection(Transform objectTransform)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
            return new Vector3(hit.point.x - objectTransform.position.x, 0f, hit.point.z - objectTransform.position.z).normalized;
        }


        return Vector3.zero;
    }

    public static Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        else return Vector3.zero;
    }
}
