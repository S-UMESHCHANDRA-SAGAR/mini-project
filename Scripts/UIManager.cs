using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField] Text highScoreText;
    
    int highScore;
    
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    // Start is called before the first frame update
    //void Awake() 
    //{
        //highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("highScore",0).ToString();
    //}
    void Start()
    {
        _scoreText.text="Score: "+0;//when we start the game this text is displayed 
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("highScore",0).ToString();//PlayerPrefs.GetInt gets the tha value of the highScore that scored in the unity storage data
    }

    

    public void UpdateScore(int playerScore)//this method is called from the player script to display score
    {
        _scoreText.text="Score: "+playerScore;//displays the score 
        if (playerScore > PlayerPrefs.GetInt("highScore"))//if the player score is greater than the stored score then it displays the new high score
        {
            PlayerPrefs.SetInt("highScore", playerScore);//SetInt is used to set the value of the highScore to score in unity database
            highScoreText.text = "HighScore: " + playerScore;//displaying highscore
            //or highScoreText.text = playerScore.ToString();
        }
        
         
    }
    //void OnGUI() 
    //{
        //GUI.color = Color.red;
        //GUILayout.Label("highScore: " + highScore.ToString());
    //}
    

    public void UpdateLives(int currentLives)//this method is called from the player scriptto display lives of the player
    {
        _LivesImg.sprite = _liveSprites[currentLives];
    }

    public void LoadGame()//this method is called when we hit the restart button
    {
        SceneManager.LoadScene(1);//loads scene(1)= game scene
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);//loads mainmenu scene
    }

    public void ResetHighScore()//this method is called when we hit on resethighscore button
    {
        PlayerPrefs.DeleteKey("highScore");//deletes the values that stored in the highScore variable by unity storage.
        //or PlayerPrefs.DeleteAll();
        highScoreText.text = "HighScore: " + 0;//and displays 0 in highScore
        //or highScoreText.text = "0";
    }
}