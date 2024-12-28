using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private Animator animator;

    private Rigidbody2D rb;
    private Vector2 inputVector;
    private float speed = 5.0f;

    private Vector2 direction = Vector2.up;

    private bool movementEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        print("twoh");
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementEnabled)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // input
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        else
        {
            inputVector.y = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        else
        {
            inputVector.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

        if (inputVector != Vector2.zero)
        {
            inputVector = inputVector.normalized;

            direction = inputVector;
        }

        // move player
        rb.linearVelocity = inputVector * speed;

        animator.SetFloat("horizontal", direction.x);
        animator.SetFloat("vertical", direction.y);
    }

    public void DisableMovement()
    {
        movementEnabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hey//");
    }
}
