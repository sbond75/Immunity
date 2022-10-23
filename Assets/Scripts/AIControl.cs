using UnityEngine;
using System.Collections;

public class AIControl : PlayerControl
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var h = Random.Range(-1f, 1f);
        var v = Random.Range(-1.0f, 1.0f);

        Vector2 position = transform.position;
        GetComponent<Agent>().Velocity = new Vector2(GetComponent<Agent>().Velocity.x + h * speed * Time.deltaTime, GetComponent<Agent>().Velocity.y + v * speed * Time.deltaTime);
        transform.position = position;

        Phagocyte p = GetComponent<Phagocyte>();
        if (p != null)
        {
            return;
        }
        BCell b = GetComponent<BCell>();
        if (b != null)
        {
            // Shoot antibodies
            fire = false;
            if (Random.Range(0, 1) < 0.3)
            {
                var closest = GetComponent<Agent>().GetClosestInstance(GameObject.FindGameObjectsWithTag(Constants.VIRUS_TAG));
                if (closest != null)
                {
                    shootDirection = closest.transform.position;
                    shootDirection.z = 0.0f;
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
        
    }
}
