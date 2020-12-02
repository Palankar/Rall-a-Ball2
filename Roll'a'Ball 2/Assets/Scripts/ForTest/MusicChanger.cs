using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
    public GameObject musicPack;
    public GameObject changeTo;

    public List<Transform> pack;

    void Start()
    {
        pack = new List<Transform>();
        for (int i = 0; i < musicPack.transform.childCount; i++)
        {
            pack.Add(musicPack.transform.GetChild(i));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Entered");
            foreach (var song in pack)
            {
                if(song.Equals(changeTo))
                    song.gameObject.SetActive(true);
                else
                    song.gameObject.SetActive(false);
            }
    }
}
