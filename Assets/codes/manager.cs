using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public static manager Instance;
    public static Vector2 youshang;
    public GameObject pt;
    public GameObject ci;
    public GameObject db;//碰到死
    public GameObject pz;
    public float crtRate;
    public Transform yi, er, san, si, wu;
    public List<GameObject> ptz = new List<GameObject>();
    bool isCifront;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void crtPT()
    {
        //        Debug.Log(isCifront);
        int randomNum = Random.Range(0, 25);
        bool isCi;
        if (isCifront)
        {
            if (randomNum < 24)
            {
                isCi = false;
            }
            else
            {
                isCi = true;
            }
        }
        else
        {
            if (randomNum < 20)
            {
                isCi = false;
            }
            else
            {
                isCi = true;
            }
        }
        //        Debug.Log(ptz);
        int a = Random.Range(0, 5);
        Vector3 pos = new Vector3(0, 0, 0);
        switch (a)
        {
            case 0:
                pos = yi.position;
                break;
            case 1:
                pos = er.position;
                break;
            case 2:
                pos = san.position;
                break;
            case 3:
                pos = si.position;
                break;
            case 4:
                pos = wu.position;
                break;
            default:
                break;
        }
        if (!isCi)
        {
            isCifront = false;
            GameObject pingtaii = PoolManager.Instance.GetObject(pt);
            //pingtaii.GetComponent<pltfm>().sr.material.color = new Color(0.8f, 0, 0);
            ptz.Add(pingtaii);
            pingtaii.transform.position = pos;
        }
        else
        {
            isCifront = true;
            GameObject pingtaii = PoolManager.Instance.GetObject(ci);
            pingtaii.transform.position = pos;
        }
        //Debug.Log("ds");
        //Instantiate(pt, pos, Quaternion.identity);
    }
    void Start()
    {
        Vector2 EndPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));//右上
        youshang = EndPos;
        Instantiate(db, new Vector3(0, -youshang.y - 0.5f, 0), Quaternion.identity);
        Instantiate(db, new Vector3(0, youshang.y + 0.3f, 0), Quaternion.identity);
        Instantiate(pz, new Vector3(youshang.x, 0, 0), Quaternion.identity);
        Instantiate(pz, new Vector3(-youshang.x, 0, 0), Quaternion.identity);
        GameObject pingtaii = PoolManager.Instance.GetObject(pt);
        pingtaii.transform.position = san.position;
        //pingtaii.GetComponent<pltfm>().sr.material.color = new Color(0.8f, 0, 0);
        ptz.Add(pingtaii);
        InvokeRepeating("crtPT", crtRate, crtRate);
    }
}
