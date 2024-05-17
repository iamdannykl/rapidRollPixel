using System;
using Unity.Mathematics;
using UnityEngine;

public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    public RapidRollPixel rp;
    public Vector2 drct;
    public Vector2 spd;
    public SpriteRenderer sr;
    public AudioSource ptsd;
    bool isOnPt;
    private Animator anim;
    Ray2D ray2D;
    void Awake()
    {
        rp = new RapidRollPixel();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void OnEnable()
    {
        rp.Enable();
    }
    void OnDisable()
    {
        rp.Disable();
    }
    void moveBall()
    {
        drct = rp.Player.Move.ReadValue<Vector2>();//获取速度
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("spd", math.abs(drct.x));
        if (drct.x < 0)
        {
            sr.flipX = true;
        }
        else if (drct.x > 0)
        {
            sr.flipX = false;
        }
        ray2D = new Ray2D(transform.position, Vector2.down);
        RaycastHit2D info = Physics2D.Raycast(ray2D.origin, ray2D.direction, 0.8f, 1 << 6);
        Debug.DrawLine(transform.position, transform.position + (Vector3)(0.8f * ray2D.direction), Color.red);
        if (info.collider != null)//如果发生了碰撞
        {
            //Debug.Log("yes");
            GameObject obj = info.collider.gameObject;
            //            Debug.Log(obj.name);
            if (obj.CompareTag("pt"))//用tag判断碰到了什么对象
                IsOnPt = true;
            else
                IsOnPt = false;
        }
        else
        {
            IsOnPt = false;
        }
    }
    public bool IsOnPt
    {
        get => isOnPt;
        set
        {
            if (value == isOnPt) return;
            if (value)
            {
                ptsd.Play();
            }
            isOnPt = value;
        }
    }
    void FixedUpdate()
    {
        moveBall();
        //Debug.Log(drct);
        transform.Translate(spd * drct.x * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "db")
        {
            if (manager.Instance.ptz.Count > 0)
                transform.position = manager.Instance.ptz[manager.Instance.ptz.Count - 1].transform.GetChild(0).transform.position;
            else
            {
                transform.position = new Vector3(0, 0, 0);
            }
        }
    }
}
