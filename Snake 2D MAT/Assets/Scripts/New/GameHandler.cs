using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private SnakeController snakeController;
    [SerializeField] private ScoreManager scoreManager;
    private LevelGrid levelGrid;

    void Start()
    {
        levelGrid = new LevelGrid(20, 20);
        snakeController.Setup(levelGrid);
        levelGrid.Setup(snakeController, scoreManager);
    }   
}
