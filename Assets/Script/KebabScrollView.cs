
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KebabScrollView : MonoBehaviour
{

    public int Children = 0;  //孩子个数
    public float R = 20;//圆半径
    private float startScale =
        3.1f;//起始缩放率
    private float endScale = 1.0f;//最终缩放率
    public float ScaleReduction = 0.0f;//缩放衰减率

    public UISprite LeftButton;
    public UISprite RightButton;
    public UISprite StartButton;
    public UISprite QuitButton;


    public GameObject childPrefab;
    private List<Vector3> KebabPosList = new List<Vector3>();
    private List<Transform> KebabTransList = new List<Transform>();
    private List<int> KebabDepthList = new List<int>();
    private List<Vector3> KebabScaleList = new List<Vector3>();
    private List<float> reDuctionList = new List<float>();


    public struct PreviouKebabItem
    {
        public Vector3 localPos;
        public Vector3 localScale;
        public Transform trans;
        public int depth;

    }


    PreviouKebabItem previousItem = new PreviouKebabItem();


    void Awake()
    {
        if (LeftButton != null && RightButton != null)
        {
            UIEventListener.Get(LeftButton.gameObject).onClick = (x) => { ScrollKebabLeft(); };
            UIEventListener.Get(RightButton.gameObject).onClick = (x) => { ScrollKebabRight(); };
        }
        if (StartButton != null && QuitButton != null)
        {
            UIEventListener.Get(StartButton.gameObject).onClick = (x) => { StartGame(); };
            UIEventListener.Get(QuitButton.gameObject).onClick = (x) => { BackToMainMenu(); };
        }


    }


    // Use this for initialization
    void Start()
    {
        if (Children != 0)
        {
            ScaleReduction = (startScale - endScale) / Children;
        }
        else
        {
            ScaleReduction = 0.01f;
        }

        InitKebabItem();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ScrollKebabRight();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ScrollKebabLeft();
        }
    }


    private void OnTweenFinished(GameObject obj)
    {
        Debug.Log(obj.name + " tween finished");
    }



    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="_kdebabPosList"></param>
    public void InitKebabItem()
    {
        if (KebabPosList == null) KebabPosList = new List<Vector3>();


        //1.生成坐标点
        float perPI = Mathf.PI * 2.0f / Children;
        for (int i = 0; i < Children; i++)
        {
            float z = R * Mathf.Sin(perPI * i - Mathf.PI / 2);
            float x = R * Mathf.Cos(perPI * i - Mathf.PI / 2);
            KebabPosList.Add(new Vector3(x, 0.0f, z));

        }

        //2.实例化对应的孩子
        for (int i = 0; i < KebabPosList.Count; i++)
        {
            GameObject child = Instantiate(childPrefab);
            if (child != null)
            {
                KebabTransList.Add(child.transform);
                UILabel label = child.GetComponentInChildren<UILabel>();
                if (label != null)
                {
                    label.text = i.ToString();
                }
                child.name = i.ToString();


                UIEventListener.Get(child.transform.Find("Sprite").gameObject).onClick = OnItemClick;
                AddChild(gameObject.transform, child);
                child.SetActive(true);
            }
        }

        //3.把孩子的坐标设置成对应的位置


        for (int i = 0; i < KebabPosList.Count; i++)
        {
            KebabTransList[i].gameObject.transform.localPosition = KebabPosList[i];
        }

        //4.设置depth
        KebabDepthList.Clear();
        reDuctionList.Clear();
        KebabScaleList.Clear();


        for (int i = 0; i < Children; i++)
        {
            KebabDepthList.Add(0);
        }

        for (int i = 0; i < Children; i++)
        {
            KebabScaleList.Add(Vector3.zero);
        }

        if (Children % 2 != 0)
        {

            int depth = Children / 2 + 1;

            for (int i = 0; i <= Children / 2; i++)//右边
            {

                KebabDepthList[i] = depth;
                depth -= 1;
            }

            depth += 1;
            for (int i = Children / 2 + 1; i < Children; i++)
            {
                KebabDepthList[i] = depth;
                depth += 1;
            }

        }
        else
        {
            int left = Children / 2 - 1;
            int right = Children / 2;

            int depth = left + 1;
            for (int i = 0; i <= left; i++)
            {
                KebabDepthList[i] = depth;
                depth -= 1;
            }

            for (int i = right; i < Children; i++)
            {
                KebabDepthList[i] = depth;
                depth += 1;
            }
        }



        //for (int i = 0; i < KebabDepthList.Count; i++)
        //{
        //    Debug.Log("[" + i + "]=" + KebabDepthList[i]);
        //}

        for (int i = 0; i < KebabTransList.Count; i++)
        {
            UIPanel up = KebabTransList[i].gameObject.GetComponent<UIPanel>();
            up.depth = KebabDepthList[i];

            Vector3 localScale = new Vector3(endScale + KebabDepthList[i] * ScaleReduction, endScale + KebabDepthList[i] * ScaleReduction, endScale + KebabDepthList[i] * ScaleReduction);
            //Debug.Log(i + "|localScale:" + localScale);
            KebabScaleList[i] = localScale;
            up.gameObject.transform.localScale = localScale;
        } 



    }



    public void OnItemClick(GameObject obj)
    {
        Debug.Log(obj.name + " was click");
    }


    /// <summary>
    /// 刷新KebabScrollView
    /// </summary>
    public void RefreshKebabScrollView()
    {

    }


    /// <summary>
    /// 向左滚动
    /// </summary>
    public void ScrollKebabRight()
    {

        if (KebabPosList.Count != 0 && KebabTransList.Count != 0 && KebabDepthList.Count != 0)
        {

            Vector3 headPos         = KebabPosList[0];
            Vector3 headScale       = KebabScaleList[0];
            int headDepth           = KebabDepthList[0];

            for (int i = 0; i < KebabPosList.Count - 1; i++)
            {

                TweenPosition tp    = KebabTransList[i].GetComponent<TweenPosition>();
                TweenScale ts       = KebabTransList[i].GetComponent<TweenScale>();
                if (tp != null && ts != null)
                {
                    tp.from         = KebabPosList[i];
                    tp.to           = KebabPosList[i + 1];
                    ts.from         = KebabScaleList[i];
                    ts.to           = KebabScaleList[i + 1];
                    tp.ResetToBeginning();
                    ts.ResetToBeginning();
                    tp.enabled      = true;
                    ts.enabled      = true;
                }

                KebabPosList[i]     = KebabPosList[i + 1];
                KebabDepthList[i]   = KebabDepthList[i + 1];
                KebabScaleList[i]   = KebabScaleList[i + 1];

            }


            TweenPosition lasttp = KebabTransList[KebabPosList.Count - 1].GetComponent<TweenPosition>();
            TweenScale lastts = KebabTransList[KebabPosList.Count - 1].GetComponent<TweenScale>();
            if (lasttp != null && lastts != null)
            {
                lasttp.from         = KebabPosList[KebabPosList.Count - 1];
                lasttp.to           = headPos;
                lastts.from         = KebabScaleList[KebabPosList.Count - 1];
                lastts.to           = headScale;
                lasttp.ResetToBeginning();
                lastts.ResetToBeginning();
                lasttp.enabled      = true;
                lastts.enabled      = true;
            }
            KebabPosList[KebabPosList.Count - 1] = headPos;
            KebabScaleList[KebabScaleList.Count - 1] = headScale;
            KebabDepthList[KebabDepthList.Count - 1] = headDepth;
            StartCoroutine(ChangeDepth());

        }
    }


    private IEnumerator ChangeDepth()
    {
        float time = KebabTransList[0].GetComponent<TweenPosition>().duration;
        yield return new WaitForSeconds(time / 2);
        for (int i = 0; i < KebabPosList.Count; i++)
        {
            UIPanel panel = KebabTransList[i].GetComponent<UIPanel>();
            BoxCollider boxCollider = KebabTransList[i].GetComponent<BoxCollider>();
            if (panel != null)
            {
                panel.depth = KebabDepthList[i];
            }

            if (boxCollider != null)
            {
                boxCollider.size = new Vector3(panel.width, panel.height, 0);
            }
        }
    }


    /// <summary>
    /// 向右滚动
    /// </summary>
    public void ScrollKebabLeft()
    {
        Debug.Log("!!");
        if (KebabPosList.Count != 0 && KebabTransList.Count != 0 && KebabDepthList.Count != 0)
        {


            Vector3 lastPos = KebabPosList[KebabPosList.Count - 1];
            Vector3 lastScale = KebabScaleList[KebabPosList.Count - 1];
            int lastDepth = KebabDepthList[KebabPosList.Count - 1];

            for (int i = KebabPosList.Count - 1; i > 0; i--)
            {

                TweenPosition tp = KebabTransList[i].GetComponent<TweenPosition>();
                TweenScale ts = KebabTransList[i].GetComponent<TweenScale>();
                if (tp != null && ts != null)
                {
                    tp.from     = KebabPosList[i];
                    tp.to       = KebabPosList[i - 1];
                    ts.from     = KebabScaleList[i];
                    ts.to       = KebabScaleList[i - 1];
                    tp.ResetToBeginning();
                    ts.ResetToBeginning();
                    tp.enabled  = true;
                    ts.enabled  = true;
                }

                KebabPosList[i]     = KebabPosList[i - 1];
                KebabDepthList[i]   = KebabDepthList[i - 1];
                KebabScaleList[i]   = KebabScaleList[i - 1];

            }



            TweenPosition headtp = KebabTransList[0].GetComponent<TweenPosition>();
            TweenScale headts = KebabTransList[0].GetComponent<TweenScale>();
            if (headtp != null && headts != null)
            {
                headtp.from     = KebabPosList[0];
                headtp.to       = lastPos;
                headts.from     = KebabScaleList[0];
                headts.to       = lastScale;
                headtp.ResetToBeginning();
                headts.ResetToBeginning();
                headtp.enabled  = true;
                headts.enabled  = true;
            }
            KebabPosList[0]     = lastPos;
            KebabScaleList[0]   = lastScale;
            KebabDepthList[0]   = lastDepth;
            StartCoroutine(ChangeDepth());
        }




    }



    /// <summary>
    ///
    /// </summary>
    /// <param name="_parent"></param>
    /// <param name="_child"></param>
    public void AddChild(Transform _parent, GameObject _child)
    {
        _child.transform.parent = _parent;
        _child.transform.localPosition = Vector3.zero;
        _child.transform.localScale = Vector3.one;
    }


    //start Game

    public void StartGame() {

        Debug.Log("!!");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);




    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }








}
