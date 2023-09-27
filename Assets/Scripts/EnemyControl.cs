using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int Hp = 1;
    private Animator ani;
    private bool showEnd = false;
    //��ȡ���
    private PlayerControl player;
    //��ʱ��
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            return;
        }
        //��ȡ����ҵľ���
        float dis = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(dis);
        timer += Time.deltaTime;
        if (dis < 0.5f)
        {
            //����
            ani.SetBool("IsR", false);
            //�����õ���һֱ���� �ü�ʱ��
            if (timer > 5)
            {
                //3�빥��һ��
                timer = 0;
                ani.SetTrigger("Atk");

            }
        }
        else if (dis < 2f)
        {
            //��ʾ
            if (showEnd == false)
            {
                ani.SetTrigger("S");
            }
            else
            {
                //׷��
                ani.SetBool("IsR", true);
                this.transform.Translate(Vector2.left * 0.3f * Time.deltaTime);
            }
        }
        else
        {
            if (showEnd)
            {
                //׷��
                ani.SetBool("IsR", true);
                this.transform.Translate(Vector2.left * 0.3f * Time.deltaTime);
            }
        }
    }
    //�ڶ���֡���������¼�
    public void ShowEnd()
    {
        showEnd = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "bullet")
        {
            Hp--;
            if(Hp < 0)
            {
                //���������ٸ�����ײ�� ����ʱ����gameObj��Ȼ�󶯻�
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<CapsuleCollider2D>());
                Destroy(gameObject, 3f);
                ani.SetTrigger("Die");

            }
        }
    }
}
