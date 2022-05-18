using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel = null;

    private GameObject currentPanel;
    private GameObject previousPanel;
    private GameObject nextPanel;

    private void Start()
    { 
        ChangePanel(mainMenuPanel);
    }

    public void ChangePanel(GameObject panel)
    {
        if (currentPanel != null) 
            currentPanel.SetActive(false);

        previousPanel = currentPanel;
        currentPanel = panel;
        panel.SetActive(true);
    }

    public void ChangePanel(GameObject panel, GameObject oldPanel)
    {
        if (oldPanel != null)
            oldPanel.SetActive(false);

        previousPanel = oldPanel;
        currentPanel = panel;
        panel.SetActive(true);
    }

    public void SetNextPanel(GameObject panel)
    {
        if (panel == null)
            return;

        nextPanel = panel;
    }

    public void Back()
    {
        ChangePanel(previousPanel);
    }

    public void Next()
    {
        ChangePanel(nextPanel);
    }
}
