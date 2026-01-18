using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScenes : MonoBehaviour
{
    [SerializeField] GameObject onetwo;
    [SerializeField] GameObject twothree;
    [SerializeField] GameObject threefour;
    [SerializeField] GameObject fourfive;
    [SerializeField] GameObject fivesix;

    public void OneTwo()
    {
        onetwo.SetActive(false);
        twothree.SetActive(true);
        threefour.SetActive(false);
        fourfive.SetActive(false);
        fivesix.SetActive(false);
    }

    public void TwoThree()
    {
        onetwo.SetActive(false);
        twothree.SetActive(false);
        threefour.SetActive(true);
        fourfive.SetActive(false);
        fivesix.SetActive(false);
    }
    public void ThreeFour()
    {
        onetwo.SetActive(false);
        twothree.SetActive(false);
        threefour.SetActive(false);
        fourfive.SetActive(true);
        fivesix.SetActive(false);
    }
    public void FourFive()
    {
        onetwo.SetActive(false);
        twothree.SetActive(false);
        threefour.SetActive(false);
        fourfive.SetActive(false);
        fivesix.SetActive(true);
    }
    public void FiveSix()
    {
        SceneManager.LoadScene("HomeMenuScene");
    }
}
