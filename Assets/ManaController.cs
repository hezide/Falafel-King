using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour {
    public enum ManaIngredients {  Hummus,Fries,Salad,Falafel}
    class Mana
    {
        private readonly int maxIngredientCount = 3;
        private Dictionary<ManaIngredients, int> m_ManaDictionary;

        public Mana()
        {
            m_ManaDictionary = new Dictionary<ManaIngredients, int>();
            RandomMana();
        }

        public int GetCountByIngredient(ManaIngredients ingredient)
        {
            return m_ManaDictionary[ingredient];
        }

        private void RandomMana()
        {
            m_ManaDictionary[ManaIngredients.Hummus] = GetRandomIngredientCount();
            m_ManaDictionary[ManaIngredients.Fries] = GetRandomIngredientCount();
            m_ManaDictionary[ManaIngredients.Salad] = GetRandomIngredientCount();
            m_ManaDictionary[ManaIngredients.Falafel] = GetRandomIngredientCount();
        }

        private int GetRandomIngredientCount()
        {
            return UnityEngine.Random.Range(1, maxIngredientCount + 1);
        }

        public bool AddIngredient(ManaIngredients ingredientType)
        {
            if(MaxReached(ingredientType))
            {
                return false;
            }
            else
            {
                m_ManaDictionary[ingredientType]--;
                return true;
            }
        }

        private bool MaxReached(ManaIngredients ingredientType)
        {
            if (m_ManaDictionary[ingredientType] == 0)
                return true;
            else
                return false;
        }
    }

    private Mana m_currentMana;
    [SerializeField] TextMesh HummusCount;
    [SerializeField] TextMesh FriesCount;
    [SerializeField] TextMesh SaladCount;
    [SerializeField] TextMesh FalafelCount;
    private Dictionary<ManaIngredients,TextMesh> m_signsText;
    private ScoreController m_scoreController;
    // Use this for initialization
    void Start () {
        m_signsText = new Dictionary<ManaIngredients, TextMesh>();
        m_signsText[ManaIngredients.Hummus] = HummusCount;
        m_signsText[ManaIngredients.Fries] = FriesCount;
        m_signsText[ManaIngredients.Salad] = SaladCount;
        m_signsText[ManaIngredients.Falafel] = FalafelCount;
        m_currentMana = new Mana();
        UpdateAllCounts();
        m_scoreController = GameObject.Find("ScoreController").GetComponent<ScoreController>();
    }

    // Update is called once per frame
    void Update () {
    }

    void CreateNewMana()
    {
        m_currentMana = new Mana();
        UpdateAllCounts();
    }

    private void UpdateAllCounts()
    {
        HummusCount.text = m_currentMana.GetCountByIngredient(ManaIngredients.Hummus).ToString();
        FriesCount.text = m_currentMana.GetCountByIngredient(ManaIngredients.Fries).ToString();
        SaladCount.text = m_currentMana.GetCountByIngredient(ManaIngredients.Salad).ToString();
        FalafelCount.text = m_currentMana.GetCountByIngredient(ManaIngredients.Falafel).ToString();
    }

    public bool AddIngredient(ManaIngredients addedIngredient)
    {
        return HandleIngredientAddition(addedIngredient);
    }

    private bool HandleIngredientAddition(ManaIngredients addedIngredient)
    {
        if (m_currentMana.AddIngredient(addedIngredient))
        {
            m_signsText[addedIngredient].text = m_currentMana.GetCountByIngredient(addedIngredient).ToString();
            m_scoreController.GoodIngredientAdded();

            //maybe do ssomething with the 3d object
            return true;
        }
        else
        {
            m_scoreController.BadIngredientAdded();
            return false;
        }
    }
   
    public bool ManaCompleted()
    {
        foreach (ManaIngredients ingredient in Enum.GetValues(typeof(ManaIngredients)))
        {
            if (m_currentMana.GetCountByIngredient(ingredient) != 0)
                return false;
        }
        CreateNewMana();
        m_scoreController.ManaWasGivenToCustomer();
        return true;
    }
}
