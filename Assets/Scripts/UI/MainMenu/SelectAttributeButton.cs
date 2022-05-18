using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectAttributeButton : MonoBehaviour
{
    public Attribute Attribute => _attribute;

    [SerializeField] private TMP_Text title;

    private Attribute _attribute;
    private SelectAttributesPanel _selectAttributesPanel;

    public void Init(SelectAttributesPanel selectAttributesPanel, Attribute attribute)
    {
        _selectAttributesPanel = selectAttributesPanel;
        _attribute = attribute;

        title.text = _attribute.ToString();
    }

    public void OnClick()
    {
        _selectAttributesPanel.OpenReplaceAttributePanel(_attribute);
    }
}
