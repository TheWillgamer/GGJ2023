using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        int verMov = 0;
        int horMov = 0;

        if (Input.GetKey(KeyCode.W))
        {
            verMov++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            verMov--;
        }
        if (Input.GetKey(KeyCode.A))
        {
            horMov--;
        }
        if (Input.GetKey(KeyCode.D))
        {
            horMov++;
        }

        Vector3 direction = new Vector3(horMov, 0, verMov).normalized;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
