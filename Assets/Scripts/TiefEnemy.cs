using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiefEnemy : Enemy
{
    [SerializeField] private string _obstacleTag = "Obstacle";
    [SerializeField] private string _giftTag = "Gift";
    [SerializeField] private LayerMask _obstacleLayer;

    [Header("Steal and move")]
    [SerializeField] private float _stealRadius = 3f;
    [SerializeField] private float _findToStealRadius = 3f;
    [SerializeField] private float _hideRadius = 5f;
    [SerializeField] private float _moveCooldown = 5f;
    [SerializeField] private float _nextMoveTime = 5f;

    private Transform _hideSpot;
    private GameObject _targetObject;
    private float _timer;
    private bool _isGiftStolen = false;

    void Update()
    {
        //if (_timer >= _nextMoveTime)
        //{
            TryStealGift();
        //    _timer = 0f;            
               

        //}
        //else
        //{
        //    _timer += Time.deltaTime;
            
        //}

        if(!_isGiftStolen)
        {

            MoveToTarget(_targetObject.transform);
        }
        else
        {
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
            //_targetObject = null;
            HideBehindObstacle();
            Debug.Log(_hideSpot);           

            
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
        
        Destroy(_targetObject);
        _isGiftStolen = true;
        _targetObject = null;
    }

    void HideBehindObstacle()
    {
        if (_player != null)
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, _hideRadius);
            foreach (Collider collider in colliders)
            {
                

                if (collider.CompareTag(_obstacleTag))
                {
                    
                    _hideSpot = collider.gameObject.transform;
                    break;
                }
            }

            //Vector3 playerDirection = _player.transform.position - transform.position;

            
            //Vector3 targetPoint = _player.transform.position - playerDirection.normalized * _hideRadius;

            //Debug.Log("player found");
            //RaycastHit hit;
            //if (Physics.Raycast(transform.position, playerDirection.normalized, out hit, _hideRadius, _obstacleLayer))
            //{
            //    Debug.Log("raycast found smth");
            //    if (hit.collider.CompareTag(_obstacleTag))
            //    {
            //        _hideSpot.position = hit.point;
            //    }
            //}

            
            //_nextMoveTime = Time.time + _moveCooldown;
        }
    }
}
