using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path : MonoBehaviour
{
    private List<Vector2> pointList = new List<Vector2>();
    public List<Vector2> path = new List<Vector2>();
    void Start()
    {
        
    }
    
    void Update()
    {
        
        pointList.Add(transform.position);
        CreatePath();
    }

    private void CreatePath()
    {
        path.Add(pointList.Last());

        for (int i = 0; (1 < path.Count &&  i + 1 != path.Count) && i < path.Count; i++)
        {
            var pos = path[i];
            var targetPos = path[i + 1];
            var direction = targetPos - pos;

            var distance = Vector2.Distance(pos, targetPos);

            if (1 <= distance)
            {
                
            }
            
            else
            {
                
            }
            
        }
    }
}
