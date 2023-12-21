using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilEnemy : Enemy
{
    [SerializeField] private float _findRadius;
    [SerializeField] private GameObject _anvilObject;
    [SerializeField] private Transform _anvilPoint;
    private Transform _target;
    private bool _isAnvilInHands = true;
    private float _timer;
    private float _delayThrow = 10f;
    private GameObject _lastThrown;

    private void Start()
    {
        
    }

    private void Update()
    {


        if (_target == null)
        {
            _isPatroling = true;
            if (_isAnvilInHands)
            {

                TryFindPlayer();
            }
            else
            {
                FindAndSetTargetAnvil();
            }

        }
        else
        {
            if (transform.position.x != _target.transform.position.x)
            {
               
                MoveToTarget(_target);
            }
            else
            {
                if (_isAnvilInHands)
                {

                    ThrowAnvil();
                }
                else
                {
                    GetAnvil();
                }
            }
        }
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
                Debug.Log(_target.gameObject.name);
                //MoveToTarget(_target);
                //_targetObject = collider.gameObject;
                break;
            }
        }
    }

    private void ThrowAnvil()
    {
        _anvilObject.GetComponent<Rigidbody>().useGravity = true;
        _lastThrown = _anvilObject;
        _anvilObject.transform.SetParent(null);
        _isAnvilInHands = false;
        _target = null;
        _playFX.PlayMeanness();       
        
    }

    void FindAndSetTargetAnvil()
    {
        _isPatroling = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _findRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Anvil>() && collider.gameObject != _lastThrown)
            {

                _target = collider.gameObject.transform;
                Debug.Log(_target.gameObject.name);
                break;
            }
            
        }
    }

    void GetAnvil()
    {
        _anvilObject = _target.gameObject;
        _anvilObject.transform.SetParent(_anvilPoint);
        _anvilObject.transform.position = _anvilPoint.transform.position;
        _target = null;
        _isAnvilInHands = true;
    }
}
