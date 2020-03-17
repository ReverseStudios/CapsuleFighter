using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
