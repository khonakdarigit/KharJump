using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class Mirror : MonoBehaviour
{
    public bool OnWork { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        OnWork = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Donky>();
        if (player != null)
        {
            Vector3 SpawnerPosition = new Vector3();
            SpawnerPosition.x = transform.position.x > 0 ? transform.position.x - 1f : transform.position.x + 1f;

            SpawnerPosition.x *= -1;
            SpawnerPosition.y = transform.position.y;

            collision.transform.position = SpawnerPosition;
        }
    }
}
