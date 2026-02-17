using System.Linq;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject scoreUI;
    [SerializeField] CESpawner spawner;

    public TMP_Text instructions;


    void Start()
    {
        
        
        scoreUI.SetActive(false);
    }

    void Update()
    {
        bool finished = spawner.GetComponent<CESpawner>().finishedSpawning;
        if (!finished)
        {
            instructions.text = "spawning elements, move camera slowly for more space.";
        }
        else
        {
            instructions.text = "Tap screen to place collecting baskets.";
        }
        // Show the UI text on screen if there are less than three baskets in the game
        var basket = FindObjectsByType<Basket>(FindObjectsSortMode.None);
        if(basket.Count() > 2)
        {
            this.gameObject.SetActive(false);
            scoreUI.SetActive(true);
        }

    }
}
