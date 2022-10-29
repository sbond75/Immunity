using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : Agent
{
    public float speed;
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject projectile;
    public float launchVelocity = 700f;
    private float fireTime = 0.5f; //seconds
    private float timeSinceFire = 0f;
    private GameObject carrying;
    public GameObject Carrying
    {
        get
        {
            return carrying;
        }
        set
        {
            carrying = value;
        }
    }
    AudioSource shoot;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.BCELL_TAG;

        shoot = gameObject.AddComponent<AudioSource>();
        shoot.clip = AudioManager.GetClip("Sounds/antibodyShootTiny");
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        timeSinceFire += Time.deltaTime;
        var p = GetComponent<PlayerControl>();
        if (p.Fire && timeSinceFire > fireTime)
        {
            print("fire");
            timeSinceFire = 0;

            // https://answers.unity.com/questions/604198/shooting-in-direction-of-mouse-cursor-2d.html
            //...setting shoot direction
            var shootDirection = p.ShootDirection;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - transform.position;
            shootDirection.Normalize();
            //...instantiating the rocket
            var ea = Quaternion.FromToRotation(transform.position, shootDirection).eulerAngles;
            GameObject bulletInstance = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(ea.x + Random.Range(-30,30), ea.y + Random.Range(-30, 30), ea.z)));
            shoot.PlayOneShot(shoot.clip);
            bulletInstance.GetComponent<Antibody>().creator = this;

            // Inhibit with a chance, increase chance if creator has a helper t nearby
            GameObject[] objects = GameObject.FindGameObjectsWithTag(Constants.HELPER_T_TAG);
            GameObject closest = GetClosestInstance(objects);
            if (closest != null)
            {
                if (Vector3.Distance(closest.transform.position, transform.position) < 50 * Constants.WORLD_SCALE)
                {
                    GameObject[] virusesNearHelperT_ = GameObject.FindGameObjectsWithTag(Constants.VIRUS_TAG);
                    var currentPos = closest.transform.position;
                    var maxDist = 20 * Constants.WORLD_SCALE;
                    IList<GameObject> virusesNearHelperT = new List<GameObject>();
                    foreach (GameObject t in virusesNearHelperT_)
                    {
                        float dist = Vector3.Distance(t.transform.position, currentPos);
                        if (dist < maxDist)
                        {
                            virusesNearHelperT.Add(t);
                        }
                    }

                    GameObject[] array = new GameObject[virusesNearHelperT.Count];
                    virusesNearHelperT.CopyTo(array, 0);
                    bulletInstance.GetComponent<Antibody>().effectiveAgainst = array;
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
