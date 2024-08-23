using UnityEngine;

public class returnToEBA_btn : MonoBehaviour
{
    public string returnURL = "https://github.com/CWF105/";

    public void ReturnToLink()
    {
        Application.OpenURL(returnURL);

        // Quit the application
        Application.Quit();
    }
}
