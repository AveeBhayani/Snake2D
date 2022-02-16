using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBoostController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
            //SoundManager.Instance.Play(Sounds.BoostPickUp);
            Destroy(gameObject);       
    }
}
