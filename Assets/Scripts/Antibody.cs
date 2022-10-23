using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antibody : Agent
{
    public Agent creator;
    public GameObject[] effectiveAgainst;
    GameObject attached;
    Vector3 attachedRelTo;
    public float velRange = 10.1f;
    private float timeLastInhibit = 10.0f;
    float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.ANTIBODY_TAG;
        transform.localScale = new Vector3(Constants.WORLD_SCALE, Constants.WORLD_SCALE, Constants.WORLD_SCALE) * 0.3f;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        elapsed += Time.deltaTime;
        if (elapsed > 9)
        {
            Destroy(gameObject);
        }
        if (attached != null)
        {
            transform.position = attached.transform.position + attachedRelTo;

            timeLastInhibit -= Time.deltaTime;
            if (timeLastInhibit < 0)
            {
                timeLastInhibit = 10;
                // Inhibit
                var virus = attached.GetComponent<Virus>();
                if (Random.Range(0.0f, 1.0f) < 0.8)
                {
                    virus.DamagePower /= 2.0f;
                }
                if (Random.Range(0.0f, 1.0f) < 0.04)
                {
                    virus.GotIn = null;
                }
            }
        }
    }

    private void Damage(float changeAmount)
    {
        base.Damage(changeAmount);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void attach(GameObject other)
    {
        attached = other;
        // Save position relative to the attached
        attachedRelTo = attached.transform.position - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(Constants.VIRUS_TAG))
        {
            // Check for effectiveness
            foreach (GameObject g in effectiveAgainst)
            {
                if (g == collision.gameObject)
                {
                    // Increase attach likeliness
                    if (Random.Range(0.0f,1.0f) < 0.8)
                    {
                        attach(collision.gameObject);
                        return;
                    }
                }
            }

            // Try basic attach
            //print("pppp"+Random.Range(0.0f, 1.0f));
            if (Random.Range(0.0f, 1.0f) < 0.04)
            {
                attach(collision.gameObject);
                return;
            }

            // Just switch to physics object with low speed
            //var rb = GetComponent<Rigidbody2D>();
            //rb.velocity = Velocity / 2;
            //Velocity = Vector2.zero;
            //rb.isKinematic = false;

            // Give initial random velocity
            Velocity = new Vector2(Random.Range(-velRange, velRange) * 10, Random.Range(-velRange, velRange) * 10);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != creator)
        {
            base.OnTriggerStay2D(collision);
        }
        // Else: don't collide with creator
    }
}
