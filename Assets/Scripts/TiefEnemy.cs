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
    private GameObject _lastStolen;

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
            if(_hideSpot == null)
            {
                HideBehindObstacle();
            }
            MoveToTarget(_hideSpot);
        }     
    }

    void TryStealGift()
    {
        if (_targetObject == null || Vector3.Distance(transform.position, _targetObject.transform.position) > _findToStealRadius)
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
            if (collider.CompareTag(_giftTag) && collider.gameObject != _lastStolen)
            {                
                _targetObject = collider.gameObject;
                break;
            }
        }
    }

    void StealGift()
    {
        _targetObject.GetComponent<Rigidbody>().useGravity = false;
        _isGiftStolen = true;
    }    


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Tree>())
        {
            _isHiding = true;
            _lastHided = collision.gameObject;
            _hideSpot = null;
        }
        else if (collision.collider.GetComponent<Snowball>())
        {
            _targetObject.GetComponent<Rigidbody>().useGravity = true;
            _lastStolen = _targetObject;
            _targetObject = null;
            _isGiftStolen = false;
            TakeDamage(1);
            
        }
        
    }

}
