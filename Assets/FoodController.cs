using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {
    [SerializeField] GameObject HummusObject;
    [SerializeField] GameObject FriesObject;
    [SerializeField] GameObject SaladObject;
    [SerializeField] GameObject FalafelObject;
    [SerializeField] float      HummusMinX, HummusMaxX, HummusMinZ, HummusMaxZ, HummusY;
    [SerializeField] float      friesMinX, friesMaxX, friesMinZ, friesMaxZ, friesY;
    [SerializeField] float      saladMinX, saladMaxX, saladMinZ, saladMaxZ, saladY;
    [SerializeField] float      falafelMinX, falafelMaxX, falafelMinZ, falafelMaxZ,falafelY;
    Transform                   m_parent;
    Dictionary<ManaController.ManaIngredients, int> m_ingredientCount = new Dictionary<ManaController.ManaIngredients, int>();
    private readonly int m_maxIngredientCount = 20;
    // Use this for initialization
    IEnumerator Start () {
        initializeIngredientCount();
        m_parent = GameObject.Find("city_stall").transform;
        
        for (int i = 0; i < m_maxIngredientCount; i++)
        {
            CreateSingleIngredient(ManaController.ManaIngredients.Hummus,HummusObject);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < m_maxIngredientCount; i++)
        {
            CreateSingleIngredient(ManaController.ManaIngredients.Fries, FriesObject);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < m_maxIngredientCount; i++)
        {
            CreateSingleIngredient(ManaController.ManaIngredients.Salad, SaladObject);
            yield return new WaitForSeconds(0.01f);
        }
        for (int i = 0; i < m_maxIngredientCount; i++)
        {
            CreateSingleIngredient(ManaController.ManaIngredients.Falafel, FalafelObject);
            yield return new WaitForSeconds(0.01f);

        }

        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "3";
        t.fontSize = 3;
        t.transform.localEulerAngles += new Vector3(90, 0, 0);
        t.transform.localPosition += new Vector3(56f, 3f, 40f);
    }

    private void initializeIngredientCount()
    {
        m_ingredientCount[ManaController.ManaIngredients.Hummus] = 0;
        m_ingredientCount[ManaController.ManaIngredients.Fries] = 0;
        m_ingredientCount[ManaController.ManaIngredients.Salad] = 0;
        m_ingredientCount[ManaController.ManaIngredients.Falafel] = 0;

    }

    private void CreateSingleIngredient(ManaController.ManaIngredients ingredient,GameObject objectToInstantiate)
    {
        if (m_ingredientCount[ingredient] >= m_maxIngredientCount)
            return;
        
        GameObject instansiated;
        Vector3 pos = GetRandomPositionInRange(ingredient);
        instansiated = Instantiate(objectToInstantiate, pos, new Quaternion(0, 0, 0, 0), m_parent);
        m_ingredientCount[ingredient]++;
        instansiated.transform.localPosition = pos;
        instansiated.transform.Rotate(new Vector3(-90, 0, 0));
    }

    private Vector3 GetRandomPositionInRange(ManaController.ManaIngredients ingredient)
    {
        Vector3 res = new Vector3() ;
        switch (ingredient)
        {
            case ManaController.ManaIngredients.Hummus:
                {
                    res.x = UnityEngine.Random.Range(HummusMinX, HummusMaxX);
                    res.y = HummusY;
                    res.z = UnityEngine.Random.Range(HummusMinZ, HummusMaxZ);
                    break;
                }
            case ManaController.ManaIngredients.Fries:
                {
                    res.x = UnityEngine.Random.Range(friesMinX, friesMaxX);
                    res.y = friesY;
                    res.z = UnityEngine.Random.Range(friesMinZ, friesMaxZ);
                    break;
                }
            case ManaController.ManaIngredients.Salad:
                {
                    res.x = UnityEngine.Random.Range(saladMinX, saladMaxX);
                    res.y = saladY;
                    res.z = UnityEngine.Random.Range(saladMinZ, saladMaxZ);
                    break;
                }
            case ManaController.ManaIngredients.Falafel:
                {
                    res.x = UnityEngine.Random.Range(falafelMinX, falafelMaxX);
                    res.y = falafelY;
                    res.z = UnityEngine.Random.Range(falafelMinZ, falafelMaxZ);
                    break;
                }
        }
        return res;
    }

    // Update is called once per frame
    void Update () {

    }

    public void RemoveSingleIngredient(ManaController.ManaIngredients ingredientType,GameObject gameObject)
    {
        Destroy(gameObject);
        m_ingredientCount[ingredientType]--;
    }
    
    public void AddIngredients(ManaController.ManaIngredients ingredientType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateSingleIngredient(ingredientType, GetObjectByType(ingredientType));
        }
    }

    private GameObject GetObjectByType(ManaController.ManaIngredients ingredientType)
    {
        switch(ingredientType)
        {
            case ManaController.ManaIngredients.Hummus:
                return HummusObject;
            case ManaController.ManaIngredients.Fries:
                return FriesObject;
            case ManaController.ManaIngredients.Salad:
                return SaladObject;
            case ManaController.ManaIngredients.Falafel:
                return FalafelObject;
        }
        return null;
    }

    //public method for addition because unity's editor cant handle function parame
    public void Add5Hummus()
    {
        AddIngredients(ManaController.ManaIngredients.Hummus, 5);
    }

    public void Add5Fries()
    {
        AddIngredients(ManaController.ManaIngredients.Fries, 5);
    }

    public void Add5Salad()
    {
        AddIngredients(ManaController.ManaIngredients.Salad, 5);
    }

    public void Add5Falafel()
    {
        AddIngredients(ManaController.ManaIngredients.Falafel, 5);
    }
}
