using UnityEngine;
using UnityEngine.EventSystems;
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

    public ScrollRect scrollRect;

    public void PopulateProfilesList()
    {
        Utilities.RemoveChildren(listTransform);
        bool toggleSelected = false;

        foreach (string profileName in GameManager.Instance.profileController.profileNameToID.Keys)
        {
            GameObject newRow = Instantiate(rowPrefab);
            newRow.GetComponent<RectTransform>().SetParent(listTransform, false);
            ProfileRowUI rowScript = newRow.GetComponent<ProfileRowUI>();
            rowScript.SetUp(profileName, this);
            rowScript.toggle.group = toggleGroup;

            if (profileName == GameManager.Instance.profileController.GetCurrentProfileName())
            {
                rowScript.toggle.isOn = true;
                rowScript.SwitchLoadedHighlight();
                currentlySelectedRow = rowScript;
                newRow.transform.SetAsFirstSibling();
                if (!toggleSelected) { rowScript.toggle.Select(); toggleSelected = true; }  
            }
        }
    }

    public void SetCurrentlySelectedRow(ProfileRowUI rowScript)
    {
        currentlySelectedRow = rowScript;

        if (currentlySelectedRow.ProfileName == GameManager.Instance.profileController.GetCurrentProfileName())
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
    }

    public void DeleteSelected()
    {
        GameManager.Instance.profileController.DeleteProfile(currentlySelectedRow.ProfileName);
        //PopulateProfilesList();
    }

    public void CreateNewProfile()
    {
        newProfilePrompt.gameObject.GetComponent<NewProfileNameInputPromptUI>().OpenForNew(myMenu);
    }

    public void RenameCurrentlySelected()
    {
        newProfilePrompt.gameObject.GetComponent<NewProfileNameInputPromptUI>().OpenForRename(myMenu, currentlySelectedRow.ProfileName);
    }
}
