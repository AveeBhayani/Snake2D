using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    FoodSpawner FoodSpawner;
    public ScoreManager ScoreManager;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            ScoreManager.IncreaseScore(1);
            SoundManager.Instance.Play(Sounds.FoodPickUp);
            Destroy(gameObject);
            playerController.Grow();
        }
    }
}
