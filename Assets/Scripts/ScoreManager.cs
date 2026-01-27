using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text scorePopup;
    public TMP_Text elementName;
    private int score = 0;
    public float timer = 2;
    private bool showingText = false;
    public bool showingElementName = false;


    // For showing and updating the player score


    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    
    void Update()
    {
        scoreText.text = "Score: " + score.ToString();

        if(showingText)
        {
            decreaseTimer();
        }
        else
        {
            scorePopup.alpha = 0;
        }

        if(!showingElementName)
        {
            elementName.alpha = 0;
        }
    }

    public void addPoints(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }

    public void positiveScore()
    {
        timer = 2;
        scorePopup.color = new Color32(122, 229, 133, 255);
        showingText = true;
        scorePopup.text = "Well Done";
    }

    public void negativeScore()
    {
        timer = 2;
        scorePopup.color = new Color32(162, 36, 37, 255);
        showingText = true;
        scorePopup.text = "Incorrect";
    }

    public void decreaseTimer()
    {
        timer -= Time.deltaTime;
        if(timer <0)
        {
            showingText = false;
            timer = 2;
        }
    }

    public void showElementName(string chemicalName)
    {
        elementName.text = (chemicalName);
        elementName.color = new Color32(255, 255, 255, 255);
    }
}
