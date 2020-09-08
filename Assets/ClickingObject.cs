using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickingObject : MonoBehaviour {
    Camera cam;
    ManaController m_manaController;
    [SerializeField] string[] possibleCustomers;
    FoodController m_foodController;
    // Use this for initialization
    void Start () {
       // GameObject cityStall = GameObject.Find("city_stall");
        cam = Camera.main;
        m_foodController = GameObject.Find("FoodController").GetComponent<FoodController>();
        m_manaController = GameObject.Find("ManaController").GetComponent<ManaController>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // in all the ingredients, if the count is really decreased, the object will be destroyed
                if (hit.transform.gameObject.tag == "Hummus")
                {
                    if (m_manaController.AddIngredient(ManaController.ManaIngredients.Hummus))
                        m_foodController.RemoveSingleIngredient(ManaController.ManaIngredients.Hummus, hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "Fries")
                {
                    if (m_manaController.AddIngredient(ManaController.ManaIngredients.Fries))
                        m_foodController.RemoveSingleIngredient(ManaController.ManaIngredients.Fries, hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "Salad")
                {
                    if (m_manaController.AddIngredient(ManaController.ManaIngredients.Salad))
                        m_foodController.RemoveSingleIngredient(ManaController.ManaIngredients.Salad, hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "Falafel")
                {
                    if (m_manaController.AddIngredient(ManaController.ManaIngredients.Falafel))
                        m_foodController.RemoveSingleIngredient(ManaController.ManaIngredients.Falafel, hit.transform.gameObject);
                }
                else if (StringArrayContains(possibleCustomers, hit.transform.gameObject.tag))
                {
                    if (m_manaController.ManaCompleted())
                    {
                        hit.transform.GetComponent<RageController>().ReceivePita();
                        hit.transform.GetComponent<moveCustomer>().ReceivePita();
                    }
                }
            }
        }
    }

    private bool StringArrayContains(string[] possibleCustomers, string tag)
    {
        foreach (string name in possibleCustomers)
        {
            if (name == tag)
                return true;
        }
        return false;
    }

}
