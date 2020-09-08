using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RageController : MonoBehaviour {
    GameObject[] m_rage = new GameObject[6];
    int m_activeRageNum;
    int m_maxRage =5;
    int m_rageAdvanceInterval = 10;
    bool m_receivedPita;
	// Use this for initialization
	void Start () {

    }
    private void OnEnable()
    {
        m_rage[0] = transform.Find("Rage Bar/0").gameObject;
        m_rage[1] = transform.Find("Rage Bar/1").gameObject;
        m_rage[2] = transform.Find("Rage Bar/2").gameObject;
        m_rage[3] = transform.Find("Rage Bar/3").gameObject;
        m_rage[4] = transform.Find("Rage Bar/4").gameObject;
        m_rage[5] = transform.Find("Rage Bar/5").gameObject;
        
        m_activeRageNum = 0;
        m_rageAdvanceInterval = 10;
        m_receivedPita = false;

        UpdateRage();
        StartCoroutine("cycle");
    }
    private void OnDisable()
    {

    }
    // Update is called once per frame
    void Update () {
    }

    IEnumerator cycle()
    {

        while (true)
        {
            if (m_receivedPita)
                yield break;

            m_activeRageNum++;
            if(m_activeRageNum == m_maxRage)
            {
                GameOver();
            }
            m_activeRageNum %= m_maxRage + 1;
            UpdateRage();
            yield return new WaitForSeconds(m_rageAdvanceInterval);
        }
    }

    private void GameOver()
    {
        //toto:: save score reset data......
        SceneManager.LoadScene("GameOver");
    }

    void UpdateRage()
    {
        for(int i = 0 ; i <= m_maxRage; i++)
        {
            if (i == m_activeRageNum)
                m_rage[i].SetActive(true);
            else
                m_rage[i].SetActive(false);
        }
    }

    void ResetRage()
    {
        m_activeRageNum = 0;
        UpdateRage();
    }

    public void ReceivePita()
    {
        m_receivedPita = true;
        ResetRage();
    }
}
