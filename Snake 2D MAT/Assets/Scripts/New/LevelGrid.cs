using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private SnakeController snakeController;
    private ScoreManager scoreManager;
    private bool toSpawnFood;
    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;     
    }

    public void Setup(SnakeController snakeController, ScoreManager scoreManger)
    {
        this.snakeController = snakeController;
        this.scoreManager = scoreManger;
        SpawnFood();
    }

    public void SpawnFood()
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }
        while (snakeController.FullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        if (toSpawnFood)
        {
            foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
            foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAsset.instance.foodSprite;
            foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        }
        else
        {
            foodGameObject = new GameObject("BadFood", typeof(SpriteRenderer));
            foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAsset.instance.badApple;
            foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
        }
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if( snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            scoreManager.IncreaseScore(1);
            SpawnFood();
            return true;
        }
        else
        {
            return false;
        }
    }
}
