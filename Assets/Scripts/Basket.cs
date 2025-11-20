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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("in start func");
        m_TextComponent = GetComponentInChildren<TMP_Text>();
        m_TextComponent.text = GroupName;
        GetComponent<Transform>().transform.Rotate(-90, 0, 0);

        if(!BasketSettings.GetComponent<BasketSettings>().checkExists("Alkali"))
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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ChemicalElement")
        {
            if (other.GetComponent<ChemicalElement>().ElementGroup == GroupName)
            {
                FindAnyObjectByType<ScoreManager>().addPoints(100);
                Destroy(other.gameObject);
            }
            else
            {
                FindAnyObjectByType<ScoreManager>().addPoints(-30);
            }
        }
      
    }
}
