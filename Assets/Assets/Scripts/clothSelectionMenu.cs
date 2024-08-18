using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ClothSelectionMenu : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleItem;

    public HorizontalLayoutGroup HLG;

    public TMP_Text clothLabel;
    public string[] itemNames; 

    public float selectedOpacity = 1f;
    public float unselectedOpacity = 0.5f;
    public Vector3 selectedScale;
    public Vector3 originalScale;

    private bool isSnapped;
    private float snapSpeed;
    public float snapForce;
    public float smoothTime = 0.3f;  

    private Button[] menuButtons;
    private Vector3 velocity = Vector3.zero;  

    void Start()
    {
        isSnapped = false;

        selectedOpacity = 1f;
        unselectedOpacity = 0.5f;
        originalScale = new Vector3(1f, 1f, 1f);
        selectedScale = new Vector3(1.2f, 1.2f, 1.2f);

        menuButtons = contentPanel.GetComponentsInChildren<Button>();
        
        for (int i = 0; i < menuButtons.Length; i++)
        {
            int index = i; 
            menuButtons[i].onClick.AddListener(() => OnMenuButtonClick(index));
        }

        UpdateMenuSelection(-1);
    }

    void Update()
    {
        if (!isSnapped)
        {
            int currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / (sampleItem.rect.width + HLG.spacing)));
            currentItem = Mathf.Clamp(currentItem, 0, menuButtons.Length - 1);

            if (scrollRect.velocity.magnitude < 200)
            {
                scrollRect.velocity = Vector2.zero;
                snapSpeed += snapForce * Time.deltaTime;

                contentPanel.localPosition = Vector3.SmoothDamp(contentPanel.localPosition, 
                    new Vector3(0 - (currentItem * (sampleItem.rect.width + HLG.spacing)), contentPanel.localPosition.y, contentPanel.localPosition.z),
                    ref velocity, smoothTime);

                clothLabel.text = itemNames[currentItem];

                if (Vector3.Distance(contentPanel.localPosition, new Vector3(0 - (currentItem * (sampleItem.rect.width + HLG.spacing)), contentPanel.localPosition.y, contentPanel.localPosition.z)) < 0.01f)
                {
                    isSnapped = true;
                    UpdateMenuSelection(currentItem);
                }
            }
            else
            {
                isSnapped = false;
                snapSpeed = 0;
            }
        }
    }

    void OnMenuButtonClick(int index)
    {
        isSnapped = false; 
        snapSpeed = 0;      

        StopAllCoroutines();  
        StartCoroutine(SmoothScrollTo(index));
    }

    IEnumerator SmoothScrollTo(int index)
    {
        Vector3 targetPosition = new Vector3(
            0 - (index * (sampleItem.rect.width + HLG.spacing)),
            contentPanel.localPosition.y,
            contentPanel.localPosition.z);

        while (Vector3.Distance(contentPanel.localPosition, targetPosition) > 0.01f)
        {
            contentPanel.localPosition = Vector3.SmoothDamp(contentPanel.localPosition, targetPosition, ref velocity, smoothTime);
            yield return null;
        }

        contentPanel.localPosition = targetPosition;
        isSnapped = true;
        UpdateMenuSelection(index);
    }

    void UpdateMenuSelection(int selectedIndex)
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            Image buttonImage = menuButtons[i].GetComponent<Image>();
            Color color = buttonImage.color;

            if (i == selectedIndex)
            {
                color.a = selectedOpacity;
                menuButtons[i].transform.localScale = selectedScale;
            }
            else
            {
                color.a = unselectedOpacity;
                menuButtons[i].transform.localScale = originalScale;
            }

            buttonImage.color = color;
        }
    }
}
