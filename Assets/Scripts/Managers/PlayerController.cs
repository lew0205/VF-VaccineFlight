using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    public float moveSpeed;
    public float atkSpeed;

    public List<GameObject> Projectiles;

    public bool isASUp = false;
    public bool isMSUp = false;

    Coroutine ASUpCor;
    Coroutine MSUpCor;

    public float atkCnt = 0;

    public AudioClip clip;

    private void Start()
    {
        anim = GetComponent<Animator>();
        GameManager.Instance.Player = gameObject;
    }

    private void Update()
    {
        Move();
        if (Input.GetMouseButton(0) && atkCnt < 0)
        {
            Shot();
            SoundManager.Instance.PlaySoundEffect("Shot", clip,.05f);
        }
        atkCnt -= Time.deltaTime;
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 vec = new Vector2(h, v);

        transform.Translate(vec.normalized * moveSpeed * Time.deltaTime);

        if (h > 0)
        {
            anim.SetInteger("LR", 1);
        }
        else if (h < 0)
        {
            anim.SetInteger("LR", -1);
        }
        else
        {
            anim.SetInteger("LR", 0);
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < .02f) pos.x = .02f;
        if (pos.y < .05f) pos.y = .05f;
        if (pos.x > .98f) pos.x = .98f;
        if (pos.y > .95f) pos.y = .95f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Shot()
    {
        atkCnt = 1 / atkSpeed;
        var prj = Instantiate(Projectiles[GameManager.Instance.shotLv - 1], new Vector2(transform.position.x, transform.position.y + .3f), Quaternion.identity);
    }

    public void AttackSpeedUp()
    {
        if (isASUp)
        {
            StopCoroutine(ASUpCor);
            atkSpeed *= .5f;
            isASUp = false;
        }
        ASUpCor = StartCoroutine(ASUp());
    }

    IEnumerator ASUp()
    {
        atkSpeed *= 2;
        isASUp = true;
        yield return new WaitForSeconds(3);
        atkSpeed *= .5f;
        isASUp = false;
    }

    public void MoveSpeedUp()
    {
        if (isMSUp)
        {
            StopCoroutine(MSUpCor);
            moveSpeed *= .5f;
            isMSUp = false;
        }
        MSUpCor = StartCoroutine(MSUp());
    }

    IEnumerator MSUp()
    {
        moveSpeed *= 2;
        isMSUp = true;
        yield return new WaitForSeconds(3);
        moveSpeed *= .5f;
        isMSUp = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Covid"))
        {
            GameManager.Instance.GetDamaged(collision.GetComponent<Covid>().atk * .5f);
        }
    }
}
