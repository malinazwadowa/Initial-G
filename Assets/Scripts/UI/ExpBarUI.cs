using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    public Slider slider;
    public void UpdateExpBar(float currentExp, float maxExp)
    {
        slider.value = currentExp / maxExp;
    }
}
