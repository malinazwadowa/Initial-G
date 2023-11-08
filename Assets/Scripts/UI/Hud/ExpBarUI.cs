using UnityEngine;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    public Image fillImage;
    public void UpdateExpBar(float currentExp, float maxExp)
    {
        fillImage.fillAmount = currentExp / maxExp;
    }
}
