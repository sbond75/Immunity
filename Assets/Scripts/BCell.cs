using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCellMovement : MonoBehaviour
{
    public float speed = 5;
    public float maxHealth = 100;
    public float currentHealth;
    private string VIRUS_TAG = "Virus";

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        BCellMoveKeyBoard();
    }

    void BCellMoveKeyBoard()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += h * speed * Time.deltaTime;
        position.y += v * speed * Time.deltaTime;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(VIRUS_TAG))
        {
            changeHealth(-10);
        }
    }

    private void changeHealth(float changeAmount)
    {
        currentHealth += changeAmount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


}
