using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deneme : MonoBehaviour
{
    
    [SerializeField] List<Transform> pointList = new List<Transform>();
    [SerializeField] private List<Vector2> path = new List<Vector2>();
    [SerializeField] private float gap;
    void Start()
    {
        
        Deneme1();
    }

    


    void Update()
    {

        

    }

    private void Deneme1()
    {
        
        for(int i = 0; i < pointList.Count - 1; i++)
            Debug.DrawLine(pointList[i].position, pointList[i + 1].position,Color.blue);
        
        int index = 0;
        var difference = 0f;
        path.Add(pointList[0].position);
        Vector2 referencePoint = path.First();

        while (index < pointList.Count - 1)
        {
            
            Vector2 direction = pointList[index + 1].position - pointList[index].position;
            var distance = Vector2.Distance(referencePoint, pointList[index + 1].position);
            var movement = gap - difference;
            
            if (distance != 0 && movement <= distance)
            {
                var newPoint = referencePoint + direction.normalized * movement;
                path.Add(newPoint);
                referencePoint = newPoint;
                difference = 0;
                continue;
            }
            
            difference += distance;

            if(index + 2 == pointList.Count && difference != 0)
                path.Add(pointList.Last().position);
                
            referencePoint = pointList[index + 1].position;
            index++;
        }
        
        
    }
    
    private void Deneme2()
    {
        
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        
        for (int i = 0; i < path.Count; i++)
        {
            Gizmos.DrawSphere(path[i],.1f);
        }
    }
}
