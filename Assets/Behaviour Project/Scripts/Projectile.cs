using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifetime;
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
}
