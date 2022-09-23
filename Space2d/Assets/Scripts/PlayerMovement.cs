using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D ship;
    public float moveSpeed = 100f;
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
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("SpacePressed", false);
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(moveHorizontal, moveVertical);
        inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        ship.velocity = Vector2.ClampMagnitude(ship.velocity, maxVelocity);
        Debug.Log(Input.GetKey(KeyCode.Space));
        if (Input.GetKey("space"))
        {
            animator.SetBool("SpacePressed", true);

            if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
            {
                ship.AddForce(movementDirection * moveSpeed * inputMagnitude * Time.deltaTime);
            }
        }

    }

    private void FixedUpdate()
    {
        Vector3 currentRotation = this.transform.localEulerAngles;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);

        if (movementDirection != Vector2.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        }
    }
}
