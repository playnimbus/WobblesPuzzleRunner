using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

    public GameObject Wobble;

    public GameObject ScoreGO;
    public GameObject StarsGO;
    public GameObject SpeedMultGO;

    Text ScoreTxt;
    Text StarTxt;
    Text SpeedMultTxt;

    float score=0;
    int stars=0;
    public int speedMult=1;

    float oldPosX = 0;

	// Use this for initialization
	void Start () {
        ScoreTxt = ScoreGO.GetComponent<Text>();
        StarTxt = StarsGO.GetComponent<Text>();
        SpeedMultTxt = SpeedMultGO.GetComponent<Text>();

        ScoreTxt.text = score.ToString();
        StarTxt.text = stars.ToString();
        SpeedMultTxt.text = speedMult.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        updateScore();
	}

    void updateScore()
    {
        score += ((Wobble.transform.position.x - oldPosX) * speedMult);
        oldPosX = Wobble.transform.position.x;

        updateScoreTxt();
    }
    void addScore(float _score)
    {
        score += (_score * speedMult);
        updateScoreTxt();
    }

    public void addStar()
    {
        stars++;
        addScore(100);
        updateStarTxt();
    }
    public void addSpeedMult(int _speedMult)
    {
        speedMult += _speedMult;
        updateSpeedMultTxt();
    }

    void updateScoreTxt() { ScoreTxt.text = ((int)score).ToString(); }
    void updateStarTxt() { StarTxt.text = stars.ToString(); }
    void updateSpeedMultTxt() { SpeedMultTxt.text = speedMult.ToString(); }
}
