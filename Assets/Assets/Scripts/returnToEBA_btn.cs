using UnityEngine;

public class returnToEBA_btn : MonoBehaviour
{
    public string returnURL = "https://github.com/CWF105/";

    public void ReturnToLink()
    {
        Application.OpenURL(returnURL);

        // Optional step: Perform any cleanup or save operations
        SaveGameData();
        LogExitEvent();

        // Quit the application
        Application.Quit();
    }


    private void SaveGameData()
    {
        Debug.Log("Game data saved.");
        // NOTE: add more logic here to save and prevent the loss of data
    }


    private void LogExitEvent()
    {
        Debug.Log("Exit event logged.");
    }
}
