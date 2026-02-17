using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Basket : MonoBehaviour
{
    [SerializeField] public string GroupName;
    [SerializeField] GameObject ChemicalElementGO;
    [SerializeField] GameObject BasketSettings;
    private TMP_Text m_TextComponent;
    private MeshFilter m_Mesh; 
    void Start()
    {
        // Get the text mesh component for the group name and perform transform on model
        m_TextComponent = GetComponentInChildren<TMP_Text>();
        m_TextComponent.text = GroupName;
        //Vector3 pos = transform.position;
        //transform.position = pos;
        //GetComponent<Transform>().transform.Rotate(-90, 0, 0);
      
        // For prototype only 3 basket types, so check if each exists then add appropriate group name to each of the baskets
        if (!BasketSettings.GetComponent<BasketSettings>().checkExists("Alkali"))
        {
            GroupName = "Alkali";
            m_TextComponent.text = GroupName;
            m_Mesh.GetComponent<Renderer>().material.color = new Color(1,0,0,1);
        }
        else if (!BasketSettings.GetComponent<BasketSettings>().checkExists("Transition"))
        {
            GroupName = "Transition";
            m_TextComponent.text = GroupName;
        }
        else if (!BasketSettings.GetComponent<BasketSettings>().checkExists("Metalloids"))
        {
            GroupName = "Metalloids";
            m_TextComponent.text = GroupName;
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Collision with chemical element
        if(other.gameObject.tag == "ChemicalElement")
        {
            // Check if the group name for that element is the same as the basket group name, if it is increase score otherwise decrease score
            if (other.GetComponent<ChemicalElement>().ElementGroup == GroupName)
            {
                FindAnyObjectByType<ScoreManager>().positiveScore();
                FindAnyObjectByType<ScoreManager>().addPoints(100);
                Destroy(other.gameObject);
            }
            else
            {
                FindAnyObjectByType<ScoreManager>().addPoints(-30);
                FindAnyObjectByType<ScoreManager>().negativeScore();
            }
        }
      
    }
}
