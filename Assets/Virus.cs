using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves somewhat randomly based on blood movement, cant control itself
public class Virus : Agent
{
    public float DamagePower = 2;
    public GameObject GotIn;

    // Start is called before the first frame update
    void Start()
    {
        tag = "Virus";
    }

    // Update is called once per frame
    void Update()
    {
        if (GotIn != null)
        {
            // Stay nearby
            Vector3 betweenUs = transform.position - GotIn.transform.position; // Vector pointing at us. move in (opposite) by some amount
            transform.position -= betweenUs / 4;
        }
    }
}
