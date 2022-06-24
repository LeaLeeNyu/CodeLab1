using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Notebook : MonoBehaviour
{
    public Component notebook;
    private bool notebookState;

    private void Start()
    {
        notebook = GetComponentInChildren<Image>();
    }

    void Update()
    {
        notebookState = notebook.gameObject.activeSelf;

        if(notebookState & Input.GetKeyDown(KeyCode.Escape))
        {
            notebook.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
