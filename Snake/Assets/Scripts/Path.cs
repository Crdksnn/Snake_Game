using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    
    public List<Vector2> path = new List<Vector2>();
    [SerializeField] List<Vector2> mousePath = new List<Vector2>();
    [SerializeField] private float gap;
    [SerializeField] private float speed;
    void Start()
    {
        
    }

    private void Update()
    {
        
        Move();
        PathCreator();
    }


    private void PathCreator()
    {   
        path.Clear();
        mousePath.Reverse();
        for (int i = 0; i < mousePath.Count - 1; i++)
        {
            Debug.DrawLine(mousePath[i], mousePath[i + 1], Color.blue, 100);
        }
        
        path.Add(mousePath[0]);
        
        int index = 0;
        float difference = 0;
        Vector2 point = path.Last();
        
        while (index < mousePath.Count - 1)
        {
            var differenceBetweenPoints = Vector2.Distance(mousePath[index], mousePath[index + 1]);
            Vector2 direction = mousePath[index + 1] - mousePath[index];
            var newPoint = point + direction.normalized * (gap - difference);
            var distance = Vector2.Distance(newPoint, mousePath[index]);
            
            while (distance != 0 && distance <= differenceBetweenPoints)
            {
                difference = 0;
                path.Insert(0,newPoint);
                newPoint = path.First() + direction.normalized * gap;
                distance = Vector2.Distance(newPoint,mousePath[index]);
                point = path.First();
            }

            if (differenceBetweenPoints < distance)
            {   
                
                if(index + 2 == mousePath.Count)
                    path.Insert(0,mousePath.First());
                
                difference += Vector2.Distance(mousePath[index + 1],point);
                point = mousePath[index + 1];
            }
            
            index++;
        }
        mousePath.Reverse();
    }
    
    private void Move()
    {
        var pos = (Vector2)transform.position;
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos != mousePos)
        {
            transform.position = Vector2.MoveTowards(pos, mousePos, speed * Time.deltaTime);
            mousePath.Add(transform.position);
        }
        
       
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        
        for(int i = 0; i < path.Count; i++)
            Gizmos.DrawSphere(path[i],.07f);
        
    }
}
