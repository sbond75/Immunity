using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves somewhat randomly based on blood movement, cant control itself
public class Virus : Agent
{
    public float DamagePower = 0.2f;
    public GameObject GotIn;
    public float velRange = 0.01f; //10.1f;
    public GameObject creator;

    public Vector3 randomVelocity()
    {
        return new Vector2(Random.Range(-velRange, velRange) * 10, Random.Range(-velRange, velRange) * 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        mass = 3;
        tag = Constants.VIRUS_TAG;

        // Give initial random velocity
        velRange = velRange * Constants.WORLD_SCALE;
        Velocity = randomVelocity();

        // This virus is BCell movable
        //gameObject.AddComponent<BCellMovable>();

        transform.localScale = new Vector3(Constants.WORLD_SCALE, Constants.WORLD_SCALE, Constants.WORLD_SCALE) * 0.1f * 5;
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
        Vector3 betweenUs;

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
                betweenUs = transform.position - attached.transform.position; // Vector pointing at us. move in (opposite) by some amount
                transform.position -= betweenUs * Time.deltaTime;
            }
        }

        //base.OnTriggerStay2D(collision);
        //print("push off");
        // Push off collision
        betweenUs = transform.position - collision.gameObject.transform.position; // Vector pointing at us. move off by some amount
        transform.position += betweenUs / mass * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        // Detach
        attached = null;
    }
}
