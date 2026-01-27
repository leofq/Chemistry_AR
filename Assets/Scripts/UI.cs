using System.Linq;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject scoreUI;

    void Start()
    {
        scoreUI.SetActive(false);
    }

    void Update()
    {
        // Show the UI text on screen if there are less than three baskets in the game
        var basket = FindObjectsByType<Basket>(FindObjectsSortMode.None);
        if(basket.Count() > 2)
        {
            this.gameObject.SetActive(false);
            scoreUI.SetActive(true);
        }

    }
}
