using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour
{
    float health = 100;
    public float maxHealth = 100;
    public Vector2 velocity = Vector2.zero;
    public float mass = 4;
    AudioSource collide;

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
    protected void Start()
    {
        tag = Constants.AGENT_TAG;
        GetComponent<Collider2D>().isTrigger = true;

        var rb = gameObject.AddComponent<Rigidbody2D>();
        if (rb != null) // can be null if cloned object it seems
        {
            //rb.simulated = false; // Just for collisions
            rb.gravityScale = 0;
            rb.isKinematic = true;
        }

        transform.localScale = new Vector3(Constants.WORLD_SCALE, Constants.WORLD_SCALE, Constants.WORLD_SCALE);

        // Choose the first player object
        GameManager m = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (IsPlayable() && m.StartedGame)
        {
            if (m.CurrentPlayer == null)
            {
                m.CurrentPlayer = gameObject;
                gameObject.AddComponent<PlayerControl>();
                // Make highlight
                //var l = gameObject.AddComponent<LineRenderer>();
                //l.widthMultiplier = 3;
                //var h = gameObject.AddComponent<Highlight>();
                //h.xradius = 2;
                //h.yradius = 2;
            }
            else
            {
                gameObject.AddComponent<AIControl>();
            }
        }

        collide = gameObject.AddComponent<AudioSource>();
        collide.clip = AudioManager.GetClip("Sounds/collide");
    }

    // Whether this is playable by the player
    public bool IsPlayable()
    {
        return GetType().Name == "BCell" || GetType().Name == "HelperT" || GetType().Name == "KillerT" || GetType().Name == "Phagocyte";
    }

    // Update is called once per frame
    protected void Update()
    {
        Vector2 position = transform.position;
        position.x += velocity.x * Time.deltaTime;
        position.y += velocity.y * Time.deltaTime;
        transform.position = position;

        float lowerLim = 150;
        float slow = 0.6f;
        if (transform.position.y < lowerLim)
        {
            transform.position = new Vector2(transform.position.x, lowerLim);
            velocity.y = Mathf.Abs(velocity.y) * slow;
            collide.Play();
        }
        float upperLim = 1080 - lowerLim;
        if (transform.position.y > upperLim)
        {
            transform.position = new Vector2(transform.position.x, upperLim);
            velocity.y = -Mathf.Abs(velocity.y) * slow;
            collide.Play();
        }
        float leftLim = transform.localScale.x;
        if (transform.position.x < leftLim)
        {
            transform.position = new Vector2(leftLim, transform.position.y);
            velocity.x = Mathf.Abs(velocity.x) * slow;
            collide.Play();
        }
        float rightLim = 1920 - leftLim;
        if (transform.position.x > rightLim)
        {
            transform.position = new Vector2(rightLim, transform.position.y);
            velocity.x = -Mathf.Abs(velocity.x) * slow;
            collide.Play();
        }
    }

    protected void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        //collide.Play();
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

    public Transform GetClosestInstance(Transform[] instances)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in instances)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public GameObject GetClosestInstance(GameObject[] instances)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in instances)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void MoveTowards(Vector3 target, float acceleration)
    {
        Vector2 to = new Vector2(target.x, target.y) - new Vector2(transform.position.x, transform.position.y);
        to.Normalize();
        Velocity += acceleration * to;
    }
}
