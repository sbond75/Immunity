using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject target;
    public float HideDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dir = target.transform.position - transform.position;

        //if (dir.magnitude < HideDistance)
        //{
        //    SetChildActive(false);
        //} else
        //{
        //    SetChildActive(true);
        //}
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.RotateAround(transform.parent.position, Vector2.up, 20 * Time.deltaTime);
    }

    //void SetChildActive(bool value)
    //{
    //    foreach(Transform child in transform)
    //    {
    //        child.gameObject.SetActive(value);
    //    }
    //}
}
