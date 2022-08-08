using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Deneme : MonoBehaviour
{

    [SerializeField] private Transform targetObject;
    [SerializeField] List<Vector2> pointList = new List<Vector2>();
    [SerializeField] private List<Vector2> path = new List<Vector2>();
    [SerializeField] private float gap;
    [SerializeField] private float speed;

    private int index = 0;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
        Movement();
        PathCreator();
    }

    private void PathCreator()
    {

        
        
    }
    
    /*
    private void Movement()
    {
        var pos = (Vector2)transform.position;
        var targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos != targetPos)
        {
            transform.position = Vector2.MoveTowards(pos, targetPos, speed * Time.deltaTime);
            pointList.Add(pos);
        }

    }
    */
    
    private void Movement()
    {
        var pos = transform.position;
        var targetPos = targetObject.position;

        var distance = Vector2.Distance(targetPos, pos);

        if (gap <= distance)
        {
            var direction = targetPos - pos;
            transform.position = pos + direction.normalized * gap;
            path.Add(transform.position);
        }

    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        for(int i = 0; i < path.Count; i++)
        {
            Gizmos.DrawSphere(path[i],.1f);    
        }
    }
}
