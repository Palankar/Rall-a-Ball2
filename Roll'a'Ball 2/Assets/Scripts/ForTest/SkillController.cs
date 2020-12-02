using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillController : MonoBehaviour
{
    private float speed = 200;
    private bool rotation = false;

    public void Rotate()
    {
        this.rotation = true;
    }

    public void StopRotating()
    {
        this.rotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            Rotate();
        if (Mouse.current.leftButton.wasReleasedThisFrame)
            StopRotating();

        if (rotation)
            gameObject.transform.Rotate(new Vector3(speed, 0, 0) * Time.deltaTime);
    }
}
