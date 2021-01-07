using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("分數介面")]
    public Text textScore;
    [Header("分數")]
    public int score;
    [Header("擊中分數")]
    public int scoreIn = 1;
    [Header("擊中音效")]
    public AudioClip soundIn;

    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "眼球")
        {
            AddScore();
        }

        if (other.transform.root.name == "Player")
        {

            scoreIn = 1;
        }
    }
    

    // 加分數
    private void AddScore()
    {
        score += scoreIn;   
        textScore.text = "Score : " + score;  //更新介面
        aud.PlayOneShot(soundIn, Random.Range(1f, 2f));   
    }
}
