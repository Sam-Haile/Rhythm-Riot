using Cinemachine;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    public CinemachineVirtualCamera camera;
    public MMFeedbacks scoreParent;

    public TextMeshProUGUI letterGrade;
    public TextMeshProUGUI sprayCansCollectedText;
    public TextMeshProUGUI notesCollectedText;

    public ScoreManager scoreManager;
    
    private int score = 0;
    

    private void OnTriggerEnter(Collider other)
    {
            if (other.CompareTag("Player"))
            {
                if (this.tag == "gameEnd")
                {
                    camera.Follow = null;
                    camera.LookAt = null;
                }
                else if (this.tag == "scoreScreen")
                {
                    DetermineGrade();
                    ChangeScores();
                    scoreParent?.PlayFeedbacks();
                }
            }
    }

    private int DetermineScore()
    {
        score += scoreManager.notesCollected;
        score += scoreManager.sprayCansCollected;
        return score;
    }


    private void DetermineGrade()
    {
        if (scoreManager.notesCollected >= 1)
        {
            letterGrade.text = "B";
        }
        
    }


    private void ChangeScores() 
    {
        notesCollectedText.text = scoreManager.notesCollected.ToString() + "/100";
        sprayCansCollectedText.text = scoreManager.sprayCansCollected.ToString() + "/4";

    }

}
