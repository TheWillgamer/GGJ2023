using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public bool right;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (right)
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        else
            transform.Translate(-Vector3.right * speed * Time.deltaTime, Space.World);
    }
}
