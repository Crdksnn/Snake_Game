using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Path : MonoBehaviour
{
    
    public List<Vector2> path = new List<Vector2>();
    [SerializeField] List<Vector2> mousePath = new List<Vector2>();
    [SerializeField] private float gap;
    
    [SerializeField] private float speed;

    private List<Transform> _snakeBody = new List<Transform>();
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private Transform bait;
    
    private Vector2 _bottomCorner;
    private Vector2 _topCorner;
    private float _boundryMinX, _boundrymaxX, _boundryminY, _boundrymaxY;
    
    void Start()
    {
        ScreenBoundry();
    }
    
    private void Update()
    {
        Movement();
        PathCreator();
        Feed();
    }

    private void Feed()
    {

        var distance = Vector2.Distance(transform.position, bait.position);
        var snakeHeadScale = transform.localScale.x / 2;
        var baitScale = bait.localScale.x / 2;

        if (distance <= snakeHeadScale + baitScale)
        {
            ChangeBaitTransform();
            //GameObject body = Instantiate(bodyPrefab,path[path.Count -_snakeBody.Count - 1],Quaternion.identity);
            //_snakeBody.Add(body.transform);
        }
            
        
    }
    
    private void ChangeBaitTransform()
    {
        // var x = condition ? var1 : var2 ;
        float newX = Random.Range(_boundryMinX, _boundrymaxX);
        float newY = Random.Range(_boundryminY, _boundrymaxY);
        bait.position = new Vector2(newX, newY);
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
    
    private void Movement()
    {
        var pos = (Vector2)transform.position;
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos != mousePos)
        {

            if (pos.x < _boundryMinX - transform.localScale.x / 2 || _boundrymaxX + transform.localScale.x / 2 < pos.x)
                pos.x = pos.x * -1;

            if (pos.y < _boundryminY - transform.localScale.x / 2 || _boundrymaxY + transform.localScale.x / 2< pos.y)
                pos.y = pos.y * -1;
            
            transform.position = Vector2.MoveTowards(pos, mousePos, speed * Time.deltaTime);
            mousePath.Add(transform.position);
            
            /*
            for (int i = 0; i < _snakeBody.Count; i++)
                _snakeBody[i].position = path[path.Count - i - 1];
            */
        }
        
       
    }
    
    private void ScreenBoundry()
    {
        _bottomCorner = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        _topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        _boundryMinX = _bottomCorner.x;
        _boundrymaxX = _topCorner.x;
        _boundryminY = _bottomCorner.y;
        _boundrymaxY = _topCorner.y;
    }
    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        
        for(int i = 0; i < path.Count; i++)
            Gizmos.DrawSphere(path[i],.07f);
        
        
    }
    
}
