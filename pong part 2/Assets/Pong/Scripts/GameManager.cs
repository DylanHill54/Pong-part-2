using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PowerUP;
    public Transform PowerDown;
    public Transform ball;
    public float startSpeed = 3f;
    public GoalTrigger leftGoalTrigger;
    public GoalTrigger rightGoalTrigger;

    private int leftPlayerScore = 0;
    private int rightPlayerScore = 0;
    private Vector3 ballStartPos;
  
    public TextMeshProUGUI RightScore;
    public TextMeshProUGUI LeftScore;

    private const int scoreToWin = 11;

    // Start is called before the first frame update
    void Start()
    {
        ballStartPos = ball.position;
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.velocity = new Vector3(1f, 0f, 0f) * startSpeed;
    }

    // If the ball entered the goal area, increment the score, check for win, and reset the ball
    public void OnGoalTrigger(GoalTrigger trigger)
    {
        if (trigger == leftGoalTrigger)
        {
            rightPlayerScore++;
            RightScore.text = rightPlayerScore.ToString();
            if (rightPlayerScore > 9)
            {
                RightScore.color=Color.red;
            }
            Debug.Log($"Right player scored: {rightPlayerScore}");
            if (rightPlayerScore == scoreToWin)
            {
                Debug.Log("Right player wins!");
                leftPlayerScore = 0;
                LeftScore.text = leftPlayerScore.ToString();
                LeftScore.color=Color.white;
                rightPlayerScore = 0;
                RightScore.text = rightPlayerScore.ToString();
                RightScore.color=Color.white;
            }
            else
            {
                ResetBall(-1f);
            }
        }
        else if (trigger == rightGoalTrigger)
        {

            leftPlayerScore++;
            LeftScore.text = leftPlayerScore.ToString();
            if (leftPlayerScore > 9)
            {
                LeftScore.color=Color.red;
            }  
            Debug.Log($"Left player scored: {leftPlayerScore}");
            if (leftPlayerScore == scoreToWin)
            {
                Debug.Log("Left player wins!");
                leftPlayerScore = 0;
                LeftScore.text = leftPlayerScore.ToString();
                LeftScore.color=Color.white;
                rightPlayerScore = 0;
                RightScore.text = rightPlayerScore.ToString();
                RightScore.color=Color.white;
            }
            else
            {
                ResetBall(1f);
            }
        }
    }

    void ResetBall(float directionSign)
    {
        ball.position = ballStartPos;

        // Start the ball within 20 degrees off-center toward direction indicated by directionSign
        directionSign = Mathf.Sign(directionSign);
        Vector3 newDirection = new Vector3(directionSign, 0f, 0f) * startSpeed;
        newDirection = Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f) * newDirection;

        var rbody = ball.GetComponent<Rigidbody>();
        rbody.velocity = newDirection;
        rbody.angularVelocity = new Vector3();
        PowerUP.gameObject.SetActive(true);
        PowerDown.gameObject.SetActive(true);

        // We are warping the ball to a new location, start the trail over
        ball.GetComponent<TrailRenderer>().Clear();
    }
}
