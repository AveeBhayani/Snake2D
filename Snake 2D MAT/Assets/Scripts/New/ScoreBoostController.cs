using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoostController : MonoBehaviour
{
    public ScoreManager ScoreManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
            //SoundManager.Instance.Play(Sounds.BoostPickUp);
            Destroy(gameObject);
            ScoreManager.ScoreBoostActive();
    }
}
