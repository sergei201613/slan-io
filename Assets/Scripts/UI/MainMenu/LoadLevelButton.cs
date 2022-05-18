using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadLevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string gameSceneName = "Game";
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
