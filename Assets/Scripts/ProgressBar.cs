using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Milestone
{
    public int score;
    public Image icon;
}
public class ProgressBar : MonoBehaviour
{
    public Image progressBarBackground;
    public Image progressBarIcon;
    private GameManager gameManager;
    public float progress = 0.0f;
    public float iconOffset = 0.0f;

    public Milestone[] milestones;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
        UpdateMilestones();
    }

    void UpdateProgressBar()
    {
        float width = progressBarBackground.rectTransform.rect.width;
        float progress = Mathf.Clamp01((float)gameManager.score / 3500); // Assuming max score is 3500

        float targetPosition = progress * width;

        progressBarIcon.rectTransform.localPosition = new Vector3(targetPosition - width / 2 + iconOffset, 0f, 0f);
    }
    void UpdateMilestones()
    {
        foreach (Milestone milestone in milestones)
        {
            float milestoneProgress = Mathf.Clamp01((float)milestone.score / 3500);
            float milestonePosition = milestoneProgress * progressBarBackground.rectTransform.rect.width;

            milestone.icon.rectTransform.localPosition = new Vector3(milestonePosition - progressBarBackground.rectTransform.rect.width / 2, 0f, 0f);
        }
    }
}
