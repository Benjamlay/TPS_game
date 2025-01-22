using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MovementStateManager : MonoBehaviour
{
    [SerializeField] public float _moveSpeed = 1;
    [SerializeField] public float _runSpeed = 2;
    
    private Vector2 _move;
    private bool _isRunning;
    private bool _isAiming;
    bool runToggle = false;

    [SerializeField]  float _groundYOffset;
    [SerializeField] private LayerMask _groundMask;
    
    [SerializeField] float gravity = -9.81f;
    [SerializeField] private float _rotationSpeed = 10f;
    private Vector3 _velocity;
    private Vector3 _spherePos;
    
    private Animator _animator;
    CharacterController _controller;
    
    //[SerializeField] CinemachineCamera _virtualCamera;
    //private Transform _cameraTransform;
    
    [HideInInspector] public Vector3 _direction;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        //_cameraTransform = _virtualCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
    }

    void GetDirectionAndMove()
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        
        Vector3 cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        
        
        _direction = cameraForward * _move.y + cameraRight * _move.x;
        //_direction.Normalize();
        
        float speed = _isRunning ? _runSpeed : _moveSpeed;
        
        _controller.Move(_direction * (speed * Time.deltaTime));
        

        if (_direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        UpdateAnimator(_direction);
    }

    void UpdateAnimator(Vector3 direction)
    {
        Vector3 localMove = transform.InverseTransformDirection(direction);

        _animator.SetFloat("vInput", localMove.z);
        _animator.SetFloat("hzInput", localMove.x);
        
        bool isWalking = direction.magnitude > 0.1f;
        bool isRunning = isWalking && _isRunning;

        if (isWalking)
        {
            _isRunning = runToggle;
        }
        else
        {
            _isRunning = false;
            runToggle = false;
        }
        _animator.SetBool("Walking", isWalking);
        _animator.SetBool("Running", isRunning);
    }
    bool IsGrounded()
    {
        _spherePos = new Vector3(transform.position.x, transform.position.y - _groundYOffset, transform.position.z);
        if(Physics.CheckSphere(_spherePos, _controller.radius - 0.05f, _groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if(!IsGrounded()) _velocity.y += gravity * Time.deltaTime;
        else if(_velocity.y < 0) _velocity.y = -2;
        
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawWireSphere(_spherePos, _controller.radius - 0.05f);
    }
    
    public void MoveInputs(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    public void IsRunning(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
            _isRunning = context.ReadValueAsButton();
            runToggle = !runToggle;
        }
        
        _isRunning = runToggle;
        
    }
}
