using UnityEngine;
using UnityEngine.UI;

public class ProfilesMenuControllerUI : MonoBehaviour
{
    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private RectTransform listTransform;
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private MenuUI myMenu;

    private ProfileRowUI currentlySelectedRow;

    public GameObject newProfilePrompt;

    public RectTransform mainSection;

    public void SetUp()
    {
        mainSection.gameObject.SetActive(true);
        Utilities.RemoveChildren(listTransform);
        foreach (string profilename in GameManager.Instance.profileController.profileNameToID.Keys)
        {
            GameObject newRow = Instantiate(rowPrefab);
            newRow.GetComponent<RectTransform>().SetParent(listTransform, false);
            ProfileRowUI rowScript = newRow.GetComponent<ProfileRowUI>();
            rowScript.SetUp(profilename, this);
            rowScript.toggle.group = toggleGroup;
            

            if(profilename == GameManager.Instance.profileController.GetCurrentProfileName())
            {
                rowScript.toggle.isOn = true;
                rowScript.SwitchLoadedHighlight();
                currentlySelectedRow = rowScript;
            }
        }
    }

    public void SetCurrentlySelectedRow(ProfileRowUI rowScript)
    {
        currentlySelectedRow = rowScript;

        if(currentlySelectedRow.ProfileName == GameManager.Instance.profileController.GetCurrentProfileName())
        {
            loadButton.interactable = false;
            deleteButton.interactable = false;
        }
        else
        {
            loadButton.interactable = true;
            deleteButton.interactable = true;
        }
    }

    public void LoadSelected()
    {
        GameManager.Instance.profileController.SwitchProfile(currentlySelectedRow.ProfileName);
        myMenu.Close();
    }

    public void DeleteSelected()
    {
        GameManager.Instance.profileController.DeleteProfile(currentlySelectedRow.ProfileName);
        SetUp();
    }

    public void CreateNewProfile()
    {
        mainSection.gameObject.SetActive(false);
        newProfilePrompt.gameObject.GetComponent<NewNameInputPromptUI>().OpenForNew(myMenu);
    }

    public void RenameCurrentlySelected()
    {
        mainSection.gameObject.SetActive(false);
        newProfilePrompt.gameObject.GetComponent<NewNameInputPromptUI>().OpenForRename(myMenu, currentlySelectedRow.ProfileName);
    }
}