using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    //[SerializeField] private float _speed = 30f;

    //private void Update()
    //{
    //    transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("meow");
        Destroy(gameObject);
    }
}
