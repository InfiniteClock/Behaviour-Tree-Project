using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float lifetime;
    public LayerMask target;

    private Vector3 velocity;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < lifetime)
        {
            timer += Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((1 << collision.gameObject.layer) == target.value)
        {
            if (collision.gameObject.TryGetComponent<Health>(out Health hitTargetHP))
            {
                hitTargetHP.TakeDamage(damage);
            }
        }
        // Projectile disappears on any collision. Ensure it does not collide with spawner
        Destroy(gameObject);
    }
}
