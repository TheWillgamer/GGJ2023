using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 1;
        RaycastHit hit;

        Vector3 relLoc = new Vector3(0f, 0f, 0f);
        if (Physics.Raycast(ray, out hit, 30, layerMask))
        {
            relLoc = hit.point - transform.position;
        }

        relLoc.y = 0f;
        transform.rotation = Quaternion.LookRotation(relLoc, Vector3.up);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, b.x - a.x) * Mathf.Rad2Deg;
    }
}
