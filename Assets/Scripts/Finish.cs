using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private GameObject messageUI;
    private bool isActivated = false;

    public void Activate() {
        isActivated = true;
        messageUI.SetActive(false);
    }
    
    public void FinishLevel() {
        if(isActivated) {
            Time.timeScale = 0f;
            levelCompleteCanvas.SetActive(true);
            //gameObject.SetActive(false);
        }
        else {
            messageUI.SetActive(true);
        }
       
   }
}
