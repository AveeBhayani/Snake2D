using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private Vector2Int gridMoveDirection;
    public int snakeBodySize;
    public List<Vector2Int> snakeMovePositionList;
    private List<SnakeBodyPart> SegmentBodyParts;
    public Transform BodyPrefab;

    private float Multiplier;
    private float MultiplierTime;
    private bool Boosting;

    void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerMax = .4f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);

        Multiplier = 1;
        MultiplierTime = 0f;
        Boosting = false;

        snakeBodySize = 1;
        snakeMovePositionList = new List<Vector2Int>();
        SegmentBodyParts = new List<SnakeBodyPart>();
    }

    private void Start()
    {

    }

    private void CreateSnakeBody()
    {
        SegmentBodyParts.Add(new SnakeBodyPart(SegmentBodyParts.Count));
    }



    void Update()
    {
        PlayerMovement();

        if (Boosting)
        {
            MultiplierTime += Time.deltaTime;
            if (MultiplierTime >= 15f)
            {
                Multiplier = 1;
                MultiplierTime = 0f;
                Boosting = false;
            }
        }
    }
    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection.y != -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = +1;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection.y != +1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection.x != +1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.x = +1;
                gridMoveDirection.y = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        gridMoveTimer += Time.deltaTime * Multiplier;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridPosition += gridMoveDirection;
            gridPosition = ScreenWrap(gridPosition);
            gridMoveTimer -= gridMoveTimerMax;
            snakeMovePositionList.Insert(0, gridPosition);
        }
        if (snakeMovePositionList.Count >= snakeBodySize + 1)
        {
            snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
        }

        transform.position = new Vector3(gridPosition.x, gridPosition.y);
        transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

        UpdateBodyPart();
    }

    public void UpdateBodyPart()
    {
        for (int i = 0; i < SegmentBodyParts.Count; i++)
        {
            SegmentBodyParts[i].SetGridPosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public void Grow()
    {
        snakeBodySize++;
        CreateSnakeBody();
    }

    public Vector2Int ScreenWrap(Vector2Int gridPosition)

    {
        if (gridPosition.x < 0)
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

    public void SpeedBoostActive()
    {
        Multiplier = 2f;
        Boosting = true;
    }

    public List<Vector2Int> FullSnakeGridPositionList()
    {
        List<Vector2Int> PositionList = new List<Vector2Int>() { gridPosition };
        PositionList.AddRange(snakeMovePositionList);
        return PositionList;
    }

    private class SnakeBodyPart
    {

        private Vector2Int gridPosition;
        private Transform transform;

        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakeBodySprite;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -1 - bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetGridPosition(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }

    }

}
