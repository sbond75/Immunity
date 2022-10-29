using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerT : Agent
{
    public BCell target;
    public Arrow arrow;
    AudioSource kill;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        tag = Constants.KILLER_T_TAG;

        kill = gameObject.AddComponent<AudioSource>();
        kill.clip = AudioManager.GetClip("Sounds/zap");
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        findInfectedBCells();
        //check arrow's reference
        arrow.setTarget(target);
    }

    void findInfectedBCells()
    {
        GameObject[] BCellsToCheck = GameObject.FindGameObjectsWithTag(Constants.BCELL_TAG);
        
        List<GameObject> BCellsWithInfected = new List<GameObject>();
        foreach (GameObject Bcell in BCellsToCheck)
        {

            GameObject carrying = Bcell.GetComponent<BCell>().Carrying;
            if (carrying != null && carrying.CompareTag(Constants.TISSUE_CELL_TAG))
            {
                BCellsWithInfected.Add(Bcell);
            }
        }
        if (BCellsWithInfected.Count != 0)
        {
            target = GetClosestInstance(BCellsWithInfected.ToArray()).GetComponent<BCell>();
            arrow.setTarget(target);
        }
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
        if (target == null)
        {
            return;
        }
        if (collision.gameObject == target.Carrying)
        {
            //accelerating death but not destroying the object
            var tc = target.Carrying.GetComponent<TissueCell>();
            tc.healthDec *= 10;
            tc.die = kill;
        }
    }
}
