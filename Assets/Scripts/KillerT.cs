using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerT : Agent
{
    public GameObject target;
    public Arrow arrow;
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
        findInfectedBCells();
    }

    void findInfectedBCells()
    {
        GameObject[] BCellsToCheck = GameObject.FindGameObjectsWithTag("BCell");
        
        List<GameObject> BCellsWithInfected = new List<GameObject>();
        foreach (GameObject Bcell in BCellsToCheck)
        {
            print(Bcell);
            if (Bcell.GetComponent<BCell>().Carrying.CompareTag(Constants.TISSUE_CELL_TAG))
            {
                print("here");
                BCellsWithInfected.Add(Bcell);
                
            }
        }
        target = GetClosestInstance(BCellsWithInfected.ToArray());
        if (target != null) arrow.setTarget(target);
    }

    public GameObject GetClosestInstance(GameObject[] instances)
    {
        return base.GetClosestInstance(instances);
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
