using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button character1Button;
    [SerializeField] private Button character2Button;
    [SerializeField] private Button character3Button;

    [SerializeField] private string sceneName;

    private void Start()
    {
        PlayerPrefs.DeleteKey("SelectedCharacter");
        
        character1Button.onClick.AddListener(() => SelectCharacter(1));
        character2Button.onClick.AddListener(() => SelectCharacter(2));
        character3Button.onClick.AddListener(() => SelectCharacter(3));
    }

    private void SelectCharacter(int characterIndex)
    {
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName); 
    }
}