using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowerSimple : MonoBehaviour
{
    public GameObject player;
    public float MouseSens = 0.14f;

    private Vector3 offset;
    private PlayerControllerSimple controller;

    void Start()
    {
        offset = transform.position - player.transform.position;
        controller = player.GetComponent<PlayerControllerSimple>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + (offset * controller.cameraRange);
        transform.Rotate(0, controller.getCameraMovementX() * MouseSens, 0);
    }
}
