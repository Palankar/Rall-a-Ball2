using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceGenerator : MonoBehaviour
{
    private AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (float item in getPeriods())
            {
                StartCoroutine(WaitToStopAndPlay(item));
            }
        }
    }

    //Ожидает некоторое время перед остановкой звука
    private IEnumerator WaitToStop(int sec)
    {
        yield return new WaitForSeconds(sec);
        audio.Stop();
    }

    private IEnumerator WaitToPlay(int sec)
    {
        yield return new WaitForSeconds(sec);
        audio.Play();
    }

    private IEnumerator WaitToStopAndPlay(float sec)
    {
        yield return new WaitForSeconds(sec);
        audio.Stop();
        //yield return new WaitForSeconds(0.01f);
        audio.Play();
    }

    private float[] getPeriods()
    {
        float[] arr = new float[UnityEngine.Random.Range(3, 7)];
        Debug.Log("Per length " + arr.Length);
        for (int i = 0; i < arr.Length; i++)
        {
            if (i > 0)
                arr[i] = arr[i-1] + UnityEngine.Random.Range(0.12f, 0.25f);
            else
                arr[i] = UnityEngine.Random.Range(0.2f, 0.25f);

            Debug.Log("Period" + i + " " + arr[i]);
        }
        Array.Sort(arr);
        return arr;
    }
}
