using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;

    private int _currentHealth;
    private PlayFX _playFX;
    protected GameObject _player;
    [SerializeField] protected float _hideRadius = 5f;
    protected Transform _hideSpot;
    protected GameObject _lastHided;
    public bool _isPatroling = true;


    private void Start()
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

    public Transform Target { get; private set; }

    protected void HideBehindObstacle()
    {
        if (_player != null)
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, _hideRadius);
            foreach (Collider collider in colliders)
            {

                if (collider.GetComponent<Tree>() && collider.gameObject != _lastHided)
                {
                Debug.Log(collider.gameObject);

                    _hideSpot = collider.gameObject.transform;
                    break;
                }
            }

        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Tree>())
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
