using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public float force;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody playerRb = other.GetComponent<Rigidbody>();
        playerRb.AddForce(-transform.right * force);
    }
}
