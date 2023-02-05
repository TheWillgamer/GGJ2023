using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NoteBehavior : MonoBehaviour
{
    public bool right;
    public float speed;
    public float timeToDisappear;
    private bool disappearing;
    private float timer;

    void Start()
    {
        disappearing = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (right)
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        else
            transform.Translate(-Vector3.right * speed * Time.deltaTime, Space.World);

        if (!disappearing && ((right && transform.localPosition.x > 0f) || (!right && transform.localPosition.x < 0f)))
        {
            disappearing = true;
            timer = 0f;
            StartCoroutine(Fade());
        }

        if (disappearing && timer > timeToDisappear)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Fade()
    {
        Color c = GetComponent<Image>().color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.01f)
        {
            c.a = alpha;
            GetComponent<Image>().color = c;
            yield return null;
        }
    }
}
