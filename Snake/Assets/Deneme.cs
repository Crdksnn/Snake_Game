using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;

public class Deneme : MonoBehaviour
{

    [SerializeField] private List<Transform> pointList = new List<Transform>();
    [SerializeField] List<Vector2> path = new List<Vector2>();
    [SerializeField] private float gap;
    private float _totalLength = 0;
    //private float _currentLength = 0;
    private float difference = 0;
    
    void Start()
    {
        Deneme3();
    }

    private void Deneme3()
    {
        
        for (int i = 0; i < pointList.Count - 1; i++)
        {
            Debug.DrawLine(pointList[i].position, pointList[i + 1].position, Color.blue, 100);
            _totalLength += Vector2.Distance(pointList[i].position, pointList[i + 1].position);
        }
        
        path.Add(pointList[0].position);

        for (int i = 0; i < pointList.Count - 1; i++)
        {
            var lastPoint = path.Last();
            var distanceBetweenPoints = Vector2.Distance(pointList[i].position, pointList[i + 1].position);
            var distance = Vector2.Distance(lastPoint, pointList[i].position);
            Vector2 direction = pointList[i + 1].position - pointList[i].position;
        
            while (distance <= distanceBetweenPoints)
            {

                var newPoint = path.Last() + direction.normalized * (gap - difference);
                distance = Vector2.Distance(newPoint, pointList[i].position);

                if (distance <= distanceBetweenPoints)
                {
                    difference = 0;
                    path.Add(newPoint);
                    distance = Vector2.Distance(newPoint, pointList[i].position);
                }
                
            }
        }
        
    }

    private void Deneme2()
    {
        
        for (int i = 0; i < pointList.Count - 1; i++)
        {
            Debug.DrawLine(pointList[i].position, pointList[i + 1].position, Color.blue, 100);
            _totalLength += Vector2.Distance(pointList[i].position, pointList[i + 1].position);
        }
        
        path.Add(pointList[0].position);
        
        for (int i = 0; i < pointList.Count - 1; i++)
        {
            
            var direction = (Vector2)pointList[i + 1].position - (Vector2)pointList[i].position;
            var distance = Vector2.Distance(path.Last(), pointList[i + 1].position);
            var part = (direction.normalized * gap).magnitude;
            int divideParts = (int) (distance / part);
            
            float x = distance;
            float y = part;
            int z = (int)(x / y);
            Debug.Log(z);
            for (int j = 0; j < divideParts ; j++)
            {
                var newPoint = path.Last() + direction.normalized * gap;
                path.Add(newPoint);
            }
        }
        
    }

    private void Deneme1()
    {
        
        for (int i = 0; i < pointList.Count - 1; i++)
        {
            Debug.DrawLine(pointList[i].position, pointList[i + 1].position, Color.blue, 100);
            _totalLength += Vector2.Distance(pointList[i].position, pointList[i + 1].position);
        }
        
        path.Add(pointList[0].position);

        for (int i = 0; i < pointList.Count - 1; i++)
        {
            var lastPoint = path.Last();
            var distance = Vector2.Distance(pointList[i].position, lastPoint);
            var distanceBetweenPoints = Vector2.Distance(pointList[i].position, pointList[i + 1].position);
            Vector2 direction = pointList[i + 1].position - pointList[i].position;
        
            while (distance <= distanceBetweenPoints)
            {

                lastPoint = path.Last();
                var newPoint = lastPoint + direction.normalized * gap;

                distance = Vector2.Distance(pointList[i].position, newPoint);
            
                if(distance <= distanceBetweenPoints)
                    path.Add(newPoint);

                else
                {
                    var difference = distanceBetweenPoints - distance;
                    var tempPoint = lastPoint + direction.normalized * (difference);

                    direction = (Vector2)pointList[i + 1].position - tempPoint;
                    newPoint = tempPoint + direction.normalized * (gap - difference);
                    path.Add(newPoint);
                }
                
            }
        }

    }
    
    void Update()
    {

        

    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        
        for(int i = 0; i < path.Count; i++)
            Gizmos.DrawSphere(path[i],.05f);
        
    }
}
