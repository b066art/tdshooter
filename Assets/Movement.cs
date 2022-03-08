using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed = 2f;
    public float rotationSpeed = 5f;

    private float _horizontalInput;
    private float _verticalInput;

    Vector2 movement;

    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButton("Strafe"))
        {
            Vector2 movement = new Vector2(_verticalInput, -_horizontalInput);
            transform.Translate(movement * movementSpeed * Time.fixedDeltaTime);
        }
        else
        {
            Vector2 movement = new Vector2(_verticalInput, 0.0f);
            transform.Translate(movement * movementSpeed * Time.fixedDeltaTime);
            transform.Rotate(Vector3.forward * -_horizontalInput * rotationSpeed);
        }
    }
}