using System.Linq;
using UnityEngine;

public class UI : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var basket = FindObjectsByType<Basket>(FindObjectsSortMode.None);
        if(basket.Count() > 2)
        {
            this.gameObject.SetActive(false);
        }

    }
}
