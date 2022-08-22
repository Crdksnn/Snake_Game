using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class PathCreator
{

    public static List<Vector2> Path(float gap, List<Node> mousePath)
    {
        List<Vector2> path = new List<Vector2>();
        int index = 0;
        var difference = 0f;
        path.Add(mousePath[0].position);
        Vector2 referencePoint = path.First();
        
        while (index < mousePath.Count - 1)
        {
            Vector2 direction = mousePath[index + 1].position - mousePath[index].position;
            var distance = Vector2.Distance(referencePoint, mousePath[index + 1].position);
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

            if(index + 2 == mousePath.Count && difference != 0)
                path.Add(mousePath.Last().position);
                
            referencePoint = mousePath[index + 1].position;
            index++;
        }

        return path;
    }
    
}
