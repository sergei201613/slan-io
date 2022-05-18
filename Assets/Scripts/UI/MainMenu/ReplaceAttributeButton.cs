using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReplaceAttributeButton : MonoBehaviour, IPointerClickHandler
{
    public Attribute Attribute => _attribute;
    
    [SerializeField] private TMP_Text title;
    
    private Attribute _attribute;
    private ReplaceAttributePanel _replaceAttributePanel;

    private void Start()
    {
        _replaceAttributePanel = GetComponentInParent<ReplaceAttributePanel>();
    }
    
    public void Init(ReplaceAttributePanel replaceAttributePanel, Attribute attribute)
    {
        _replaceAttributePanel = replaceAttributePanel;
        _attribute = attribute;

        title.text = _attribute.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _replaceAttributePanel.Replace(_attribute);
    }
}