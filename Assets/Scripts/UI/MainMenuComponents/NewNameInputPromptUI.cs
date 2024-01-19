using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewNameInputPromptUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI errorText;

    public Button cancelButton;
    public Button acceptButton;
    private MenuUI ourMenu;

    public void Open(MenuUI ourMenu)
    {
        gameObject.SetActive(true);
        errorText.gameObject.SetActive(false);
        inputField.text = "";
        this.ourMenu = ourMenu;
    }

    public void OpenForNew(MenuUI ourMenu)
    {
        Open(ourMenu);
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(OnAcceptNew);
    }

    public void OpenForRename(MenuUI ourMenu, string nameToChange)
    {
        Open(ourMenu);
        acceptButton.onClick.AddListener(() => OnAcceptRename(nameToChange));
    }

    public void OnAcceptRename(string nameToChange)
    {
        Debug.Log("on accept rename ? ");
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

    public void OnAcceptNew()
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

    private void Close()
    {
        gameObject.SetActive (false);
        ourMenu.Open();
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
