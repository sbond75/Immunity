using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves somewhat randomly based on blood movement, cant control itself
public class Virus : Agent
{
    public float DamagePower = 0.2f;
    public GameObject GotIn;
    public float velRange = 10.1f;
    public GameObject creator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        mass = 3;
        tag = Constants.VIRUS_TAG;

        // Give initial random velocity
        Velocity = new Vector2(Random.Range(-velRange, velRange) * 10, Random.Range(-velRange, velRange) * 10);

        // This virus is BCell movable
        gameObject.AddComponent<BCellMovable>();
    }

    public GameObject attached;

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (GotIn != null)
        {
            // Stay nearby
            Vector3 betweenUs = transform.position - GotIn.transform.position; // Vector pointing at us. move in (opposite) by some amount
            transform.position -= betweenUs / 2 * Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GotIn == null && !collision.gameObject.CompareTag("Virus"))
        {
            // Have a chance to attach while we collide
            if (Random.Range(0, 1) < 0.005 && collision.gameObject != creator)
            {
                print("attach" +  " " + collision.gameObject + " " + creator);
                attached = collision.gameObject;
            }

            if (attached != null)
            {
                // Stay nearby
                Vector3 betweenUs = transform.position - attached.transform.position; // Vector pointing at us. move in (opposite) by some amount
                transform.position -= betweenUs * Time.deltaTime;
            }
        }
        base.OnTriggerStay2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        // Detach
        attached = null;
    }
}
