using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSUP : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Player.GetComponent<PlayerController>().MoveSpeedUp();
            GameManager.Instance.usedItems++;
            SoundManager.Instance.PlaySoundEffect("Item", clip, .1f);
            Destroy(gameObject);
        }
    }
}
