using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MovementScript : MonoBehaviour
{
    // movement variables
    public float speed;
    public float drag;

    // direction (for camera and movement calculations)
    public Transform direction;
    
    // ground properties
    public LayerMask Ground;
    private bool onGround;

    // player's rigidbody
    Rigidbody rB;

    // annoying dash variables
    public int maxDashes = 3;
    public float dashCooldown = 0.25f;
    public float dashRecharge = 3f;
    private int currentDashes;
    private bool onCooldown;

    // jump variables
    public float jumpForce = 500f;
    private bool canJump = true;

    private void Start()
    {
        rB = GetComponent<Rigidbody>();
        currentDashes = maxDashes;
        onCooldown = false;
    }

    private void Update()
    {
        onGround = Physics.Raycast(transform.position, Vector3.down, transform.localScale.y, Ground);
        rB.drag = onGround ? drag : 0;

        if (!onGround)
        {
            Vector3 rawVelocity = new Vector3(rB.velocity.x, 0f, rB.velocity.z);

            if (rawVelocity.magnitude > speed)
            {
                Vector3 normVelocity = rawVelocity.normalized * speed;
                rB.velocity = new Vector3(normVelocity.x, rB.velocity.y, normVelocity.z);
            }
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = direction.forward * verticalInput + direction.right * horizontalInput;

        if (currentDashes > 0 && !onCooldown)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                currentDashes--;
                rB.AddForce(moveDirection.normalized * 1000f * 10f, ForceMode.Force);
                StartCoroutine(StartCooldown());
                StartCoroutine(StartRecharge());
            }
        }

        if (onGround && Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rB.AddForce(Vector3.up * jumpForce, ForceMode.Force);
            canJump = false;
        }

        if (onGround)
        {
            canJump = true;
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = direction.forward * verticalInput + direction.right * horizontalInput;
        rB.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    private IEnumerator StartCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        onCooldown = false;
    }
    private IEnumerator StartRecharge()
    {
        yield return new WaitForSeconds(dashRecharge);
        currentDashes++;
    }
}