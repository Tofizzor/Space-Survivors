using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    public float force = 100f;
    Vector2 direction;
    private float inputMagnitude;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        inputMagnitude = Mathf.Clamp01(direction.magnitude);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 10f);
        LookAtMouse();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.AddRelativeForce(force * inputMagnitude * Time.deltaTime * direction);
        if (Input.anyKeyDown == false)
        {
            rb.velocity = rb.velocity / 1.010f;
        }
    }

    void LookAtMouse()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

}
