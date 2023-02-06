using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyParticle", 2f);
    }


    private void DestroyParticle(){
        Destroy(gameObject);
    }

    
}
