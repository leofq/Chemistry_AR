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
        // create a bool for checking if chemical elements have finished spawning
        bool finished = spawner.GetComponent<CESpawner>().finishedSpawning;
        if (!finished)
        {
            // if it hasn't, keep UI on the screen as instructions to user
            instructions.text = "spawning elements, move camera slowly for more space.";
        }
        else
        {
            // otherwise change UI to inform user to place baskets
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
