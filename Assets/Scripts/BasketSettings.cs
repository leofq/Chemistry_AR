using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasketSettings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    List<string> usedElements = new List<string>();
    GameObject[] baskets;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool checkExists(string name)
    {
        usedElements.Clear();

        var basket = FindObjectsByType<Basket>(FindObjectsSortMode.None);

        foreach (var bas in basket)
        {
            usedElements.Add(bas.GroupName);
        }

        if (usedElements.Contains(name))
        {
            Debug.Log("Already exists");
            return true;
        }
        else
        {
            Debug.Log("Does not exist");
            return false;
        }
    }
}
