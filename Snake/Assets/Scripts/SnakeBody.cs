using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private List<Vector2> _path;
    [SerializeField] private int index;
    void Start()
    {
        _path = GameObject.FindWithTag("Player").GetComponent<Path>().path;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _path[_path.Count - index];
    }
}
