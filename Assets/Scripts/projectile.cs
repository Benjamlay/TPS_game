using UnityEngine;
using UnityEngine.Serialization;

public class projectile : MonoBehaviour
{
    [SerializeField] private float bulletforce = 100;
    [FormerlySerializedAs("layerMask")] [SerializeField] private LayerMask aimLayer;
    private Camera _mainCamera;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera = Camera.main;
        Ray ray2 = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, _mainCamera.farClipPlane));

        Vector3 direction = ray2.direction;
        
        GetComponent<Rigidbody>().linearVelocity = direction * bulletforce;
        Destroy(gameObject, 0.6f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collision detected");
        Destroy(gameObject);
    }
}
