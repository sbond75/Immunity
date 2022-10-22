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
            yield return new WaitForSeconds(Random.Range(1, 5));
            randomIndex = Random.Range(0, cellReferences.Length);
            randomSide = Random.Range(0, spawnerRefenrences.Length);

            spawnedCell = Instantiate(cellReferences[randomIndex]);
            spawnedCell.transform.position = spawnerRefenrences[randomSide].position;

            if (randomIndex < 3)
            {
                spawnedCell.GetComponent<Agent>().velocity = new Vector2(Random.Range(2, 6), Random.Range(2, 6));

            } else
            {
                spawnedCell.GetComponent<Agent>().velocity = new Vector2(-Random.Range(2, 6), -Random.Range(2, 6));
            }
        }
    }
}
