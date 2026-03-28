using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonMananger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string startGame = "Level1";
    public void StartGameButton()
    {
        if (!string.IsNullOrEmpty(startGame))
        {
            SceneManager.LoadScene(startGame);
        }
        else
        {
            Debug.LogError("Scene name is empty!");
        }
    }

}
