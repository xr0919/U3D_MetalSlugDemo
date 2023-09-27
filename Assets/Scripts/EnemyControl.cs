using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int Hp = 1;
    private Animator ani;
    private bool showEnd = false;
    //获取玩家
    private PlayerControl player;
    //定时器
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
        //获取与玩家的距离
        float dis = Vector3.Distance(player.transform.position, transform.position);
        Debug.Log(dis);
        timer += Time.deltaTime;
        if (dis < 0.5f)
        {
            //攻击
            ani.SetBool("IsR", false);
            //不能让敌人一直攻击 用计时器
            if (timer > 5)
            {
                //3秒攻击一次
                timer = 0;
                ani.SetTrigger("Atk");

            }
        }
        else if (dis < 2f)
        {
            //显示
            if (showEnd == false)
            {
                ani.SetTrigger("S");
            }
            else
            {
                //追击
                ani.SetBool("IsR", true);
                this.transform.Translate(Vector2.left * 0.3f * Time.deltaTime);
            }
        }
        else
        {
            if (showEnd)
            {
                //追击
                ani.SetBool("IsR", true);
                this.transform.Translate(Vector2.left * 0.3f * Time.deltaTime);
            }
        }
    }
    //在动画帧数中设置事件
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
                //死亡先销毁刚体碰撞体 再延时销毁gameObj，然后动画
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<CapsuleCollider2D>());
                Destroy(gameObject, 3f);
                ani.SetTrigger("Die");

            }
        }
    }
}
