using Palmmedia.ReportGenerator.Core;
using TMPro;
using UnityEngine;

public class ChemicalElement : MonoBehaviour
{

    [SerializeField] public string ElementName;
    [SerializeField] public string ElementGroup;

    private TMP_Text m_TextComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_TextComponent = GetComponentInChildren<TMP_Text>();
        m_TextComponent.text = ElementName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
