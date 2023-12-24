using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    [SerializeField] GameObject _snowballHit;
    [SerializeField] private float _attractionForce = 10f;
    [SerializeField] private float _attractionRadius;
    //private AudioSource _as;
    private bool _isAttracted;

    private void Start()
    {
        //_as = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(_snowballHit, transform.position, Quaternion.identity);
        //_as.Play();
        Destroy(gameObject);
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _attractionRadius);
        
        foreach(Collider collider in colliders)
        {
            if(collider.GetComponent<Enemy>() && !_isAttracted)
            {
                
                Vector3 directionToEnemy = collider.transform.position - transform.position;
                
                GetComponent<Rigidbody>().AddForce(directionToEnemy.normalized * _attractionForce);
                _isAttracted = true;
            }
        }
    }
}
