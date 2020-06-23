using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialMenu : MonoBehaviour
{
    public Transform tutorialsParent;
    [SerializeField] int nowPage = 0;
    [Space]
    public GameObject nextBtn;
    public GameObject prevBtn;
    public GameObject playBtn;

    public void OnScrollBtn(int n)
    {
        if (nowPage + n >= 0 && nowPage + n < tutorialsParent.childCount)
            nowPage += n;

        //Nextbtn checker
        if (nowPage == tutorialsParent.childCount - 1)
        {
            nextBtn.SetActive(false);
            playBtn.SetActive(true);
        }
        else
        {
            nextBtn.SetActive(true);
            playBtn.SetActive(false);
        }

        //Prevbtn checker
        if (nowPage == 0)
            prevBtn.SetActive(false);
        else
            prevBtn.SetActive(true);
        

        LoadPage(nowPage);
    }

    public void OnPlayBtn()
    {
        SceneManager.LoadScene(1);
    }

    void LoadPage(int page)
    {
        for (int i = 0; i < tutorialsParent.childCount; i++)
        {
            if (i != nowPage)
                tutorialsParent.GetChild(i).gameObject.SetActive(false);
            else
                tutorialsParent.GetChild(i).gameObject.SetActive(true);
        }
    }
}
