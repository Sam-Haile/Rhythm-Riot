using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score;


    public GameObject[] sprayCans;

    // Start is called before the first frame update
    void Start()
    {
        score = int.Parse(scoreText.text);
        foreach (GameObject can in sprayCans)
        {
            can.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            IncreaseScore(100);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("can1"))
            CollectSprayCan(0, other);
        else if (other.CompareTag("can2"))
            CollectSprayCan(1, other);
        else if (other.CompareTag("can3"))
            CollectSprayCan(2, other);
        else if (other.CompareTag("can4"))
            CollectSprayCan(3, other);
    }

    private void IncreaseScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = score.ToString("D6");
    }

    private void CollectSprayCan(int index, Collider other)
    {
        IncreaseScore(500);
        Destroy(other.gameObject);
        sprayCans[index].SetActive(true);
    }
}
