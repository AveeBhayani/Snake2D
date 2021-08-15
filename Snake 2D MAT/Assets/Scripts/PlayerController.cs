using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private Vector2Int gridMoveDirection;
    private List<Transform> Segments;

    public Transform BodyPrefab;

    void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerMax = 0.5f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
        
    }

    private void Start()
    {
        Segments = new List<Transform>();
        Segments.Add(this.transform);
    }
  
    void Update()
    {
        PlayerMovement();
        //HandleGridMovement();
    }
    private void PlayerMovement()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(gridMoveDirection.y != -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = +1;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(gridMoveDirection.y != +1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(gridMoveDirection.x != +1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if( gridMoveDirection.x != -1)
            {
                gridMoveDirection.x = +1;
                gridMoveDirection.y = 0;
            }
        }
    }
 
    private void FixedUpdate()
    {
        gridMoveTimer += Time.deltaTime;
        if(gridMoveTimer >= gridMoveTimerMax)
        {
            gridPosition += gridMoveDirection;
            gridPosition = ScreenWrap(gridPosition);
            gridMoveTimer -= gridMoveTimerMax;
        }

        for(int i = Segments.Count - 1; i > 0; i--)
        {
            Segments[i].position = Segments[i - 1].position;
        }
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);
    }
        
    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void Grow()
    {
        Transform Segement = Instantiate(this.BodyPrefab);
        Segement.position = Segments[Segments.Count - 1].position;
        Segments.Add(Segement);
    }

    public Vector2Int ScreenWrap(Vector2Int gridPosition)
    {
        if(gridPosition.x < 0)
        {
            gridPosition.x = 20;
        }

        if (gridPosition.x > 20)
        {
            gridPosition.x = 0;
        }

        if (gridPosition.y < 0)
        {
            gridPosition.y = 20;
        }

        if (gridPosition.y > 20)
        {
            gridPosition.y = 0;
        }
        return gridPosition;
    }
}
