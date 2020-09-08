using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour {
    [SerializeField] List<GameObject> customersList;//only for setting prefabs in editoe, converted on Start() to  activeCusomers with active property

    public Vector3 m_initialCustomersPositionRight;
    public Vector3 m_initialCustomersPositionLeft;
    public Vector3[] m_customerPositions;

    private int numOfInitialPositions = 2;
    private int spawnInterval = 5;
    private float m_timer = 0;



    void Start () {
        InstansiateAndDisableAllCustomers();
        StartCoroutine(SpawningIntervalChange());
    }

    private void InstansiateAndDisableAllCustomers()
    {
        for (int i = 0; i < customersList.Capacity; i++)
        {
            customersList[i] = Instantiate(customersList[i], ChooseInitialCustomerPosition(), new Quaternion(0, 0, 0, 0));//todo test the quaternion thing(this is the rotation)
            customersList[i].SetActive(false);
        }
    }

    IEnumerator SpawningIntervalChange()
    {
        while (true)//todo:: change to some kind of mission started
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCustomer();
        }
    }

    // Update is called once per frame
    void Update () {
        //count time
        m_timer += Time.deltaTime;


        if (m_timer < 30 && m_timer > 50)
        {

        }

    }

    void SpawnCustomer()
    {
        for (int i = 0; i < customersList.Capacity; i++)
        {
            if(!customersList[i].activeSelf)
            {
                customersList[i].SetActive(true);
                break;
            }
        }
    }
    //random decision;
    private Vector3 ChooseInitialCustomerPosition()
    {
        int leftOrRight = UnityEngine.Random.Range(0, numOfInitialPositions);
        switch (leftOrRight)
        {
            case 0:
                return m_initialCustomersPositionLeft;
            case 1:
                return m_initialCustomersPositionRight;
            default:
                return m_initialCustomersPositionRight;
        }
    }

    public void TargetReached(GameObject reachingCustomer)
    {
        for (int i = 0; i < customersList.Capacity; i++)
        {
            if (GameObject.ReferenceEquals(customersList[i], reachingCustomer))
            {
                customersList[i].SetActive(false);
                break;
            }
        }

    }
}
