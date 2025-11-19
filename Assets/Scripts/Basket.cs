using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] public string GroupName;
    [SerializeField] GameObject ChemicalElementGO;

    private TMP_Text m_TextComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_TextComponent = GetComponentInChildren<TMP_Text>();
        m_TextComponent.text = GroupName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ChemicalElement")
        {
            Debug.Log("collision");
            if (other.GetComponent<ChemicalElement>().ElementGroup == GroupName)
            {
                Debug.Log("correct");
                FindAnyObjectByType<ScoreManager>().addPoints(100);
                Destroy(other.gameObject);
            }
        }
    }
}
