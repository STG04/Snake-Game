using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> segments;
    public Transform segmentPrefab;

    [SerializeField]
    private int initialSize = 4;
    private bool yEnable = true;
    private bool xEnable = true;

    // Start is called before the first frame update
    void Start()
    {
        segments = new List<Transform>();
        ResetState();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && yEnable)
        {
            _direction = Vector2.up;
            yEnable = false;
            xEnable = true;
        }
        else if(Input.GetKeyDown(KeyCode.S) && yEnable)
        {
            _direction = Vector2.down;
            yEnable = false;
            xEnable = true;
        }
        else if(Input.GetKeyDown(KeyCode.A) && xEnable)
        {
            _direction = Vector2.left;
            xEnable = false;
            yEnable = true;
        }
        else if(Input.GetKeyDown(KeyCode.D) && xEnable)
        {
            _direction = Vector2.right;
            xEnable = false;
            yEnable = true;
        }
    }

    private void FixedUpdate()
    {
        for(int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x) + _direction.x, Mathf.Round(transform.position.y) + _direction.y, 0.0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);

    }

    private void ResetState()
    {
        for(int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for(int i = 1; i < initialSize; i++)
        {
            Grow();
        }

        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            Grow();
        }

        if(collision.tag == "Enemy")
        {
            ResetState();
        }
    }
}
