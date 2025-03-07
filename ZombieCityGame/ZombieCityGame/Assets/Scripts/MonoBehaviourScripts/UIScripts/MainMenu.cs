using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button character1Button;
    public Button character2Button;
    public Button character3Button;

    public string sceneName;

    private void Start()
    {
        PlayerPrefs.DeleteKey("SelectedCharacter");
        // Назначаем методы на кнопки
        character1Button.onClick.AddListener(() => SelectCharacter(1));
        character2Button.onClick.AddListener(() => SelectCharacter(2));
        character3Button.onClick.AddListener(() => SelectCharacter(3));
    }

    private void SelectCharacter(int characterIndex)
    {
        // Сохраняем выбор персонажа в PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene(sceneName); 
    }
}