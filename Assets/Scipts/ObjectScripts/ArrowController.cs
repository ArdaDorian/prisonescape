using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{

    [SerializeField] float arrowSpeed = 1f;
    private void Update()
    {
        transform.Translate(arrowSpeed*-transform.localScale.x*Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}
