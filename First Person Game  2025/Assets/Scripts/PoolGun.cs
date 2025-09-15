using UnityEngine;
using UnityEngine.InputSystem;

public class PoolGun : MonoBehaviour
{

    public PoolManager pool;
    public Transform firePoint;

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        GameObject bullet = pool.GetObject();

        if (bullet == null) return;

        bullet.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
        var rb = bullet.GetComponent<Rigidbody>();
        if (rb) rb.linearVelocity = firePoint.forward * 20f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
