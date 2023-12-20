using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 2;
    [SerializeField] protected float _movementSpeed = 10f;
    private int _currentHealth;
    private PlayFX _playFX;
    protected GameObject _player;
    [SerializeField] protected float _hideRadius = 5f;
    protected Transform _hideSpot;
    protected GameObject _lastHided;
    public bool _isPatroling = true;
    //protected GameObject _targetObject;

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
            Destroy(gameObject, 0.3f);
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

                    _hideSpot = collider.gameObject.transform;
                    break;
                }
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
