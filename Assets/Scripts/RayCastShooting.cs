using UnityEngine;

public class RayCastShooting : MonoBehaviour
{
    [SerializeField] private float rayDistance = 100.0f;
    [SerializeField] private Transform redSpot;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float damageRate = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);


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
