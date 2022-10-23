using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject currentPlayer = null;
    public GameObject CurrentPlayer
    {
        get
        {
            return currentPlayer;
        }
        set
        {
            currentPlayer = value;
            currentPlayer.GetComponent<Agent>().Velocity = Vector2.zero;
        }
    }
    public GameObject Spawner;
    public bool StartedGame = false;
    public bool oneVirus = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var viruses = GameObject.FindGameObjectsWithTag(Constants.VIRUS_TAG);
        if (viruses.Length > 0)
        {
            oneVirus = true;
        }
        if (StartedGame && viruses.Length == 0)
        {
            // Win
            SceneManager.LoadScene("VictoryScene");
        }

        var tissues = GameObject.FindGameObjectsWithTag(Constants.TISSUE_CELL_TAG);
        if (StartedGame && tissues.Length == 0)
        {
            // Game over
            SceneManager.LoadScene("GameOver");
        }
    }
}
