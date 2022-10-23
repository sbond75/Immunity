using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerT : Agent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.KILLER_T_TAG;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void Damage(float changeAmount)
    {
        base.Damage(changeAmount);
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag(Constants.BCELL_TAG))
        {

            // Eat it up
            Destroy(collision.gameObject);
        }
    }
}
