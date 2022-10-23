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
        StartCoroutine(SpawnCells());
    }

    IEnumerator SpawnCells()
    {
        while (true)
        {
            if (VaccineMode.vaccine)
            {
                yield return new WaitForSeconds(Random.Range(4, 6));
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
            
            randomIndex = Random.Range(0, cellReferences.Length);
            randomSide = Random.Range(0, spawnerRefenrences.Length);

            spawnedCell = Instantiate(cellReferences[randomIndex]);
            spawnedCell.transform.position = spawnerRefenrences[randomSide].position;

            if (randomIndex < 3)
            {
                spawnedCell.GetComponent<Agent>().velocity = new Vector2(-Random.Range(100, 200), -Random.Range(100, 200));

            } else
            {
                spawnedCell.GetComponent<Agent>().velocity = new Vector2(Random.Range(100, 200), Random.Range(100, 200));
                spawnedCell.transform.localScale = new Vector3(1f, -1f, 1f);
            }
        }
    }
}
