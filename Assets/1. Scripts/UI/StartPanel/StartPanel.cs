using UnityEngine;

public class StartPanel : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameDescription;

    public void ClickGameStart()
    {
        startPanel.SetActive(false);
    }

    public void ClickGameDescription()
    {
        gameDescription.SetActive(true);
    }

}
