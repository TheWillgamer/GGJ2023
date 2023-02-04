using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public Transform leftSpawner;
    public Transform rightSpawner;
    public Transform note;
    public float bpm;
    private float timer;

    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > bpm / 60)
        {
            timer -= bpm / 60;
            var lNote = Instantiate(note, leftSpawner.position, Quaternion.identity);
            lNote.transform.parent = leftSpawner;
            var rNote = Instantiate(note, rightSpawner.position, Quaternion.identity);
            rNote.transform.parent = leftSpawner;
        }
    }
}
