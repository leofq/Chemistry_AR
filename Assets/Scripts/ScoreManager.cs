using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Experimental.GraphView;
//using UnityEditor.ShaderGraph;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text scorePopup;
    public TMP_Text elementName;
    private int score = 0;
    public float timer = 2;
    private bool showingText = false;
    public bool showingElementName = false;

    public AudioSource audioSource;
    public AudioClip correctAudio;
    public AudioClip incorrectAudio;


    void Start()
    {
        // Show score and get audio manager
        scoreText.text = "Score: " + score.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        // keep the score updated based on score variable
        scoreText.text = "Score: " + score.ToString();

        // if showing text is true (well done! or incorrect) decrease the timer
        if(showingText)
        {
            decreaseTimer();
        }
        else
        {
            scorePopup.alpha = 0; // otherwise, don't want to see the text so turn alpha channel to 0
        }

        if(!showingElementName)
        {
            elementName.alpha = 0; // if not showing element name, don't want to see text so turn alpha channel to 0
        }
    }

    // Function for increasing score and updating UI
    public void addPoints(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();

        if (score < 0)
        {
            score = 0;
        }
    }

    // Function for showing 'well done!' to player when correctly placing element, also plays a sound
    public void positiveScore()
    {
        timer = 2;
        scorePopup.color = new Color32(122, 229, 133, 255); //green
        showingText = true;
        scorePopup.text = "Well Done";
        audioSource.PlayOneShot(correctAudio);
    }

    // Function for showing 'incorrect' to player when correctly placing element, also plays a sound
    public void negativeScore()
    {
        timer = 2;
        scorePopup.color = new Color32(162, 36, 37, 255); //red
        showingText = true;
        scorePopup.text = "Incorrect";
        audioSource.PlayOneShot(incorrectAudio);
    }

    // Function for decreasing the timer 
    public void decreaseTimer()
    {
        timer -= Time.deltaTime;
        if(timer <0) // changes the showing text to false when timer is at 0
        {
            showingText = false;
            timer = 2;
        }
    }

    // Function for showing the element name on the screen - pops up when user is grabbing element
    public void showElementName(string chemicalName)
    {
        elementName.text = (chemicalName);
        elementName.color = new Color32(255, 255, 255, 255);
    }
}
