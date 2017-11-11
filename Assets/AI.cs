using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{


    //private List<GameObject>[] _mem = new List<GameObject>[13];

    private GameObject[,] _mem = new GameObject[13, 2]; 

    private Stack<GameObject> rem;

    private GameObject _man;
    private GameObject _cd;

    private GameObject temp;
    private Button but;

    public float difficulty;

    bool popped = false;

    public int flipped;

    private int[] ct = new int[13];     //default value is 0.

    public delegate void TestDelegate();
    //public TestDelegate method;

    // Use this for initialization
    void Start()
    {
        //int i;

        /* for(i=0;i<13;i++)
         {
            for(int j=0;j<2;j++)
            {
                _mem[i,j] = new GameObject;
            }
            //_mem[i] = new List<GameObject>();
         }*/
        _man = GameObject.FindGameObjectWithTag("Manager");
        //g = GameObject.FindGameObjectWithTag("EditorOnly");
        rem = new Stack<GameObject>();
        flipped = 0;
        //difficulty = diff_level.diff;

    }

    // Update is called once per frame
    void Update()
    {
        /*if (_man.GetComponent<GameManager>().turn && flipped<2)
        {
            
            if (rem.Count == 0)
            {
                //falsecheck_ai(playturn);
                StartCoroutine(pause_ai(playturn));
                //falsecheck_ai(playturn);
                //playturn();

            }
            else
            {
               // while(rem.Count!=0)
                {
                    //StartCoroutine(pause_ai(playturn));
                    //_man.GetComponent<GameManager>().checkCards();
                }
            }
        }
        else if (_man.GetComponent<GameManager>().turn && flipped == 2)
        {
            //_man.GetComponent<GameManager>().p_turn();
            //flipped = 0;
        }*/

    }

    public void turn_call()
    {
        if (_man.GetComponent<GameManager>().turn)// && flipped < 2)
        {

            if (rem.Count <=1)
            {
                //falsecheck_ai(playturn);
                StartCoroutine(pause_ai(playturn));
                //falsecheck_ai(playturn);
                //playturn();

            }
            else
            {
               // while(rem.Count!=0)
                {
                    StartCoroutine(pause_ai(popper));
                    //_man.GetComponent<GameManager>().checkCards();
                }
            }
        }
        /*else if (_man.GetComponent<GameManager>().turn && flipped == 2)
        {
            //_man.GetComponent<GameManager>().p_turn();
            //flipped = 0;
        }*/
    }

    public void addlist(int val, GameObject ob)
    {
       /* for(int i=0;i<13;i++)
        {
            if(_mem[i,0]||_mem[i,1])
            {
                //foreach(List<GameObject> l in _mem)
                {
                    Debug.Log("index: ");
                    Debug.Log(i);
                    Debug.Log("count: ");
                    Debug.Log(_mem[i].Count);
                    Debug.Log("#");
                }
            }
            Debug.Log("##");
        }*/
        float rand = Random.Range(0, 1.0f);
        if (ct[val - 1] < 2)
        {
            if (rand < difficulty && !_mem[val - 1, ct[val - 1]] && _mem[val - 1, 0] != ob)
            {
                //_mem[val - 1].Add(ob);
                Debug.Log(val - 1 + "" + ct[val - 1]);
                _mem[val - 1, ct[val - 1]] = ob;
                ct[val - 1]++;
            }
        }
        
        if (ct[val-1] == 2)
        {
           rem.Push(_mem[val-1,0]);
           rem.Push(_mem[val-1,1]);
            ct[val - 1] = 3;
        }
    }

    void playturn()
    {
        //Debug.Log("hulahula");
        bool valid = false;
        while (!valid)
        {
            int pick = Random.Range(0, 25);     //card list length
            temp = _man.GetComponent<GameManager>().cards[pick];
            //Debug.Log(x);
            but = _man.GetComponent<GameManager>().cards[pick].GetComponent<Button>();
            if (temp.GetComponent<card>().state==0)
            {
                //Debug.Log("played");
                valid = true;
                flipped++;
                _man.GetComponent<GameManager>().ck = 0;
                temp.GetComponent<card>().flipcard();
                //but.onClick.Invoke();
                
                addlist(temp.GetComponent<card>().cardvalue, temp);
            }
        }
    }

    void popper()
    {
        if(rem.Count!=0)
        {
            temp = rem.Peek();
            while (temp.GetComponent<card>().state == 2 && rem.Count != 0)
                temp = rem.Pop();
            if (rem.Count > 0)
                temp = rem.Pop();
            temp.GetComponent<card>().flipcard();
        }
        else
        {
            popped = true;
        }
        
    }
    
    public void falsecheck_ai(TestDelegate loc_meth)
    {
        StartCoroutine(pause_ai(loc_meth));
    }

    IEnumerator pause_ai(TestDelegate meth)
    {
        yield return new WaitForSeconds(2);
        meth();
        //Debug.Log("1");
        yield return new WaitForSeconds(1);
        meth();
        //Debug.Log("2");
        yield return new WaitForSeconds(1);
        //playturn(1);
        //playturn(2);
        //Debug.Log(_man.GetComponent<GameManager>().turn);
        _man.GetComponent<GameManager>().checkCards_ai();

        if(popped)
        {
            popped = false;
            StartCoroutine(pause_ai(playturn));
        }
        //_man.GetComponent<GameManager>().turn = false;
    }
}
