using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves somewhat randomly based on blood movement, cant control itself
public class Virus : Agent
{
    public float DamagePower = 2;
    public GameObject GotIn;
    public float velRange = 10.1f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.VIRUS_TAG;

        // Give initial random velocity
        Velocity = new Vector2(Random.Range(-velRange, velRange) * 10, Random.Range(-velRange, velRange) * 10);
    }

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
}
