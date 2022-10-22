using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    float health = 100;
    public float maxHealth = 100;
    public Vector2 velocity = Vector2.zero;

    public float Health
    {
        get {
            return health;
        }
    }

    public Vector2 Velocity
    {
        get
        {
            return velocity;
        }
        set
        {
            velocity = value;
        }
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
    protected void Update()
    {
        Vector2 position = transform.position;
        position.x += velocity.x * Time.deltaTime;
        position.y += velocity.y * Time.deltaTime;
        transform.position = position;
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
