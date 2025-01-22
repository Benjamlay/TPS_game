using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCastShooting : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100.0f;
    [SerializeField] private Transform redSpot;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float damageRate = 50f;
    [SerializeField] private CinemachineCamera AimCamera;
    
    private bool _isShooting;
    private bool _isAiming;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        AimCamera.Priority = _isAiming ? 100 : 0;
        
        if (_isShooting)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
            Debug.Log("shooting");

            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, layerMask))
            {
                redSpot.gameObject.SetActive(true);
                redSpot.position = hit.point;
            
                if(hit.collider.TryGetComponent(out Target target))
                {
                    target.TakeDamage(damageRate * Time.deltaTime);
                }
            
            }
            else
            {
                redSpot.gameObject.SetActive(false);
            }
        }
        
    }

    public void IsShooting(InputAction.CallbackContext context)
    {
        _isShooting = context.ReadValueAsButton();
    }
    public void IsAiming(InputAction.CallbackContext context)
    {
        _isAiming = context.ReadValueAsButton();
    }
}
