using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D ship;
    private float moveSpeed = 100f;
    public float maxVelocity = 10f;
    public float rotationSpeed = 90f;
    public Animator animator;
    private float moveHorizontal;
    private float moveVertical;
    private Vector2 movementDirection;
    private float inputMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        ship = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Blend", 0);
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(moveHorizontal, moveVertical);
        inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        ship.velocity = Vector2.ClampMagnitude(ship.velocity, maxVelocity);

        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            if (Input.GetKey("a"))
            {
                animator.SetFloat("Blend", 1);
            }
            if (Input.GetKey("w"))
            {
                animator.SetFloat("Blend", 5);
            }
            if (Input.GetKey("d"))
            {
                animator.SetFloat("Blend", 10);
            }


            ship.AddRelativeForce(movementDirection * moveSpeed * inputMagnitude * Time.deltaTime);
            //animator.SetFloat("Blend", 0.5f);
        }

        var mouseLocation = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(mouseLocation.y, mouseLocation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);


    }

    private void FixedUpdate()
    {
       /* Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);

        if (movementDirection != Vector2.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        } */
    }

}
