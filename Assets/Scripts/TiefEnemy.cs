using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class TiefEnemy : Enemy
{

    [SerializeField] private string _giftTag = "Gift";

    [Header("Steal and move")]
    [SerializeField] private float _stealRadius = 3f;
    [SerializeField] private float _findToStealRadius = 3f;

    [SerializeField] private float _moveCooldown = 5f;
    [SerializeField] private float _nextMoveTime = 5f;
    [SerializeField] private GameObject _stolenGiftPoint;

    
    private GameObject _targetObject;
    private float _timer;
    private bool _isGiftStolen = false;
    private bool _isHiding = false;
    private GameObject _alreadyHided;

    void Update()
    {              
        if(!_isGiftStolen)
        {
            TryStealGift();
            MoveToTarget(_targetObject?.transform);
        }
        else
        {
            _targetObject.transform.position = _stolenGiftPoint.transform.position;
            MoveToTarget(_hideSpot);
        }     
    }

    void TryStealGift()
    {
        if (_targetObject == null || Vector3.Distance(transform.position, _targetObject.transform.position) > _stealRadius)
        {
            FindAndSetTargetGift();            
        }
        else if (Vector3.Distance(transform.position, _targetObject.transform.position) <= _stealRadius)
        {
            StealGift();
            HideBehindObstacle();
        }
    }

    void FindAndSetTargetGift()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _findToStealRadius);
        foreach (Collider collider in colliders)
        {
            
            if (collider.CompareTag(_giftTag))
            {
                _targetObject = collider.gameObject;
                break;
            }
        }
    }

    void StealGift()
    {        
        //_targetObject?.SetActive(false);
        _isGiftStolen = true;
        //_targetObject = null;
    }    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Tree>())
        {
            _isHiding = true;
            _alreadyHided = collision.gameObject;
        }
        else if (collision.collider.GetComponent<Snowball>())
        {
            Debug.Log("snowball");
            TakeDamage(1);
            _isGiftStolen = false;
            _targetObject.GetComponent<Rigidbody>().useGravity = true;
            
        }
        
    }


    //public void Hide(bool isHiding)
    //{
    //    foreach (MeshRenderer mr in gameObject.GetComponentsInChildren<MeshRenderer>())
    //    {
    //        mr.enabled = !isHiding;
    //    }
    //    //_hideSpot.position = null;
    //    var ps = gameObject.GetComponentInChildren<ParticleSystem>();
    //    ps.enableEmission = !isHiding;
    //    _targetObject.SetActive(!isHiding);
      
    //}
}
