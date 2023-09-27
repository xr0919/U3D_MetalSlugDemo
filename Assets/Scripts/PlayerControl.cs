using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Animator ani;
    public float Hp = 100;
    private float Speed = 1;
    private bool isGround;

    //�ӵ�Ԥ����
    public GameObject BulletPre;
    //�����
    public Transform FirePoint;
    //�����ʱ��
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            return;
        }
        //�ƶ�
        float horizontal = Input.GetAxis("Horizontal");
        //ת��
        if (horizontal != 0)
        {
            this.transform.localScale = new Vector3(horizontal > 0 ? -0.5f : 0.5f, 0.5f, 0.5f);
            //�ƶ�
            this.transform.Translate(Vector2.right * horizontal * Speed * Time.deltaTime);
            ani.SetBool("IsRun", true);
        }
        else
        {
            ani.SetBool("IsRun", false);
        }


        //��Ծ
        if (Input.GetKeyDown(KeyCode.Space)&& isGround)
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
        }
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.U) && timer > 0.5f)
        {
            timer = 0;
            ani.SetTrigger("S");
            AudioManager.Instance.PlaySound("shoot");
            Instantiate(BulletPre, FirePoint.transform.position, FirePoint.rotation).GetComponent<BulletControl>().dir = transform.localScale.x * -1;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
            ani.SetBool("IsJump", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isGround = false;
            ani.SetBool("IsJump", true);
        }
    }


}
