using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour {

    public static bool DO_NOT = false;

    [SerializeField]
    private int _state;

    [SerializeField]
    private int _cardvalue;

    [SerializeField]
    private bool _initialized=false;

    private Sprite _cardback;
    private Sprite _cardface;

    private GameObject _manager;

    private GameObject _ai;



	// Use this for initialization
	void Start () {

       // _state = 0;
        //StartCoroutine(pause1());
        _state = 1;
        _manager = GameObject.FindGameObjectWithTag("Manager");
        _ai = GameObject.FindGameObjectWithTag("AI");
		
	}

    public void setupGraphics()
    {
        _cardback = _manager.GetComponent<GameManager>().getCardBack();
        _cardface = _manager.GetComponent<GameManager>().getCardFace(_cardvalue);

        flipcard();

    }

    public void flipcard()
    {
        //Debug.Log(_manager.GetComponent<GameManager>().turn);
        if (_state == 0)
            _state = 1;
        else if (_state == 1)
            _state = 0;

        if (_manager.GetComponent<GameManager>()._init && _state==1)
        {
            //_state = 1;
            //_ai.GetComponent<AI>().addlist(_cardvalue, gameObject);
            //Debug.Log("hu");
        }

        if (_state==0 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _cardback;
        }
        else if (_state == 1 && !DO_NOT)
        {
            GetComponent<Image>().sprite = _cardface;
        }

    }

    public int cardvalue
    {
        get { return _cardvalue; }
        set { _cardvalue = value; }
    }

    public int state
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }


    public void falsecheck()
    {
        StartCoroutine(pause());
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1);
            if (_state==0)
        {
            GetComponent<Image>().sprite = _cardback;
        }
        else if (_state == 1)
        {
            GetComponent<Image>().sprite = _cardface;
        }
        DO_NOT = false;
    }


    IEnumerator pause1()
    {
        yield return new WaitForSeconds(5);
       
    }

    // Update is called once per frame
    void Update () {
		
	}
}
