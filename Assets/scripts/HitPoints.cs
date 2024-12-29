using UnityEngine;

public class HitPoints : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] hitPoints;
    [SerializeField] private Sprite hitPointOn;
    [SerializeField] private Sprite hitPointOff;

    public void ShowHitPoints(int numHitPoints)
    {
        if (numHitPoints < 0)
        {
            //this is a bug
            numHitPoints = 0;
        }
        else if (numHitPoints > hitPoints.Length)
        {
            //this is also a bug
            numHitPoints = hitPoints.Length;
        }

        for(int i = 0; i < hitPoints.Length; ++i)
        {
            if (i < numHitPoints)
            {
                hitPoints[i].sprite = hitPointOn;
            }
            else
            {
                hitPoints[i].sprite = hitPointOff;
            }
        }
    }
}
