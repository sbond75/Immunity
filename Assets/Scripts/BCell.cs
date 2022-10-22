using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCell : Agent
{
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        tag = Constants.BCELL_TAG;
    }

    // Update is called once per frame
    protected void Update()
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
}
