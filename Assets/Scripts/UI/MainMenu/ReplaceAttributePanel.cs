using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReplaceAttributePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text attributeText;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ReplaceAttributeButton replaceAttributeButton;
    [SerializeField] private Transform defensiveAttributesParent;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private List<Attribute> defensiveAttributes = new List<Attribute>();
    private readonly List<ReplaceAttributeButton> _defensiveAttributeButtons = new List<ReplaceAttributeButton>();
    
    private Attribute _attributeToReplace;
    private SelectAttributesPanel _selectAttributesPanel;

    private void OnEnable()
    {
        foreach (var attribute in defensiveAttributes)
        {
            if (playerData.defensiveAttributes.Contains(attribute))
                continue;
            
            var button = Instantiate(replaceAttributeButton, defensiveAttributesParent);
            button.Init(this, attribute);
            _defensiveAttributeButtons.Add(button);
        }
    }
    
    private void OnDisable()
    {
        foreach (var button in _defensiveAttributeButtons)
        {
            Destroy(button.gameObject);
        }
        
        _defensiveAttributeButtons.Clear();
    }

    public void Init(Attribute attributeToReplace, SelectAttributesPanel selectAttributesPanel)
    {
        _attributeToReplace = attributeToReplace;
        _selectAttributesPanel = selectAttributesPanel;

        attributeText.text = attributeToReplace.ToString();
    }
    
    public void Replace(Attribute replacement)
    {
        _selectAttributesPanel.Replace(_attributeToReplace, replacement);
        
        if (!defensiveAttributes.Contains(_attributeToReplace))
            defensiveAttributes.Add(_attributeToReplace);
        
        defensiveAttributes.Remove(replacement);
        
        mainMenu.Back();
    }
}
