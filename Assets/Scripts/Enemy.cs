using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;

    private int _currentHealth;
    protected PlayFX _playFX;
    protected GameObject _player;
    [SerializeField] protected float _hideRadius = 5f;
    protected Transform _hideSpot;
    protected GameObject _lastHided;
    public bool _isPatroling = true;
    protected Transform _target;

    public Transform Target { get; private set; }

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _playFX = GetComponent<PlayFX>();        
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            _playFX.PlayDeathEffect(transform.position);
            foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = false;
            }
            Destroy(gameObject, 1f);
        }
        else
        {
            _playFX.PlayDamageEffect(transform.position);
        }
    }

    protected void MoveToTarget(Transform target)
    {
        if (target != null)
        {
            _isPatroling = false;
            transform.LookAt(target);
            Target = target;
            //transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);            
        }
        else
        {
            _isPatroling = true;
        }
    }

    

    protected void HideBehindObstacle()
    {
        
            Collider[] colliders = Physics.OverlapSphere(transform.position, _hideRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<TreeShelter>() && collider.gameObject != _lastHided)
            {
                _hideSpot = collider.gameObject.transform;
                MoveToTarget(_hideSpot);
                //_isPatroling = false;
                break;
            }
        }

        
    }

    protected void TryFindPlayer(float searchRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PlayerMovement>())
            {
                Physics.IgnoreCollision(collider, GetComponent<NavMeshAgent>().GetComponent<Collider>(), true);
                
                if (gameObject.GetComponent<AnvilEnemy>())
                {
                    _target = collider.transform;
                    Target = _target;
                }
                else if (gameObject.GetComponent<TiefEnemy>())
                {
                    HideBehindObstacle();
                }
                //_isPatroling = false;

                break;
            }
            else _isPatroling = true;
            //if(collider.GetComponent<AnvilEnemy>() )
            //{
            //    Physics.IgnoreCollision(collider, GetComponent<Collider>(), true);
            //}
            
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<TreeShelter>())
        {
            _lastHided = collision.gameObject;
            _hideSpot = null;
        }
        else if (collision.collider.GetComponent<Snowball>())
        {            
            TakeDamage(1);
        }

    }
}
