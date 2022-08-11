using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    [SerializeField] List<Vector2> _pointList = new List<Vector2>();
    [SerializeField] List<Vector2> _path = new List<Vector2>();

    void Start()
    {
        
    }

   
    void Update()
    {
        CreatePath();
        Move();
    }

    private void CreatePath()
    {

        
        
    }

    private void Move()
    {
        var pos = (Vector2)transform.position;
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos != mousePos)
        {
            transform.position = Vector2.MoveTowards(pos, mousePos, speed * Time.deltaTime);
            _pointList.Add(transform.position);
        }
        
       
    }
    
    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        
        for(int i = 0; 0 < _path.Count && i < _path.Count; i++)
            Gizmos.DrawSphere(_path[i],.1f);

    }
    
}
