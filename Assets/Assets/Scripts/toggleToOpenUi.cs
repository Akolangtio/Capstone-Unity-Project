using UnityEngine;
using System.Collections; 
using System.Collections.Generic;

public class toggleToOpenUi : MonoBehaviour
{
    public GameObject containerUI;

    public void ShowUI (){
        if(containerUI != null) {
            Animator animate = containerUI.GetComponent<Animator>();

            if(animate != null) {
                bool isOpen = animate.GetBool("show");
                animate.SetBool("show", !isOpen);
            }
        }
    }
}
