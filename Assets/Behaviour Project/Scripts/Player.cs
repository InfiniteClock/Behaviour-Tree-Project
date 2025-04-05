using NodeCanvas.Tasks.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public GameObject projectile;
    public float shotCooldown;

    private Vector3 velocity;
    private Rigidbody rb;
    private float shotTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input from directional keys for 8-way movement
        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");

        // Clamp velocity to moveSpeed so diagnal movement is not faster
        velocity = new Vector3(hMove * moveSpeed, 0, vMove * moveSpeed);
        velocity = Vector3.ClampMagnitude(velocity, moveSpeed);
        rb.velocity = velocity;

        // Have the player look at the mouse
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 mousePoint;
        if (Physics.Raycast(ray, out hit))
        {
            mousePoint = hit.point;
            Vector3 dirToMouse = mousePoint - transform.position;
            dirToMouse.y = 0;
            rb.rotation = Quaternion.LookRotation(dirToMouse, Vector3.up);
        }

        if (Input.GetMouseButton(0) && shotTimer > shotCooldown)
        {
            // Create a projectile in the right location
            GameObject shot = GameObject.Instantiate(projectile);
            Vector3 pos = transform.position;
            shot.transform.position = pos;
            // Set the direction of the projectile towards the target
            shot.transform.forward = transform.forward;
            shotTimer = 0;
        }
        else
        {
            shotTimer += Time.deltaTime;
        }
    }
}
