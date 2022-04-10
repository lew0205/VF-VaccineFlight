using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandage : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Heal(20);
            GameManager.Instance.usedItems++;
            SoundManager.Instance.PlaySoundEffect("Item", clip, .1f);
            Destroy(gameObject);
        }
    }
}
