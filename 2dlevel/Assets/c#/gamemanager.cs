using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class gamemanager : MonoBehaviour
{
    public  int scorecount = 0;
    public TextMeshProUGUI scoretext;
    public TextMeshProUGUI uiscoretext;
    public TextMeshProUGUI uihighscoretext;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uiscoretext.text = scorecount.ToString();
        uihighscoretext.text = PlayerPrefs.GetInt("highscore").ToString();
        if (scorecount > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", GameObject.Find("gamemanager").GetComponent<gamemanager>().scorecount);
        }
        scoretext.text = scorecount.ToString();

    }
   public void restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
