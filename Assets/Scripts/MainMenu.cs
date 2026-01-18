using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject HowToPlay;
    [SerializeField] GameObject HowToPause;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Continue()
    {
        HowToPause.SetActive(false);
        HowToPlay.SetActive(true);

    }
    public void CloseHWT()
    {
        HowToPause.SetActive(true);
        HowToPlay.SetActive(false);

    }
    public void MainExit()
    {

    }
}
