using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;
    public static int coins;

    public GameObject CoinText;
    public GameObject winText;
    public static bool haveWin;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        coins = 0;
        haveWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        //startingText.GetComponent<TextMeshProUGUI>().text = "";
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        CoinText.GetComponent<TextMeshProUGUI>().text = "Coins: " + coins;

        if (SwipeManager.tap) {
            isGameStarted = true;
            Destroy(startingText);
        }

        if (coins >= 20)
        {
            Time.timeScale = 0;
            winText.SetActive(true);
        }
    }
}
