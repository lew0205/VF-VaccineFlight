using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PainKiller : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.PainKill(20);
            GameManager.Instance.usedItems++;
            SoundManager.Instance.PlaySoundEffect("Item", clip, .1f);
            Destroy(gameObject);
        }
    }
}
