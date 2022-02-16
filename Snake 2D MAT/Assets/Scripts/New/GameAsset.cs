using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    public static GameAsset instance;

    public Sprite snakeHead;
    public Sprite snakeBody;
    public Sprite foodSprite;
    public Sprite badApple;

    public void Awake()
    {
        instance = this;
    }
}
