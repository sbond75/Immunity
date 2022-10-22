using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phagocyte : Agent
{
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        tag = Constants.PHAGOCYTE_TAG;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        PhagocyteMoveKeyBoard();
    }

    void PhagocyteMoveKeyBoard()
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
        if (collision.gameObject.CompareTag(Constants.VIRUS_TAG))
        {
            Damage(10);
        }
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
