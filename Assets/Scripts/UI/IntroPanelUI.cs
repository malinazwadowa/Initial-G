using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroPanelUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Start()
    {
        timerText.text = LevelManager.Instance.LevelDuration.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
