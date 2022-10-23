using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        MoveKeyBoard();
    }
    
    void MoveKeyBoard()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += h * speed * Time.deltaTime;
        position.y += v * speed * Time.deltaTime;
        transform.position = position;
    }

}
