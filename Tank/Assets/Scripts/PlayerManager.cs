using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int life = 3;
    public int score = 0;
    public bool isDead = false;
    public GameObject born;
    public bool isDefeated = false;
    public Text playScoreText;
    public Text playLifeText;
    public GameObject isDefeatUi;

    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get => instance;
        set => instance = value;
    }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeated)
        {
            isDefeatUi.SetActive(true);
            Invoke("ReturnToMain", 3);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        playLifeText.text = life.ToString();
        playScoreText.text = score.ToString();
    }

    private void Recover()
    {
        if (life <= 0)
        {
            isDefeated = true;
        }
        else
        {
            life--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

    private void ReturnToMain()
    {
        SceneManager.LoadScene(0);
    }
}
