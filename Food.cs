using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        float xpos = Random.Range(bounds.min.x, bounds.max.x);
        float ypos = Random.Range(bounds.min.y, bounds.max.y);

        transform.position = new Vector3(Mathf.Round(xpos), Mathf.Round(ypos), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
