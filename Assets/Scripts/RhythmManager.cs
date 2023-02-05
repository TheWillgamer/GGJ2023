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
    public float bpm;
    public float spawnDelay;
    private float spawnRate;
    private float timer;

    void Start()
    {
        timer = 0f - spawnDelay;
        spawnRate = 1f / (bpm / 60);
    }

    // Update is called once per frame
    void Update()
    {
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
        for(int i = 0; i < hitLoc.childCount; i++)
        {
            Transform hit = hitLoc.GetChild(i);

            // For closest left node
            if(hit.localPosition.x < 0f && hit.localPosition.x > -100f && (leftNote == null || leftNote.localPosition.x < hit.localPosition.x))
                leftNote = hit;

            // For closest right node
            else if (hit.localPosition.x > 0f && hit.localPosition.x < 100f && (rightNote == null || rightNote.localPosition.x > hit.localPosition.x))
                rightNote = hit;
        }

        Debug.Log(leftNote);
    }
}
