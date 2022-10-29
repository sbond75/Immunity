using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TissueCell : Agent
{
    Virus dnaInjection;
    float spawnVirusWaitingTime = 3; // seconds
    float spawnVirusRandomization = 1; // plus or minus the above
    float spawnVirusRadius = 1;
    public float healthDec = 0.5f; // When infected
    float spawnCount = 5; // viruses to spawn
    float spawnCountVariation = 3; // plus or minus the above
    AudioSource infect;
    public AudioSource die;
    AudioSource spawn;

    public bool Infected
    {
        get
        {
            return Health < 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        mass = 20;
        tag = Constants.TISSUE_CELL_TAG;

        infect = gameObject.AddComponent<AudioSource>();
        infect.clip = AudioManager.GetClip("Sounds/tissueCellDie");
        die = gameObject.AddComponent<AudioSource>();
        die.clip = AudioManager.GetClip("Sounds/muffledExplode");
        spawn = gameObject.AddComponent<AudioSource>();
        spawn.clip = AudioManager.GetClip("Sounds/Enemy_spawn_green");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    // https://answers.unity.com/questions/1396115/making-coroutine-repeat.html
    IEnumerator VirusSpawn()
    {
        while (true)
        {
            // Check for stopping production
            if (Health < -spawnCount / healthDec + Random.Range(-spawnCountVariation, spawnCountVariation))
            {
                // go away
                die.PlayOneShot(die.clip);
                Destroy(gameObject);
                yield return null;
            }

            // Damage to negative amounts
            Damage(healthDec);

            // Color tint
            var tint = (-13 / Health) * 0.13f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(tint, tint, tint, 1);

            if (Health < -0.05f) // Let the production warm up after infection basically
            {
                // This TissueCell is BCell movable
                gameObject.AddComponent<BCellMovable>();

                // Disable existing virus
                dnaInjection.gameObject.SetActive(false);

                // Spawn virus clone
                GameObject virus2 = Instantiate(dnaInjection.gameObject, new Vector3(Random.Range(-spawnVirusRadius, spawnVirusRadius), Random.Range(-spawnVirusRadius, spawnVirusRadius), 0), transform.rotation);
                virus2.transform.position = transform.position;
                var virus2_ = virus2.GetComponent<Virus>();
                virus2_.creator = gameObject;
                virus2_.attached = null;
                virus2_.GotIn = null;
                virus2_.Velocity = virus2_.randomVelocity();
                virus2.SetActive(true);

                // Sound
                spawn.PlayOneShot(spawn.clip);
            }
            yield return new WaitForSeconds(spawnVirusWaitingTime + Random.Range(-spawnVirusRandomization, spawnVirusRandomization));
            //yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Push off collision etc.
        base.OnTriggerEnter2D(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("test " + Health);
        if (Health > 0 && collision.gameObject.CompareTag(Constants.VIRUS_TAG))
        {
            Virus virus = collision.gameObject.GetComponent<Virus>();
            // A little bit of breaking through cell wall happens or whatever
            //print("dmg " + virus.DamagePower);
            Damage(virus.DamagePower * (VaccineMode.vaccine ? 0.1f : 1.0f));

            if (Health <= 0)
            {
                // Virus got in
                virus.GotIn = gameObject;
                dnaInjection = virus;
                // Start timer
                StartCoroutine(VirusSpawn());

                infect.PlayOneShot(infect.clip);
            }
        }

        base.OnTriggerStay2D(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }
}
