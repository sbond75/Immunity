using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public const float SPEED = 5;
    public float speed = SPEED;

    protected bool fire = false;
    public bool Fire { get { return fire; } }
    protected Vector3 shootDirection;
    public Vector3 ShootDirection
    {
        get
        {
            return shootDirection;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        PhagocyteMoveKeyBoard();

        fire = Input.GetButtonDown("Fire1");
        if (fire)
        {
            shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        }
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

}
