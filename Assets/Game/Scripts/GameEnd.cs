using Cinemachine;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    new public CinemachineVirtualCamera camera;
    public MMFeedbacks scoreParent;

    public TextMeshProUGUI letterGrade;
    public TextMeshProUGUI sprayCansCollectedText;
    public TextMeshProUGUI notesCollectedText;

    public ScoreManager scoreManager;
    

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

                ChangeScores();
                scoreParent?.PlayFeedbacks();
            }
        }
    }

    private void ChangeScores() 
    {
        notesCollectedText.text = scoreManager.notesCollected.ToString() + "/61";
        sprayCansCollectedText.text = scoreManager.sprayCansCollected.ToString() + "/3";

        letterGrade.text = scoreManager.DetermineGrade();
    }


}
