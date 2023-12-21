using UnityEngine;

public class SnowballThrower : MonoBehaviour
{
    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] protected Transform snowballPoint;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float throwAngle = 45f;
    private Vector3 throwPoint;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ThrowSnowball();
        }
    }

    

    void ThrowSnowball()
    {       
            
        GameObject snowball = Instantiate(snowballPrefab, snowballPoint.position, Quaternion.identity);            
        Rigidbody rb = snowball.GetComponent<Rigidbody>();

        if (rb != null)
        {

            Vector3 throwDirection = CalculateThrowDirection(transform, CursorPosition.GetMousePosition(), throwAngle);
            //Vector3 throwDirection = (hit.point - transform.position).normalized;

            Vector3 throwVelocity = throwDirection * throwForce;

            rb.velocity = throwVelocity;
        }
        
    }

    Vector3 CalculateThrowDirection(Transform origin, Vector3 target, float angle)
    {
        
        Vector3 direction = target - origin.position;
        direction.y = 0f; 
        float distance = direction.magnitude;

        float radianAngle = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(radianAngle);

        
        //direction.Normalize();

        return direction;
    }
}
