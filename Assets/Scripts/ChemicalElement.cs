
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ChemicalElement : MonoBehaviour
{

    [SerializeField] public string ElementName;
    [SerializeField] public string ElementGroup;

    Dictionary<string, string> elementNameDictionary = new Dictionary<string, string>();
    private TMP_Text m_TextComponent;
    XRGrabInteractable m_ComponentGrabbed;
    [SerializeField] private ScoreManager m_ScoreManager;

    void Start()
    {
        // initalise chemical element
        m_TextComponent = GetComponentInChildren<TMP_Text>();
        m_ScoreManager = FindFirstObjectByType<ScoreManager>();

        m_TextComponent.text = ElementName;

        elementNameDictionary.Add("Li", "Lithium");
        elementNameDictionary.Add("Na", "Sodium");
        elementNameDictionary.Add("K", "Potassium");
        elementNameDictionary.Add("Rb", "Rubidium");


        elementNameDictionary.Add("Sc", "Scandium");
        elementNameDictionary.Add("Ti", "Titanium");
        elementNameDictionary.Add("Cr", "Chromium");
        elementNameDictionary.Add("Mn", "Manganese");


        elementNameDictionary.Add("B", "Boron");
        elementNameDictionary.Add("Si", "Silicon");
        elementNameDictionary.Add("Ge", "Germanium");
        elementNameDictionary.Add("As", "Arsenic");

    }

    void Update()
    {
        // if the chemical is being grabbed, update the UI to show the chemical's full name
         if (checkGrabbed())
        {
            string name = elementNameDictionary[ElementName.ToString()];
            Debug.Log(name);
            m_ScoreManager.showingElementName = true;
            m_ScoreManager.showElementName(name);
        }
        else
        {
            m_ScoreManager.showingElementName = false;
            m_ScoreManager.showingElementName = false;
        }
    }

    // Function for checking if the element is being grabbed
    bool checkGrabbed()
    {
        m_ComponentGrabbed = GetComponent<XRGrabInteractable>();
        bool grabbed = m_ComponentGrabbed.isSelected;
        return grabbed;
    }
}
