using UnityEngine;

public class navigateToShop_btn : MonoBehaviour
{
    public string shopURL = "https://shopee.com";

    public void OpenShop()
    {
        Application.OpenURL(shopURL);
        // uncomment the below code to quit the app
        // Application.Quit();
    }
}
