using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperT : Agent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.HELPER_T_TAG;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
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
