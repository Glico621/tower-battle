using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
    public Text ScoreText;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        score = AnimalGenerator.getscore();
        ScoreText.text = string.Format("SCORE:{0}", score-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
