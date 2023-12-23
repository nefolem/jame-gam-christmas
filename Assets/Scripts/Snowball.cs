using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField] GameObject _snowballHit;
    [SerializeField] AudioSource _as;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_snowballHit, transform.position, Quaternion.identity);
        _as?.Play();
        Destroy(gameObject);
    }
}
