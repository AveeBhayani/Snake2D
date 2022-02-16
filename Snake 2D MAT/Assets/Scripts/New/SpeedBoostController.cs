using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostController : MonoBehaviour
{
    public SnakeController snakeController;
    private void OnTriggerEnter2D(Collider2D other)
    {
           //SoundManager.Instance.Play(Sounds.BoostPickUp);
            Destroy(gameObject);
            snakeController.SpeedBoostActive();       
    }
}
