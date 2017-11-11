using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text matchtext;

    public Text pltext;
    public Text aitext;

    private GameObject _aig;

    public bool _init = false;
    private int _matches = 13;

    //private List<GameObject>[] _mem;

    public bool turn;

    public int ck;

    bool sh = false;

	// Use this for initialization
	void Start () {
        // initializecards();
        /*int i;

        for (i = 0; i < 13; i++)
        {
            _mem[i] = new List<GameObject>();
        }*/
        turn = false;
        _aig = GameObject.FindGameObjectWithTag("AI");
        ck = 0;
        show();

    }
	
	// Update is called once per frame
	void Update () {

        if(!_init)
        {
            initializecards();
        }

        //if(Input.GetTouch(0).phase==TouchPhase.Ended)
        if(Input.GetMouseButtonUp(0)&& !turn)
        {
            /*float rand = Random.Range(0, 1.0f);
            if(rand<0.7f)
            {

            }*/
            ck++;
            checkCards();
        }
		
	}

    void show()
    {
        StartCoroutine(pause());
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
        foreach(GameObject c in cards)
        {
            c.GetComponent<card>().flipcard();
        }
        yield return new WaitForSeconds(6);
        foreach (GameObject c in cards)
        {
            c.GetComponent<card>().flipcard();
        }
        sh = true;
    }

    void initializecards()
    {
        for(int id=0; id<2;id++)
        {
            for(int i=1;i<14;i++)
            {
                bool test = false;
                int choice = 0;
                while(!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice].GetComponent<card>().initialized);
                }
                cards[choice].GetComponent<card>().cardvalue = i;
                cards[choice].GetComponent<card>().initialized = true;

            }
        }

        foreach(GameObject c in cards)
        {
            c.GetComponent<card>().setupGraphics();
        }
        if(!_init)
        {
            _init = true;
        }
    }

    public Sprite getCardBack()
    {
        return cardBack;
    }

    public Sprite getCardFace(int i)
    {
        //Debug.Log(i);
        return cardFace[i-1];
    }
    
    public void checkCards()
    {
        List<int> c = new List<int>();

        for (int i=0;i<cards.Length;i++)
        {
            if(cards[i].GetComponent<card>().state==1 && sh)
            {
                c.Add(i);
                foreach(int it in c)
                {
                    //Debug.Log(it);
                }
                //Debug.Log("#");
            }
        }
        if(c.Count==2)
        {
            cardComparison(c);
        }
    }

    public void p_turn()
    {
        turn = false;
        //Debug.Log(ck);
    }

    void cardComparison(List<int> c)
    {
        card.DO_NOT = true;
        bool del = false;

        int x = 0;
        if (cards[c[0]].GetComponent<card>().cardvalue == cards[c[1]].GetComponent<card>().cardvalue)
        {
            x = 2;
            _matches--;
            matchtext.text = "Number of matches:" + _matches;
            if (_matches == 0)
            {
                SceneManager.LoadScene("menu");
            }
        }
        else
            del = true;

        for(int i=0;i < c.Count; i++)
        {
            _aig.GetComponent<AI>().addlist(cards[c[i]].GetComponent<card>().cardvalue, cards[c[i]].GetComponent<card>().gameObject);
            cards[c[i]].GetComponent<card>().state = x;
            cards[c[i]].GetComponent<card>().falsecheck();
        }

        if (del)
        {
            turn = true;
            Debug.Log("turn call");
            _aig.GetComponent<AI>().turn_call();
        }
           
    }

    public void checkCards_ai()
    {
        List<int> ca = new List<int>();

        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i].GetComponent<card>().state == 1)
            {
                ca.Add(i);
                foreach (int it in ca)
                {
                    //Debug.Log(it);
                }
                //Debug.Log("#");
            }
        }
        if (ca.Count == 2)
        {
            cardComparison_ai(ca);
        }
    }

    void cardComparison_ai(List<int> c)
    {
        card.DO_NOT = true;
        bool del = false;

        int x = 0;
        if (cards[c[0]].GetComponent<card>().cardvalue == cards[c[1]].GetComponent<card>().cardvalue)
        {
            x = 2;
            _matches--;
            matchtext.text = "Number of matches:" + _matches;
            //_aig.GetComponent<AI>().turn_call();
            if (_matches == 0)
            {
                SceneManager.LoadScene("menu");
            }
        }
        else
            del = true;

        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<card>().state = x;
            cards[c[i]].GetComponent<card>().falsecheck();
        }

        if (del)
            turn = false;
        else
            _aig.GetComponent<AI>().turn_call();

    }
}
