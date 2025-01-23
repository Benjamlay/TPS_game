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
    [SerializeField] private GameObject aimingPanel;
    [SerializeField] private GameObject Gun;
    private Camera _mainCamera;
    private bool _isShooting;
    private bool _isAiming;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
        aimingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        AimCamera.Priority = _isAiming ? 100 : 0;
        aimingPanel.SetActive(_isAiming);
        
         Ray ray2 = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
         Debug.DrawRay(ray2.origin, ray2.direction, Color.red);
        
        if (_isShooting)
        {
            Ray ray = new Ray(Gun.gameObject.transform.position, ray2.direction);
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
