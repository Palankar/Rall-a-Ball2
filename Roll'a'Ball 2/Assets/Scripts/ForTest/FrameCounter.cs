using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameCounter : MonoBehaviour
{
    private int FPS;
    private TextMeshProUGUI fpsText;

    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(fixedChanger(1));
    }

    void Update()
    {
        FPS = (int)(60/Time.deltaTime);
    }

    //Чтобы обновление происходило раз в секунду, а не нонстоп
    private IEnumerator fixedChanger(int sec)
    {
        while (true)
        {
            yield return new WaitForSeconds(sec);
            fpsText.text = "FPS: " + FPS;
        }
    }
}
