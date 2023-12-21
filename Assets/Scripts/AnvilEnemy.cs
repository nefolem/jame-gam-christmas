using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilEnemy : Enemy
{
    [SerializeField] private float _findRadius;
    [SerializeField] private GameObject _anvilObject;
    private Transform _target;

    private void Start()
    {
        _isPatroling = true;
    }

    private void Update()
    {
        if(_target == null)
        {
            TryFindPlayer();
        }
        else if(_target!=null && transform.position.x != _target.transform.position.x)
        {
            MoveToTarget(_target);            
        }
        else ThrowAnvil();
    }
 
    private void TryFindPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _findRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PlayerMovement>())
            {
                _target = collider.transform;
                _isPatroling = false;
                //MoveToTarget(_target);
                //_targetObject = collider.gameObject;
                break;
            }
        }
    }

    private void ThrowAnvil()
    {
        _anvilObject.GetComponent<Rigidbody>().useGravity = true;
        _anvilObject.transform.SetParent(null);
        HideBehindObstacle();
    }
}
