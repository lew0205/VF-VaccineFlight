using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.shotLv < 5)
            {
                GameManager.Instance.shotLv++;
            }
            else
            {
                GameManager.Instance.ScoreUp(10);
            }
            GameManager.Instance.usedItems++;
            SoundManager.Instance.PlaySoundEffect("Item", clip, .1f);
            Destroy(gameObject);
        }
    }
}
