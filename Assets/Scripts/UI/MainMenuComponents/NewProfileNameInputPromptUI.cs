using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewProfileNameInputPromptUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI errorText;

    public Button cancelButton;
    public Button acceptButton;

    private MenuUI profilesMenu;
    public MenuUI myMenu;

    public void Open(MenuUI menu)
    {
        gameObject.SetActive(true);
        errorText.gameObject.SetActive(false);
        inputField.text = "";
        this.profilesMenu = menu;
    }

    public void OpenForNew(MenuUI menu)
    {
        Open(menu);
        acceptButton.onClick.RemoveAllListeners();

        acceptButton.onClick.AddListener(CreateNew);
        myMenu.OnAcceptEvent.AddListener(CreateNew);
    }

    public void OpenForRename(MenuUI menu, string nameToChange)
    {
        Open(menu);
        acceptButton.onClick.RemoveAllListeners();

        acceptButton.onClick.AddListener(() => Rename(nameToChange));
        myMenu.OnAcceptEvent.AddListener(() => Rename(nameToChange));
    }

    public void Rename(string nameToChange)
    {
        string userInput = inputField.text;
        (bool isValid, string message) = CheckValidity(userInput);

        if (isValid)
        {
            GameManager.Instance.profileController.RenameProfile(nameToChange, userInput);
            Close();
        }
        else
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true);
        }
    }

    public void CreateNew()
    {
        string userInput = inputField.text;
        (bool isValid, string message) = CheckValidity(userInput);

        if (isValid)
        {
            GameManager.Instance.profileController.AddProfile(userInput);
            GameManager.Instance.profileController.SwitchProfile(userInput);
            Close();
        }
        else
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true );
        }
    }

    public void Close()
    {
        gameObject.SetActive (false);
        profilesMenu.Open();
    }

    private (bool isValid, string message) CheckValidity(string userInput)
    {
        if (string.IsNullOrEmpty(userInput))
        {
            return (false, "Please enter a profile name.");
        }
        else if (GameManager.Instance.profileController.profileNameToID.ContainsKey(userInput))
        {
            return (false, "A profile with this name already exists.");
        }
        else
        {
            return (true, "");
        }
    }
}
