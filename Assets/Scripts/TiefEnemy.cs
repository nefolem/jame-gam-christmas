using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

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
        
        

        if(!_isGiftStolen)
        {
            TryStealGift();
            MoveToTarget(_targetObject?.transform);
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
        _targetObject.SetActive(false);
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
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(_obstacleTag))
        {
            Hide(true);
        }
        else if (collision.collider.CompareTag("Snowball"))
        {
            TakeDamage(1);
            _targetObject?.SetActive(true);
            _targetObject.transform.position = transform.position;
        }
        else { return; }
    }


    public void Hide(bool isHiding)
    {
        foreach (MeshRenderer mr in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = !isHiding;
        }

        var ps = gameObject.GetComponentInChildren<ParticleSystem>();
        ps.enableEmission = !isHiding;
      
    }
}
