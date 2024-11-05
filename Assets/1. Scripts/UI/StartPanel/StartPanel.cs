using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPanel : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject Control1;
    public GameObject Control2;

    public void ClickGameStart()
    {
        startPanel.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickControlsDescriptionButton()
    {
        if (Control1.activeInHierarchy == false)
            Control1.SetActive(true);
    }

    public void ClickRightArrowButton()
    {
        if (Control2.activeInHierarchy == false)
        {
            Control1.SetActive(false);
            Control2.SetActive(true);
        }
    }

    public void ClickLeftArrowButton()
    {
        if (Control1.activeInHierarchy == false)
        {
            Control2.SetActive(false);
            Control1.SetActive(true);
        }
    }

    public void ClickExitButton()
    {
        Control1.SetActive(false);
        Control2.SetActive(false);
    }
}
