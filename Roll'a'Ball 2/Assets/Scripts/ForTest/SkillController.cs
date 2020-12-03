using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkillController : MonoBehaviour
{
    public float speed = 200;
    public float cameraDistance;
    public float cameraSpeed;
    public float power;

    private bool isCharge = false;
    //private Vector3 cameraVector;
    private Animator camShake;

    private void Awake()
    {
        //cameraVector = Camera.main.transform.forward;
        //cameraStartPos = Camera.main.transform.localPosition.z;
        camShake = Camera.main.GetComponent<Animator>();
    }

    public void ChargePrepare()
    {
        this.isCharge = true;
        //cameraStartPos = Camera.main.transform.localPosition.z;
        camShake.SetTrigger("Shakes");
    }

    public void StopCharge()
    {
        this.isCharge = false;
        camShake.SetTrigger("StopShaking");

        gameObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * power);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            ChargePrepare();
        if (Mouse.current.leftButton.wasReleasedThisFrame)
            StopCharge();

        //При активном Charge - кручение объекта
        if (isCharge)
            gameObject.transform.Rotate(new Vector3(speed, 0, 0) * Time.deltaTime);

        //При активном Charge - отдаление камеры (по translate - не работает, если Animator использует position)
        /*
        if (isCharge && Camera.main.transform.localPosition.z > cameraStartPos - cameraDistance)
            Camera.main.transform.Translate(cameraVector, Space.Self);
        else if (!isCharge && Camera.main.transform.localPosition.z < cameraStartPos)
            Camera.main.transform.Translate(-cameraVector * 10, Space.Self);
        */
    }
}
