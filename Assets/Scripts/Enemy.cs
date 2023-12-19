using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;
    [SerializeField] protected float _movementSpeed = 10f;
    protected Rigidbody _rigidbody;
    private int _currentHealth;
    private PlayFX _playFX;
    protected GameObject _player;
    //protected GameObject _targetObject;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _rigidbody = GetComponent<Rigidbody>();
        _playFX = GetComponent<PlayFX>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        if (_currentHealth <= 0)
        {
            _playFX.PlayDeathEffect(transform.position);
            Destroy(gameObject);
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
           
            transform.LookAt(target);
            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                target.position = Vector3.zero;
            }
        }
    }


    protected void RotateEnemy(Vector3 targetDirection)
    {
        if (targetDirection != Vector3.zero)
        {
            Vector3 deltaPosition = targetDirection - transform.position;
            float angle = Mathf.Atan2(deltaPosition.x, deltaPosition.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
        }
    }
}
