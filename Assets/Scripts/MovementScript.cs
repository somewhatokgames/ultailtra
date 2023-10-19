using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementScript : MonoBehaviour
{

    public float speed = 25f;
    public Transform direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = direction.forward * verticalInput + direction.right * horizontalInput; 
        Vector3 movement = moveDirection.normalized * speed * Time.deltaTime;
        transform.position += movement;
    }
}