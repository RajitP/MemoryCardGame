using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diff_level : MonoBehaviour {

    // Use this for initialization

    public static float diff;

    public bool get_diff()
    {
        if (diff > 0.4f)
            return true;
        else
            return false;
    }

    public void set_diff(float d)
    {
        diff = d;
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(diff);
		
	}
}
