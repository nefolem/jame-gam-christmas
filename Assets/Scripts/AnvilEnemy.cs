using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnvilEnemy : Enemy
{
    [SerializeField] private float _findRadius;
    [SerializeField] private GameObject _anvilObject;
    [SerializeField] private Transform _anvilPoint;
    //private Transform _target;
    private bool _isAnvilInHands = true;
    private float _timer;
    //private float _delayThrow = 10f;
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

                TryFindPlayer(_findRadius);
            }
            else
            {
                FindAndSetTargetAnvil();
            }

        }
        else
        {
            Vector3 throwPosition = new Vector3(_target.transform.position.x - transform.forward.x * 3, transform.position.y, _target.transform.position.z);
            //print($"throw pos: {throwPosition} trans pos: {transform.position}");
            if (transform.position != throwPosition)
            {
               
                MoveToTarget(_target);
            }
            else
            {
                //Debug.Log("else");
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
 
  

    private void ThrowAnvil()
    {
        _anvilObject.GetComponent<Rigidbody>().useGravity = true;
        _anvilObject.GetComponent<Rigidbody>().isKinematic = false;
        _lastThrown = _anvilObject;
        _anvilObject.transform.SetParent(null);
        _anvilObject.transform.position = transform.position - Vector3.down * 15;
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
                break;
            }
            
        }
    }

    void GetAnvil()
    {
        _anvilObject = _target.gameObject;
        _anvilObject.transform.SetParent(_anvilPoint);
        _anvilObject.transform.position = _anvilPoint.transform.position;
        _anvilObject.GetComponent<Rigidbody>().useGravity = false;
        _anvilObject.GetComponent<Rigidbody>().isKinematic = true;
        _target = null;
        _isAnvilInHands = true;
    }
}
