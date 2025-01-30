using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class RayCastShooting : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100.0f;
    //[SerializeField] private Transform redSpot;
    [FormerlySerializedAs("layerMask")] [SerializeField] private LayerMask aimLayer;
    [SerializeField] private float damageRate = 50f;
    
    [SerializeField] private CinemachineCamera AimCamera;
    [SerializeField] private GameObject aimingPanel;
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject spotter;
    
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    //[SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource gunShot;
    private Coroutine Shoot_Routine = null;
    private float timeBetweenShots = 0.2f;
    private float lastShotTime;
    
    private Camera _mainCamera;
    private bool _isShooting;
    private bool _isAiming;
    private Transform _startSpotter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
        aimingPanel.SetActive(false);
        _startSpotter = spotter.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        lastShotTime += Time.deltaTime;
        
        AimCamera.Priority = _isAiming ? 100 : 0;
        aimingPanel.SetActive(_isAiming);
        
         Ray ray2 = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));
        // Debug.DrawRay(ray2.origin, ray2.direction * rayDistance, Color.red);
        
        if (_isAiming)
        {
            //Ray ray = new Ray(Gun.gameObject.transform.position, ray2.direction);
            //Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
           //Debug.Log("aiming");

            if(_isShooting && lastShotTime >= timeBetweenShots)
            {
                if (Shoot_Routine != null)
                {
                    StopCoroutine(Shoot_Routine);
                }
                Shoot_Routine = StartCoroutine("Fire");
                
                gunShot.Play();
                lastShotTime = 0f;
                
                //muzzleFlash.Play();
                if (Physics.Raycast(ray2, out RaycastHit hit, _mainCamera.farClipPlane, aimLayer))
                {
                    //spotter.SetActive(true);
                   //spotter.transform.position = hit.point;
                    if (hit.collider.TryGetComponent(out Target target))
                    {
                        target.TakeDamage(damageRate);
                    }
                }
                else
                {
                    spotter.gameObject.SetActive(false);
                }
            }
            else
            {
                StopCoroutine("Fire");
                Shoot_Routine = null;
            }
        }
        
        
    }

    IEnumerator Fire()
    {
        while(true)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(fireRate);
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
