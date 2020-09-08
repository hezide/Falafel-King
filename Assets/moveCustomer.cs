using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCustomer : MonoBehaviour {
    Animation           m_customerAnimator;
    Vector3             m_target,m_sellerPosition;
    Quaternion          m_sellerRotation;
    CustomerController  m_customerController;
    bool                m_receivedPita;
    private Vector3     m_originalPosition;//this holds the original slot that the customer has taken, before it's destroys the array is set back
    // Use this for initialization
    void Start () {
    }
    void OnEnable()
    {
        m_customerAnimator = GetComponent<Animation>();
        m_customerAnimator.Play("walk 1");

        m_customerController = GameObject.Find("CustomerController").GetComponent<CustomerController>();

        m_target = GetAvailableSlotAndClearFromArray();

        m_sellerPosition = GameObject.Find("city_stall/Main Camera").transform.position;

        m_sellerRotation = Quaternion.LookRotation(m_sellerPosition);
        m_receivedPita = false;
    }
    void OnDisable()
    {
        RestoreOriginalSlotToArray();
    }
    private Vector3 GetAvailableSlotAndClearFromArray()
    {
        int safetyIndex = 0;
        while (true)
        {

            int selectedIndex = UnityEngine.Random.Range(0, m_customerController.m_customerPositions.Length);
            if (!IsReset(m_customerController.m_customerPositions[selectedIndex]))
            {
                m_target = m_customerController.m_customerPositions[selectedIndex];
                m_originalPosition = m_target;
                ResetVector(selectedIndex);
                return m_target;
            }
            if (safetyIndex == 50)
                Destroy(this.gameObject);//dirty but protects from infinite loop

            safetyIndex++;
        }
    }

    private void ResetVector(int selectedIndex)
    {
        m_customerController.m_customerPositions[selectedIndex].x = 0;
        m_customerController.m_customerPositions[selectedIndex].y = 0;
        m_customerController.m_customerPositions[selectedIndex].z = 0;
    }

    private bool IsReset(Vector3 vector3)
    {
        if (vector3.x == 0 && vector3.y == 0 && vector3.z == 0)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update () {

        //target reached after receiving pita
        if (m_receivedPita && transform.position == m_target)
        {
            m_customerController.TargetReached(this.gameObject);
            //Destroy(this.gameObject);
            return; 
        }

        float step = 1.5f * Time.deltaTime;
        
        //customer did not reach his position yet
        if (transform.position != m_target)
        {
            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
            //orient towards walking direction
            var lookPos = m_target - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
        }
        //customer reached the target, need to orient toward the seller
        else
        {   
            
            if (transform.rotation != m_sellerRotation)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, m_sellerRotation, Time.deltaTime * 1);

            }
        }
    }

    public void  ReceivePita()
    {
        m_receivedPita = true;
        RestoreOriginalSlotToArray();
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                m_target = m_customerController.m_initialCustomersPositionLeft;
                break;
            case 1:
                m_target = m_customerController.m_initialCustomersPositionRight;
                break;
        }
    }

    private void RestoreOriginalSlotToArray()
    {
        for (int i = 0; i < m_customerController.m_customerPositions.Length; i++)
        {
            if (IsReset(m_customerController.m_customerPositions[i]))
                m_customerController.m_customerPositions[i] = m_originalPosition;
        }
    }
}
