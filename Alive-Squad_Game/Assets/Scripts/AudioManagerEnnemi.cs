using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerEnnemi : MonoBehaviour
{
    public GameObject Ennemi;
    private float rangeSon = 6;

    private Transform target;
    private float bestdisttoplayer = 999;
    private GameObject[] objs;
    public AudioClip playlist;
    public AudioSource audioSource;

    void Start()
    {
        audioSource.clip = playlist;
    }

    void Update()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length > 0)
        {
            bestdisttoplayer = Vector2.Distance(Ennemi.transform.position, objs[0].transform.position);
            target = objs[0].transform;
            foreach(GameObject ob in objs)
            {
                if (Vector2.Distance(Ennemi.transform.position, ob.transform.position) < bestdisttoplayer)
                {
                    bestdisttoplayer = Vector2.Distance(Ennemi.transform.position, ob.transform.position);
                    target = ob.transform;
                }
            }
        }
        if (bestdisttoplayer < rangeSon)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
