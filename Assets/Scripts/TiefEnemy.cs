using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class TiefEnemy : Enemy
{

    [SerializeField] private string _giftTag = "Gift";

    [Header("Steal and move")]
    [SerializeField] private float _stealRadius = 3f;
    [SerializeField] private float _findToStealRadius = 50f;

    [SerializeField] private GameObject _stolenGiftPoint;
    
    private GameObject _targetObject;
    private bool _isGiftStolen = false;
    private GameObject _lastStolen;
    private bool _isTargetBox;
    

    void Update()
    {              
        if(!_isGiftStolen)
        {
            TryStealGift();
            MoveToTarget(_targetObject?.transform);
        }
        else
        {            
            if(_targetObject != null)
            {
                _targetObject.transform.position = _stolenGiftPoint.transform.position;
                TryFindPlayer(_findToStealRadius);
            }
            else _isGiftStolen=false;
            
            //if(_hideSpot == null)
            //{
            //    HideBehindObstacle();
            //}
            //MoveToTarget(_hideSpot);
        }     
    }

    void TryStealGift()
    {
        if (_targetObject == null || Vector3.Distance(transform.position, _targetObject.transform.position) > _findToStealRadius || _isTargetBox)
        {
            FindAndSetTargetGift();            
        }
        else if (_targetObject != null && !_isTargetBox && Vector3.Distance(transform.position, _targetObject.transform.position) <= _stealRadius)
        {
            StealGift();
            //HideBehindObstacle();
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
                _isTargetBox = false;
                break;
            }
            else if(collider.GetComponent<GiftsSpawner>() && !_isTargetBox)
            {
                _isTargetBox = true;
                _targetObject = collider.gameObject;
            }
        }
    }

    private void StealGift()
    {
     
        _targetObject.GetComponent<Rigidbody>().useGravity = false;
        _targetObject.GetComponent<GiftPickUp>().enabled = false;
        _playFX.PlayMeanness();
        _isGiftStolen = true;
    }    


    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.collider.GetComponent<Snowball>())
        {
            if (_isGiftStolen)
            {
                _targetObject.GetComponent<Rigidbody>().useGravity = true;
                _targetObject.GetComponent<GiftPickUp>().enabled = true;
                _lastStolen = _targetObject;
                _targetObject = null;
                _isGiftStolen = false;
            }
            
        }
        
    }

}
