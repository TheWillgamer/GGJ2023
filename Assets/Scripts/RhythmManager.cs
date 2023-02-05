using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    // Note Detection
    public Transform leftNote;
    public Transform rightNote;

    // Spawning
    public Transform hitLoc;        // where the notes collide
    public Transform leftSpawner;
    public Transform rightSpawner;
    public Transform note;
    public float spawnRate;
    public float timer;
    public bool activated;           // needs to be true for notes to start spawning

    void Start()
    {
        activated = false;
    }

    void Update()
    {
        if (activated)
            SpawnNotes();
        UpdateClosestNodes();
    }

    void SpawnNotes()
    {
        timer += Time.deltaTime;

        if (timer > spawnRate)
        {
            timer -= spawnRate;
            var lNote = Instantiate(note, leftSpawner.position, Quaternion.identity);
            var rNote = Instantiate(note, rightSpawner.position, Quaternion.identity);
            lNote.transform.SetParent(hitLoc);
            rNote.transform.SetParent(hitLoc);
            lNote.transform.localScale = new Vector3(1f, 1f, 1f);
            rNote.transform.localScale = new Vector3(1f, 1f, 1f);
            lNote.GetComponent<NoteBehavior>().right = true;
        }
    }

    void UpdateClosestNodes()
    {
        leftNote = null;
        rightNote = null;
        for(int i = 0; i < hitLoc.childCount; i++)
        {
            Transform hit = hitLoc.GetChild(i);

            // For closest left node
            if(hit.localPosition.x < 0f && hit.localPosition.x > -30f)
                leftNote = hit;

            // For closest right node
            else if (hit.localPosition.x > 0f && hit.localPosition.x < 30f)
                rightNote = hit;
        }
    }
}
