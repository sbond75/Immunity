using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cellReferences;

    private GameObject spawnedCell;

    [SerializeField]
    private Transform[] spawnerRefenrences;

    private int randomIndex;
    private int randomSide; 
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Spawner = gameObject;
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnCells());
    }

    IEnumerator SpawnCells()
    {
        while (true)
        {

            yield return new WaitForSeconds(Random.Range(2, 3));
            
            randomIndex = Random.Range(0, cellReferences.Length);
            if (!VaccineMode.vaccine || (VaccineMode.vaccine && randomIndex < 5))
            {
                randomSide = Random.Range(0, spawnerRefenrences.Length);

                spawnedCell = Instantiate(cellReferences[randomIndex]);
                spawnedCell.transform.position = spawnerRefenrences[randomSide].position;


                //vaccine and about to generate virus, stop
                if (randomIndex < 3)
                {
                    spawnedCell.GetComponent<Agent>().velocity = new Vector2(-Random.Range(10, 20), -Random.Range(10, 20));

                }
                else
                {
                    spawnedCell.GetComponent<Agent>().velocity = new Vector2(Random.Range(10, 20), Random.Range(10, 20));
                    //spawnedCell.transform.localScale = new Vector3(1f, -1f, 1f);
                }
            }
        }
    }
}
