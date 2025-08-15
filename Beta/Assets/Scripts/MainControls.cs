using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControls : MonoBehaviour
{
    public GameObject mainButtonsPanel;
    MainButtons mainButtons;

    // Start is called before the first frame update
    void Start()
    {
        mainButtons = mainButtonsPanel.GetComponent<MainButtons>();
    }

    public void Back()
    {
        Hide();
        mainButtons.Show();
    }

    public void Hide()
    {
        // Deactivate the panel
        gameObject.SetActive(false);
    }

    public void Show()
    {
        // Activate the panel
        gameObject.SetActive(true);
    }
}
