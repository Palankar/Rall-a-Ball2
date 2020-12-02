using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System;

public class PlayerControllerSimple : MonoBehaviour
{
    public float speed = 0;
    public float fallSpeed;
    public Boolean rotateCamera;
    public float cameraRange = 10;

    private Rigidbody rb;
    private Vector3 rawMovement;
    private float cameraMovementX;   //Значений не ставим, это для получения значения скриптом объекта камеры
    private float cameraWheel;
    private PlayerInput playerInput;
    private Boolean onGound;
    private SkillController skillController;
    private InputActions controls;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        skillController = GetComponent<SkillController>();
    }
    
    //Получаем вектор движения мыши вбок
    void OnLook(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        cameraMovementX = movementVector.x;
    }
    
    //Получаем вектор движения нажатия клавиш
    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        rawMovement = new Vector3(movementVector.x, 0, movementVector.y);
    }

    //Получаем движение колесика мыши
    void OnScroll()
    {
        //Находим нужный Action, если он срабатывает, то cameraWheel наращивает себе значение от движения
        playerInput.actions.FindAction("Scroll").performed += x => cameraWheel = x.ReadValue<float>();
    }

    private void Update()
    {
        if (cameraWheel > 0 && cameraRange >= 10)
            cameraRange -= 10;
        if (cameraWheel < 0 && cameraRange <= 180)
            cameraRange += 10;
    }

    void FixedUpdate()
    {
        if(onGound)
        {
            if (rawMovement.x != 0 && rawMovement.z != 0)
                //Потому что из-за сложения векторов движение по диагонали было сильно быстрее прямого
                rb.AddForce((forX(rawMovement) + forY(rawMovement)) / 1.5f * speed);
            else
                rb.AddForce((forX(rawMovement) + forY(rawMovement)) * speed);
        }
        rb.AddForce(Vector3.down * fallSpeed);
    }

    //Катим, когда на земле
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGound = true;
    }

    //Оторвались от земли - не катим
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            onGound = false;
    }

    //Получаем вектор движения относительно камеры по X
    private Vector3 forX(Vector3 vector)
    {
        if (vector.x > 0)
            return new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        if (vector.x < 0)
            return new Vector3(-Camera.main.transform.right.x, 0, -Camera.main.transform.right.z);
        else
            return new Vector3(0, 0, 0);
    }

    //Получаем вектор движения относительно камеры по Y
    private Vector3 forY(Vector3 vector)
    {
        if (vector.z > 0)
            return new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        if (vector.z < 0)
            return new Vector3(-Camera.main.transform.forward.x, 0, -Camera.main.transform.forward.z);
        else
            return new Vector3(0, 0, 0);
    }
    public float getCameraMovementX()
    {
        return cameraMovementX;
    }

    //Смена музыки при входе в триггер сменятеля
    //TODO: перенести всю логику в MusicChanger, оставив тут только вызов метода
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MusicChanger"))
        {

            Debug.Log("Entered");
            MusicChanger changer = other.GetComponent<MusicChanger>();

            foreach (var song in changer.pack)
            {
                if (song.name.Equals(changer.changeTo.name))
                    song.gameObject.SetActive(true);
                else
                    song.gameObject.SetActive(false);
            }
        }
            
    }
}
