using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlTest : MonoBehaviour
{
    /// <summary>
    /// 动画里Trigger返回不要取消勾选has exit time!!!!!!!!!!!!!!!!-----------------------------------------------
    /// </summary>

    private Animator ani;
    private int Hp;
    private bool isGround = false;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp < 0)
        {
            return;
        }
        //
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            this.transform.localScale = new Vector3(horizontal > 0 ? -0.5f : 0.5f, 0.5f, 0.5f);
            this.transform.Translate(Vector2.right * 1 * Time.deltaTime * horizontal);
            ani.SetBool("R", true);
            if (Input.GetKeyDown(KeyCode.U))
            {
                ani.SetTrigger("S2");
            }
        }
        else
        {
            ani.SetBool("R", false);

        }
        if (Input.GetKeyDown(KeyCode.Space)&&isGround)
        {
            //ani.SetBool("JBool",true);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ani.SetTrigger("S1");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = true;
            Debug.Log(isGround);

            ani.SetBool("JBool", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGround = false;
            ani.SetBool("JBool", true);
        }
    }
}
