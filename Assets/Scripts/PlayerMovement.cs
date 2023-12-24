using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("Ground Check Settings")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 0.1f;

    [Header("Health wtf")]
    [SerializeField] private int _maxHealth;

    private PlayFX _playFX;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private float _timer;
    private int _currentHealth;
    

    void Awake()
    {
        _currentHealth = _maxHealth;
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _playFX = GetComponent<PlayFX>();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
        HandleJumpInput();
        RotateTowardsCursor();
        
    }

    void HandleMovementInput()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        _rigidbody.velocity = new Vector3(movement.x * _moveSpeed, _rigidbody.velocity.y, movement.z * _moveSpeed);

        if (horizontal != 0f || vertical != 0f)
        {
            WaitForStep();
            _animator.SetBool("IsMoving", true);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }

    }
    

    private void WaitForStep()
    {
        if(_timer >= 0.3f)
        {            
            _timer = 0;
            _playFX.PlayStepEffect(transform.position);

        }
        _timer += Time.fixedDeltaTime;
    }

    void HandleJumpInput()
    {
        _isGrounded = CheckIfGrounded();
        if (_isGrounded && Input.GetButton("Jump"))
        {
            Debug.Log(_isGrounded);

            Jump();
        }
    }

    bool CheckIfGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }



    void RotateTowardsCursor()
    {
        Vector3 targetDirection = CursorPosition.GetMouseTargetDirection(transform);
        
        RotatePlayer(targetDirection);
    }

    void RotatePlayer(Vector3 targetDirection)
    {
        if (targetDirection != Vector3.zero)
        {
            
            float angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;            
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
            //Debug.Log(targetDirection);
            float step = _rotationSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, step);
        }
    }

    public void GetDamage()
    {
        _currentHealth--;
        if(_currentHealth <= 0) 
        {
            _playFX.PlayDeathEffect(transform.position);
            Destroy(gameObject, 0.3f);
        }
    }
}
