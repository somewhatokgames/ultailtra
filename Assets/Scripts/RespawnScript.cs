using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public GameObject player;

    void FixedUpdate()
    {
        if (player.transform.position.y <= -5)
        {
            player.transform.position = new Vector3(0, 3, 0);

            Rigidbody pRB = player.GetComponent<Rigidbody>();
            pRB.velocity = Vector3.zero;
        }
    }
}
