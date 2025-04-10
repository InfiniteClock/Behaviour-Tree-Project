using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float lifetime;
    public LayerMask target;
    public LayerMask sword;

    private Vector3 velocity;
    private float timer = 0;
    private bool deflected = false;
    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Set the projectile to move forwards while alive, and disappear after its lifetime is over
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
        // Have to bit convert the layer to compare to layermask
        if ((1 << collision.gameObject.layer) == target.value)
        {
            // Reduce the health of the hit target, if possible
            if (collision.gameObject.TryGetComponent<Health>(out Health hitTargetHP))
            {
                hitTargetHP.TakeDamage(damage);
            }
        }
        // Deflect off of the sword
        if ((1 << collision.gameObject.layer) == sword.value)
        {
            // It deflects the projectile only if it hasn't been deflected before
            if (!deflected)
            {
                Vector3 newDirection = collision.transform.position - transform.position;
                newDirection.y = 0f;
                velocity = -newDirection.normalized * speed*2;
                deflected = true;
            }
        }
        else
        {
            // Projectile disappears on any collision EXCEPT the sword
            // Note that projectiles will not collide with layers that match their spawner
            Destroy(gameObject);
        }
    }
}
