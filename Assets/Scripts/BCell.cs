using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : Agent
{
    public float speed = 5;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject projectile;
    public float launchVelocity = 700f;
    private float fireTime = 0.5f; //seconds
    private float timeSinceFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.BCELL_TAG;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();

        timeSinceFire += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && timeSinceFire > fireTime)
        {
            print("fire");
            timeSinceFire = 0;

            // https://answers.unity.com/questions/604198/shooting-in-direction-of-mouse-cursor-2d.html
            //...setting shoot direction
            Vector3 shootDirection;
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;
            //...instantiating the rocket
            GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            bulletInstance.GetComponent<Antibody>().creator = this;

            // Inhibit with a chance, increase chance if creator has a helper t nearby
            GameObject[] objects = GameObject.FindGameObjectsWithTag(Constants.HELPER_T_TAG);
            GameObject closest = GetClosestInstance(objects);
            if (closest != null)
            {
                if (Vector3.Distance(closest.transform.position, transform.position) < 100)
                {
                    bulletInstance.GetComponent<Antibody>().effectiveAgainst = objects;
                }
            }

            bulletInstance.GetComponent<Agent>().Velocity = new Vector2(shootDirection.x * speed, shootDirection.y * speed);
        }
    }

    private void Damage(float changeAmount)
    {
        base.Damage(changeAmount);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        //GameObject attached = null;
        //// Passive ability: make the virus and infected cells stick to me
        //if (collision.gameObject.CompareTag(Constants.VIRUS_TAG))
        //{
        //    attached = collision.gameObject;
        //}
        //if (collision.gameObject.CompareTag(Constants.TISSUE_CELL_TAG))
        //{
        //    if (collision.gameObject.GetComponent<TissueCell>().Infected)
        //    {
        //        attached = collision.gameObject;
        //    }
        //}

        //if (attached != null)
        //{
        //    // Stay nearby
        //    Vector3 betweenUs = transform.position - attached.transform.position; // Vector pointing at us. move in (opposite) by some amount
        //    var agent = attached.GetComponent<Agent>();
        //    attached.transform.position += betweenUs / agent.mass * Time.deltaTime;
        //}
    }
}
