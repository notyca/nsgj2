using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask excludeEnemy;
    [SerializeField] private LayerMask excludeNothing;
    [SerializeField] private SpriteMask cooldownBarMask;

    [SerializeField] private GameObject crosshair;
    private float crosshairSpeed = 15.0f;

    private Animator animator;

    private Rigidbody2D rb;

    private Vector2 inputVector;
    private float speed = 5.0f;
    private float dashSpeed = 40.0f;
    public bool dashing = false;
    private bool coolingDown = false;

    private bool bulletTime = false;
    private float bulletTimeSlowDown = 0.1f;

    private Vector2 direction = Vector2.up;

    private bool movementEnabled = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movementEnabled)
        {
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

        if (!coolingDown && Input.GetKeyDown(KeyCode.Space))
        {
            bulletTime = true;

            crosshair.transform.localPosition = new Vector2(0, 0);
            crosshair.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (bulletTime && Input.GetKeyUp(KeyCode.Space))
        {
            bulletTime = false;

            crosshair.GetComponent<SpriteRenderer>().enabled = false;

            Dash(crosshair.transform.localPosition.normalized);
            return;
        }

        if (inputVector != Vector2.zero)
        {
            inputVector = inputVector.normalized;

            direction = inputVector;
        }

        // move player
        if (bulletTime)
        {
            rb.linearVelocity = Vector2.zero;

            crosshair.transform.localPosition += (Vector3)(inputVector * crosshairSpeed * Time.deltaTime);
            direction = crosshair.transform.localPosition.normalized;
        }
        else
        {
            rb.linearVelocity = inputVector * speed;
        }

        animator.SetFloat("horizontal", direction.x);
        animator.SetFloat("vertical", direction.y);
    }

    public void DisableMovement()
    {
        rb.linearVelocity = Vector2.zero;
        movementEnabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public bool IsInBulletTime()
    {
        return bulletTime;
    }

    public float GetBulletTimeSlowDown()
    {
        return bulletTimeSlowDown;
    }

    private void Dash(Vector2 dashDirection)
    {
        if (dashDirection == Vector2.zero)
        {
            return;
        }
        DisableMovement();
        StartCoroutine(Dashing(dashDirection));
    }

    private IEnumerator Dashing(Vector2 dashDirection)
    {
        dashing = true;
        GetComponent<Rigidbody2D>().excludeLayers = excludeNothing;
        GetComponent<Rigidbody2D>().linearVelocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(0.08f);
        EnableMovement();
        GetComponent<Rigidbody2D>().excludeLayers = excludeEnemy;
        StartCoroutine(Cooldown());
        dashing = false;
    }

    private IEnumerator Cooldown()
    {
        coolingDown = true;
        float timePassed = 0;
        while(timePassed < 3)
        {
            cooldownBarMask.alphaCutoff = (timePassed / 3);
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
        }
        coolingDown = false;
    }
}
