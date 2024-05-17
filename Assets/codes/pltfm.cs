using System;
using UnityEngine;

public class pltfm : MonoBehaviour
{
    public float spdup;
    public float shuiPingSpd;
    private Rigidbody2D rb;
    bool isUp;
    public bool isCi;
    int direction;
    bool isMove;
    bool isBian;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        isUp = false;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, spdup);
        direction = 1;
        int randomNum = UnityEngine.Random.Range(0, 25);
        if (randomNum < 18)
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(transform.position.x) <= manager.youshang.x * 0.6)
        {
            isBian = false;
        }
        if (!isBian && Math.Abs(transform.position.x) >= manager.youshang.x)
        {
            isBian = true;
            direction = -direction;
        }
        if (isMove)
        {
            rb.velocity = new Vector2(shuiPingSpd * direction, spdup);
        }
        if (!isUp && transform.position.y >= manager.youshang.y)
        {
            isUp = true;
            transform.position = new Vector3(0, 0, 0);
            //sr.material.color = new Color(0, 0, 0);
            manager.Instance.ptz.Remove(gameObject);
            if (isCi)
            {
                PoolManager.Instance.SetInPool(manager.Instance.ci, gameObject);
            }
            else
            {
                PoolManager.Instance.SetInPool(manager.Instance.pt, gameObject);
            }
            //Destroy(gameObject);
        }
    }
}
