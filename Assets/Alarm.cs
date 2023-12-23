using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public float rotationSpeed = 30f;
    [SerializeField] private GameObject _alarmLight;


    void Update()
    {
        
        float currentRotation = _alarmLight.transform.localRotation.eulerAngles.y;        
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

        _alarmLight.transform.localRotation = Quaternion.Euler(0f, newRotation, 0f);
    }
}
