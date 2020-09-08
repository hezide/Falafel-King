using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {
    private int m_score = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Score is:" + m_score);
	}
    public void GoodIngredientAdded()
    {
        m_score ++;
    }
    public void BadIngredientAdded()
    {
        m_score-=5;
        if (m_score < 0)
            m_score = 0;
    }
    public void ManaWasGivenToCustomer()
    {
        m_score += 5;
    }
}
