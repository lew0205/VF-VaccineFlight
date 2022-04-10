using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Covid : MonoBehaviour
{
    public float hp;
    public float maxhp;
    public float moveSpeed;
    public float atk;
    public float atkSpeed;
    public float score;

    public Animator anim;

    protected Vector2 vec;

    public List<GameObject> Projectiles;

    private void Awake()
    {
        if (GameManager.Instance.stage == GameManager.Stage.stage2)
        {
            maxhp *= 2;
            atk *= 2;
        }

        hp = maxhp;
        anim = GetComponent<Animator>();
    }

    public void GetDamaged(float damage)
    {
        anim.SetTrigger("GetHitted");
        anim.ResetTrigger("GetHitted");
        hp = Mathf.Max(hp - damage, 0);
    }

    protected bool isDownExited()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.y < -0.3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected bool isUpExited()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.y > 1.3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
