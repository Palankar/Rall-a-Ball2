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
    public AudioSource castingSound;
    public int secToEndPreparation;

    private bool isCharge = false;
    private bool isPrepared = false;
    private Animator camShake;
    private int secOfPreparation = 0;

    private void Awake()
    {
        camShake = Camera.main.GetComponent<Animator>();
    }

    public void ChargePrepare()
    {
        this.isCharge = true;
        castingSound.Play();
        camShake.SetTrigger("Shakes");
        StartCoroutine(Preparing());
    }

    public void StopCharge()
    {
        this.isCharge = false;
        camShake.SetTrigger("StopShaking");
        castingSound.Stop();
        secOfPreparation = 0;
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

        //Проверяем таймер и зажатость кнопки
        if (secOfPreparation >= secToEndPreparation && isCharge)
        {
            isPrepared = true;
            castingSound.Stop();
            secOfPreparation = 0;
        }
    }

    //Потому что тут прикладываем силу
    private void FixedUpdate()
    {
        if (isPrepared && !isCharge)
        {
            gameObject.GetComponent<Rigidbody>()
                .AddForce(new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * power);
            isPrepared = false;
        }
    }

    //Таймер подготовки, будет считать до отпускания клавиши через isCharge или окончания отсчета
    private IEnumerator Preparing()
    {
        while(isCharge && secOfPreparation < secToEndPreparation)
        {
            yield return new WaitForSeconds(1f);
            secOfPreparation++;
        }

    }
}
