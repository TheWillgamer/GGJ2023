using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform player;
    private Vector3 distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        distanceFromPlayer = new Vector3(0f, 15f, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + distanceFromPlayer;
    }
}
