using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileRowUI : MonoBehaviour
{
    [SerializeField] private RawImage selectedHighlight;
    [SerializeField] private RawImage loadedHighlight;
    [SerializeField] public Toggle toggle;
    [SerializeField] private TextMeshProUGUI text;
    private ProfilesMenuControllerUI menuController;
    public string ProfileName { get; private set; }
    
    public void SetUp(string profileName, ProfilesMenuControllerUI menuController)
    {
        selectedHighlight.enabled = false;
        ProfileName = profileName;
        text.text = profileName;
        this.menuController = menuController;
    }

    public void SwitchSelectedHighlight()
    {
        if(toggle.isOn) { selectedHighlight.enabled = true; menuController.SetCurrentlySelectedRow(this); }
        else { selectedHighlight.enabled = false; }
    }

    public void SwitchLoadedHighlight()
    {
        loadedHighlight.enabled = true; 
    }
}
