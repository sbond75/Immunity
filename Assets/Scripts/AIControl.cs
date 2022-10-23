using UnityEngine;
using System.Collections;

public class AIControl : PlayerControl
{
    float elapsed = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var h = Random.Range(-1f, 1f);
        var v = Random.Range(-1.0f, 1.0f);

        elapsed += Time.deltaTime;
        Vector2 position = transform.position;
        if (Mathf.Sin(elapsed / 100) > 0.2)
        {
            // Slow down
            //GetComponent<Agent>().Velocity = new Vector2(GetComponent<Agent>().Velocity.x * 0.7f, GetComponent<Agent>().Velocity.y * 0.7f);// TODO: need time.deltatime here

        }
        GetComponent<Agent>().Velocity = new Vector2(GetComponent<Agent>().Velocity.x + h * speed * Time.deltaTime * Mathf.Sin(elapsed), GetComponent<Agent>().Velocity.y + v * speed * Time.deltaTime * Mathf.Cos(elapsed));
        transform.position = position;

        Phagocyte p = GetComponent<Phagocyte>();
        if (p != null)
        {
            GameObject near = GetComponent<Agent>().GetClosestInstance(GameObject.FindGameObjectsWithTag(Constants.VIRUS_TAG));
            if (near != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, near.transform.position, 10f * Time.deltaTime);
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
                    transform.position = Vector3.MoveTowards(transform.position, kt.target.transform.position, 10f * Time.deltaTime);
                    shootDirection.z = 0.0f;

                }
            }
        }
    }
}
