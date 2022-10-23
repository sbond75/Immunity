using UnityEngine;
using System.Collections;

// Object that can be moved by a B cell
public class BCellMovable : MonoBehaviour
{
    // The BCell we're attached to
    public GameObject attached = null;
    Vector3 orig;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Bcell's passive ability: make the virus and infected cells stick to it.
        // BCellMovable must check if a BCell is nearby and then move to its position
        if (collision.gameObject.CompareTag(Constants.BCELL_TAG) && attached == null)
        {
            if (collision.gameObject.GetComponent<BCell>().Carrying == null)
            {
                attached = collision.gameObject;
                orig = transform.position - attached.transform.position;
                attached.GetComponent<BCell>().Carrying = gameObject;
            }
        }

        if (attached != null)
        {
            print("nearby");
            // Stay nearby
            transform.position = attached.transform.position + orig;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == attached)
        {
            attached.GetComponent<BCell>().Carrying = null;
        }
    }
}
