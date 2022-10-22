using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phagocyte : Agent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.PHAGOCYTE_TAG;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        print("co;");
        if (collision.gameObject.CompareTag(Constants.VIRUS_TAG))
        {
            // Eat it up
            Destroy(collision.gameObject);
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
}
