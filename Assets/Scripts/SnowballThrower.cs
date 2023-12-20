using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] protected Transform snowballPoint;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float throwAngle = 45f;
    private Vector3 throwPoint;

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //GetMousePosition();
            //throwPoint = CursorPosition.GetMouseTargetDirection(transform);
            ThrowSnowball();
        }
    }

    void GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        throwPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        Debug.Log(throwPoint);
    }

    void ThrowSnowball()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit))
        {
            
            GameObject snowball = Instantiate(snowballPrefab, snowballPoint.position, Quaternion.identity);

            
            Rigidbody rb = snowball.GetComponent<Rigidbody>();

            if (rb != null)
            {
                
                Vector3 throwDirection = CalculateThrowDirection(transform, hit.point, throwAngle);

                
                Vector3 throwVelocity = throwDirection * throwForce;

                
                rb.velocity = throwVelocity;
            }
        }
    }

    Vector3 CalculateThrowDirection(Transform origin, Vector3 target, float angle)
    {
        
        Vector3 direction = target - origin.position;
        direction.y = 0f; 
        float distance = direction.magnitude;

        float radianAngle = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(radianAngle);

        
        direction.Normalize();

        return direction;
    }
}
