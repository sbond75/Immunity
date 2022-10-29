using UnityEngine;
using System.Collections;

public class AIControl : PlayerControl
{
    float elapsed = 0;
    float randomMovementSpeed = 0;
    float moveTowardsSpeed = 10;
    bool moved = false;

    // Use this for initialization
    void Start()
    {
        if (GetComponent<Virus>() == null)
        {
            randomMovementSpeed = 7;
        }
        else
        {
            randomMovementSpeed = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var h = Random.Range(-1, 1);
        var v = Random.Range(-1, 1);
        moved = false;

        elapsed += Time.deltaTime;
        Vector2 position = transform.position;
        if (GetComponent<Virus>() == null && Mathf.Cos(elapsed / 100) > 0.2)
        {
            // Slow down
            //GetComponent<Agent>().Velocity = new Vector2(GetComponent<Agent>().Velocity.x * 0.7f, GetComponent<Agent>().Velocity.y * 0.7f);// TODO: need time.deltatime here

            // Move around
            var vel = GetComponent<Agent>().Velocity;
            GetComponent<Agent>().Velocity = new Vector2(vel.x + h * randomMovementSpeed * Mathf.Sin(elapsed / 10) * (vel.x < 0 ? 1 : -1), vel.y + v * randomMovementSpeed * Mathf.Cos(elapsed / 10 + Random.Range(0,2)) * (vel.y < 0 ? 1 : -1)); // TODO: use Time.deltaTime
        }
        transform.position = position;

        Phagocyte p = GetComponent<Phagocyte>();
        if (p != null)
        {
            GameObject near = GetComponent<Agent>().GetClosestInstance(GameObject.FindGameObjectsWithTag(Constants.VIRUS_TAG));
            if (near != null && Random.Range(0.0f, 1.0f) < 0.8)
            {
                GetComponent<Agent>().MoveTowards(near.transform.position, moveTowardsSpeed * Time.deltaTime);
                moved = true;
            }
            return;
        }
        BCell b = GetComponent<BCell>();
        if (b != null)
        {
            // Shoot antibodies
            fire = false;
            if (Random.Range(0.0f, 1.0f) < 0.3)
            {
                var closest = GetComponent<Agent>().GetClosestInstance(GameObject.FindGameObjectsWithTag(Constants.VIRUS_TAG));
                if (closest != null)
                {
                    shootDirection = closest.transform.position;
                    fire = true;
                }
            }
            return;
        }
        HelperT ht = GetComponent<HelperT>();
        if (ht != null)
        {
            return;
        }
        KillerT kt = GetComponent<KillerT>();
        if (kt != null)
        {
            fire = false;
            if (Random.Range(0.0f, 1.0f) < 0.3)
            {
                // Follow target
                if (kt.target != null)
                {
                    GetComponent<Agent>().MoveTowards(kt.target.transform.position, moveTowardsSpeed * Time.deltaTime);
                    shootDirection.z = 0.0f;
                    moved = true;
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (GetComponent<Virus>() == null && !moved && Random.Range(0.0f, 1.0f) < 0.1)
        {
            // Slow down
            GetComponent<Agent>().Velocity = new Vector2(GetComponent<Agent>().Velocity.x * 0.7f, GetComponent<Agent>().Velocity.y * 0.7f);// TODO: need time.deltatime here
        }
    }
}
