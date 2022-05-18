using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectAttributesPanel : MonoBehaviour
{
    [SerializeField] private SelectAttributeButton selectAttributeButtonPrefab;
    [SerializeField] private Transform defensiveAttributesParent;
    [SerializeField] private ReplaceAttributePanel replaceAttributePanel;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private PlayerData playerData;
    private readonly List<SelectAttributeButton> _defensiveAttributeButtons = new List<SelectAttributeButton>();

    private void OnEnable()
    {
        foreach (var attribute in playerData.defensiveAttributes)
        {
            var button = Instantiate(selectAttributeButtonPrefab, defensiveAttributesParent);
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

    public void OpenReplaceAttributePanel(Attribute attribute)
    {
        mainMenu.ChangePanel(replaceAttributePanel.gameObject);
        replaceAttributePanel.Init(attribute, this);
    }

    public void Replace(Attribute replaceable, Attribute replacement)
    {
        var defensiveAttributes = playerData.defensiveAttributes;
        
        int oldItem = defensiveAttributes.IndexOf(replaceable);
        defensiveAttributes.Insert(oldItem, replacement);
        defensiveAttributes.Remove(replaceable);
    }
}
