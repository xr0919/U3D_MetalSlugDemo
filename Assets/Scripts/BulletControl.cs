using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    //·½Ïò
    public float dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector2.right * 3 * (dir > 0 ? 1 : -1) * Time.deltaTime);
    }
}
