using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    public float lifeSpan;
    public int damage;
    public int scorePerHit;
    public int meterGainOnHit;
    private float deathTime;
    private bool active;
    private GameplayManager gm;

    void Start()
    {
        active = true;
        deathTime = Time.time + lifeSpan;
        gm = GameObject.FindWithTag("EventSystem").GetComponent<GameplayManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > deathTime)
        {
            Destroy(gameObject);
        }
        if (active)
            MoveProjectile();
    }

    void MoveProjectile()
    {
        //Determine how far object should travel this frame.
        float travelDistance = (speed * Time.deltaTime);

        //Explode bullet if it goes through the wall
        RaycastHit hit;
        // Does the ray intersect any walls

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, travelDistance))
        {
            if (hit.transform.gameObject.tag == "Enemy")
            {
                //hit.transform.gameObject.GetComponent<EnemyHitDetection>().TakeDamage(damage);
                gm.addScore(scorePerHit);
                gm.meterGain(meterGainOnHit);
                Invoke("DestroyProjectile", 1.5f);
                active = false;
            }
        }

        transform.position += transform.forward * travelDistance;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
