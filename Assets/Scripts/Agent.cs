using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    float health = 100;
    public float maxHealth = 100;
    public Vector2 Velocity = Vector2.zero;
    public float mass = 4;

    public float Health
    {
        get
        {
            return health;
        }
    }
    public void Damage(float damage)
    {
        health -= damage;
    }

    // Use this for initialization
    protected void Start()
    {
        tag = Constants.AGENT_TAG;
        GetComponent<Collider2D>().isTrigger = true;

        var rb = gameObject.AddComponent<Rigidbody2D>();
        //rb.simulated = false; // Just for collisions
        rb.gravityScale = 0;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    protected void Update()
    {
        Vector2 position = transform.position;
        position.x += Velocity.x * Time.deltaTime;
        position.y += Velocity.y * Time.deltaTime;
        transform.position = position;
    }

    protected void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        //print("push off");
        // Push off collision
        Vector3 betweenUs = transform.position - collision.gameObject.transform.position; // Vector pointing at us. move off by some amount
        transform.position += betweenUs / mass * Time.deltaTime;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {

    }
}
