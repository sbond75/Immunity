using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public BCell target;
    //public float HideDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void setTarget(BCell value)
    {
        target = value;
    }
    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            var dir = target.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //if (dir.magnitude < HideDistance)
        //{
        //    SetChildActive(false);
        //} else
        //{
        //    SetChildActive(true);
        //}

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
