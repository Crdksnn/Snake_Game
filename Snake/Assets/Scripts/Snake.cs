using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct Node
{
    public Vector3 position;
    public bool jump;
}

public class Snake : MonoBehaviour
{
    
    public List<Vector2> path = new List<Vector2>();
    [SerializeField] List<Node> mousePath = new List<Node>();
    [SerializeField] private float gap;
    
    [SerializeField] private float speed;
    [SerializeField] private int maxSnakeBodyCount;
    
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
        SnakePath();
        SnakeBodyMovement();
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
            GameObject body = Instantiate(bodyPrefab, path[_snakeBody.Count + 2], Quaternion.identity);
            _snakeBody.Add(body.transform);
        }
        
    }
    
    private void ChangeBaitTransform()
    {
        float newX = Random.Range(_boundryMinX, _boundrymaxX);
        float newY = Random.Range(_boundryminY, _boundrymaxY);
        bait.position = new Vector2(newX, newY);
    }
    
    private void SnakePath()
    {
        for (int i = 0; i < mousePath.Count - 1; i++)
        {
            Debug.DrawLine(mousePath[i].position, mousePath[i + 1].position, Color.blue);
        }
        
        // path.Clear();
        // path.Add(mousePath[0].position);
        // float difference = 0;
        // Vector2 point = path.Last();
        //
        // for (int i = 0; i < mousePath.Count - 1; i++)
        // {
        //     if (mousePath[i].jump)
        //     {
        //         path.Add(mousePath[i+1].position);
        //         point = path.Last();
        //         continue;
        //     }
        //
        //     var differenceBetweenPoints = Vector2.Distance(mousePath[i].position, mousePath[i + 1].position);
        //     Vector2 direction = mousePath[i + 1].position - mousePath[i].position;
        //     var newPoint = point + direction.normalized * (gap - difference);
        //     var distance = Vector2.Distance(newPoint, mousePath[i].position);
        //     
        //     while (distance != 0 && distance <= differenceBetweenPoints)
        //     {
        //         difference = 0;
        //         path.Add(newPoint);
        //         newPoint = path.Last() + direction.normalized * gap;
        //         distance = Vector2.Distance(newPoint,mousePath[i].position);
        //         point = path.Last();
        //     }
        //
        //     if (differenceBetweenPoints < distance)
        //     {   
        //         
        //         if(i + 2 == mousePath.Count)
        //             path.Add(mousePath.Last().position);
        //         
        //         difference += Vector2.Distance(mousePath[i + 1].position,point);
        //         point = mousePath[i + 1].position;
        //     }
        
        // path.Clear();
        // int index = 0;
        // var difference = 0f;
        // path.Add(mousePath[0].position);
        // Vector2 referencePoint = path.First();
        //
        // while (index < mousePath.Count - 1)
        // {
        //     Vector2 direction = mousePath[index + 1].position - mousePath[index].position;
        //     var distance = Vector2.Distance(referencePoint, mousePath[index + 1].position);
        //     var movement = gap - difference;
        //     
        //     if (distance != 0 && movement <= distance)
        //     {
        //         var newPoint = referencePoint + direction.normalized * movement;
        //         path.Add(newPoint);
        //         referencePoint = newPoint;
        //         difference = 0;
        //         continue;
        //     }
        //     
        //     difference += distance;
        //
        //     if(index + 2 == mousePath.Count && difference != 0)
        //         path.Add(mousePath.Last().position);
        //         
        //     referencePoint = mousePath[index + 1].position;
        //     index++;
        //

        path = PathCreator.Path(gap, mousePath);
        
        
    }
    
    private void Movement()
    {
        var snakeHeadPos = (Vector2)transform.position;
        var mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject body = Instantiate(bodyPrefab, path[_snakeBody.Count + 1], Quaternion.identity);
            _snakeBody.Add(body.transform);
        }
        
        if (snakeHeadPos != mousePos)
        {
            snakeHeadPos = Vector2.MoveTowards(snakeHeadPos, mousePos, speed * Time.deltaTime);
            bool jump = false;
            if (snakeHeadPos.x < _boundryMinX - transform.localScale.x / 2 || _boundrymaxX + transform.localScale.x / 2 < snakeHeadPos.x)
            {
                snakeHeadPos.x = snakeHeadPos.x * -1;
                jump = true;
            }


            if (snakeHeadPos.y < _boundryminY - transform.localScale.x / 2 || _boundrymaxY + transform.localScale.x / 2 < snakeHeadPos.y)
            {
                snakeHeadPos.y = snakeHeadPos.y * -1;
                jump = true;
            }
            
            transform.position = snakeHeadPos;
            
            mousePath.Insert(0, new Node()
            {
                jump = jump,
                position = snakeHeadPos,
            });
            
        }
        
    }

    private void SnakeBodyMovement()
    {
        
        for (int i = 0; i < _snakeBody.Count; i++)
            _snakeBody[i].position = (Vector2)path[i + 1];
    }

    private void ScreenBoundry()
    {
        _bottomCorner = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        _topCorner = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

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
