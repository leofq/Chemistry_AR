using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasketSettings : MonoBehaviour
{
    // Get a list of the elements already ysed
    List<string> usedElements = new List<string>();
    GameObject[] baskets;
    void Start()
    {
        
    }

    void Update()
    {

    }

    // Check if the element already exists in the scene
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
            return true;
        }
        else
        {
            return false;
        }
    }
}
