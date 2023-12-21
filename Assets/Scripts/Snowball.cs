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
    [SerializeField] GameObject _snowballHit;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_snowballHit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
