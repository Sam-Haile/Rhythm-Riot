using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    private int score;
    private int noteTracker;
    private int sprayCanTracker;
    public GameObject[] sprayCans;

    public int notesCollected
    {
        get { return noteTracker; }
    }
    public int sprayCansCollected
    {
        get { return sprayCanTracker; }
    }


    // Start is called before the first frame update
    void Start()
    {
        noteTracker = 0;
        sprayCanTracker = 0;
        score = int.Parse(scoreText.text);
        foreach (GameObject can in sprayCans)
        {
            can.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Note"))
        {
            noteTracker++;
            IncreaseScore(100);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("can1"))
        {
            sprayCanTracker++;
            CollectSprayCan(0, other);
        }
        else if (other.CompareTag("can2"))
        {
            sprayCanTracker++;
            CollectSprayCan(1, other);
        }
        else if (other.CompareTag("can3"))
        {
            sprayCanTracker++;
            CollectSprayCan(2, other);
        }
        else if (other.CompareTag("can4"))
        {
            sprayCanTracker++;
            CollectSprayCan(3, other);
        }
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

    public string DetermineGrade()
    {
        if (score >= 7600)
        {
            return "S";
        }
        else if (score < 7600 && score >= 6080)
        {
            return "A";
        }
        else if (score < 6080 && score >= 4560)
        {
            return "B";
        }
        else if (score < 4560 && score >= 3040)
        {
            return "C";
        }  
        else if (score < 3040 && score >= 1520)
        {
            return "D";
        }
        else
        {
            return "F";
        }

    }

}
