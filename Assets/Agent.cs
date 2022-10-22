using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    float health = 100;
    public float maxHealth = 100;

    public float Health
    {
        get;
    }
    public void Damage(float damage)
    {
        health -= damage;
    }

    // Use this for initialization
    void Start()
    {
        tag = Constants.VIRUS_TAG;
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        // Push off collision
        Vector3 betweenUs = transform.position - collision.gameObject.transform.position; // Vector pointing at us. move off by some amount
        transform.position += betweenUs / 4;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {

    }
}
