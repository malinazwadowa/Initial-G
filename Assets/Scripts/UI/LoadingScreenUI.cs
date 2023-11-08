using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenUI : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    

    void Start()
    {
        loadingBar.fillAmount = 0;
    }

    void Update()
    {
        //loadingBar.fillAmount = SceneLoadingManager.LoadProgress;
    }
}
