using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float attackRange;
    public Transform Explosion;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 relLoc = player.position - transform.position;
        relLoc.y = 0;
        transform.position = transform.position + transform.forward * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(relLoc, Vector3.up), turnSpeed * Time.deltaTime);

        if (relLoc.magnitude < 1.3f)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        
    }
}
