using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    private enum State
    {
        Alive,
        Dead
    }

    private State state;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    private Direction gridMoveDirection;
    private int snakeBodySize;
    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyPartList;

    private LevelGrid levelgrid;

    private int multiplier;
    private float multiplierTime;
    private bool boosting;

    private bool shieldBoosting;

    public void Setup(LevelGrid levelGrid)
    {
        this.levelgrid = levelGrid;
    }

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerMax = .4f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Up;
        snakeBodySize = 0;
        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodyPartList = new List<SnakeBodyPart>();
        state = State.Alive;

        multiplier = 2;
        multiplierTime = 0f;
        boosting = false;
        shieldBoosting = false;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Alive:
                PlayerMovement();
                PlayerMovementHandler();
                break;
            case State.Dead:
                break;
        }

        if (boosting)
        {
            multiplierTime += Time.deltaTime;
            if (multiplierTime >= 15f)
            {
                multiplier = 2;
                multiplierTime = 0f;
                boosting = false;
            }
        }

        if (shieldBoosting)
        {
            multiplierTime += Time.deltaTime;
            if (multiplierTime >= 15f)
            {
                multiplierTime = 0f;
                shieldBoosting = false;
            }
        }

    }

    public void SpeedBoostActive()
    {
        multiplier = 3;
        boosting = true;

    }

    public void ShieldBoostActive()
    {
        shieldBoosting = true;

    }

    private void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection != Direction.Down)
            {
                gridMoveDirection = Direction.Up;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection !=  Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left; 
            }

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != Direction.Left)
            {
                gridMoveDirection = Direction.Right;
            }
        }
    }

    public void PlayerMovementHandler()
    {
        gridMoveTimer += Time.deltaTime * multiplier;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            
            //gridPosition = ScreenWrap(gridPosition);
            gridMoveTimer -= gridMoveTimerMax;
            //SegmentsPosition.Insert(0, gridPosition);

            SnakeMovePosition previousSnakeMovePosition = null;
            if(snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition,gridPosition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int gridMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1, 0); break;
                case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: gridMoveDirectionVector = new Vector2Int(0, +1); break;
                case Direction.Down: gridMoveDirectionVector = new Vector2Int(0, -1); break;

            }

            gridPosition += gridMoveDirectionVector;

            gridPosition = ScreenWrap(gridPosition);

            bool snakeAteFood = levelgrid.TrySnakeEatFood(gridPosition);
            if (snakeAteFood)
            {
                snakeBodySize++;
                CreateSnakeBodyPart();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            foreach(SnakeBodyPart snakeBodyPart in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if(gridPosition == snakeBodyPartGridPosition && shieldBoosting == false)
                {
                    state = State.Dead;
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);

            UpadteSnakeBodyParts();
        }
    }

    private void CreateSnakeBodyPart()
    {
        /*GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
        snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAsset.instance.snakeBody;
        snakeBodyPartList.Add(snakeBodyGameObject.transform);
        snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -snakeBodyPartList.Count;*/
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));

    }

    private void UpadteSnakeBodyParts()
    {
        for (int i = 0; i < snakeBodyPartList.Count; i++)
        {
            snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public List<Vector2Int> FullSnakeGridPositionList()
    {
        List<Vector2Int> PositionList = new List<Vector2Int>() { gridPosition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            PositionList.Add(snakeMovePosition.GetGridPosition());
        }
        
        return PositionList;
    }

    private class SnakeBodyPart
    {
        private SnakeMovePosition snakeMovePosition;
        private Transform transform;
        public SnakeBodyPart(int bodyIndex)
        {
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAsset.instance.snakeBody;
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);

            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 0; break;
                        case Direction.Left:
                            angle = 0 + 45; break;
                    case Direction.Right:
                            angle = 0 - 45; break;
                    }
                    break;
                case Direction.Down:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 180; break;
                        case Direction.Left:
                            angle = 180 + 45; break;
                    case Direction.Right:
                            angle = 180 - 45; break;
                    }
                    break;
                case Direction.Left:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = -90; break;
                        case Direction.Down:
                            angle = -45; break;
                    case Direction.Up:
                            angle = 45; break;
                    }
                    break;
                case Direction.Right:
                    switch (snakeMovePosition.GetPreviousDirection())
                    {
                        default:
                            angle = 90; break;
                        case Direction.Down:
                            angle = 45; break;
                    case Direction.Up:
                            angle = 45; break;
                    }
                    break;
            }

            transform.eulerAngles = new Vector3(0, 0, angle);


        }

        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition(); 
        }
    }

    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPreviousDirection()
        {
            if (previousSnakeMovePosition == null)
            {
                return Direction.Up;
            }
            else
            {
                return previousSnakeMovePosition.direction;
            }
        }

        
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
}
