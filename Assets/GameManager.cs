using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public ShootBallController ShootBallController;
        public Grid                Grid;

        public Vector3 InitialShootBallPosition = new Vector3(0f, -3.8f, 0f);

        private int _score      = 0;
        private int _totalScore = 0;

        private int _currentBallCount = 0;

        public int      TotalShootBallCount = 30;
        public TMP_Text Score1, Score2, Score3, TotalScore1, TotalScore2, TotalScore3;
        public TMP_Text ShootBallCountText;


        public GameObject MainMenu, SettignsMenu, PauseMenu, WinnerMenu, LoserMenu;

        public static bool EnableVibration = true;
        public static bool EnableSound     = true;

        public AudioSource AudioSource;

        private void OnEnable()
        {
            ShootBallController.FingerLift += OnFingerLift;
            ShootBall.BallHit              += OnBallHit;
        }

        private void OnBallHit(GameObject arg1, GameObject arg2)
        {
            if (arg1.name.Contains("Blackball"))
            {
                Destroy(arg2);
                UpdateScore();
            }
            else if (arg1.name == arg2.name)
            {
                Destroy(arg1);
                Destroy(arg2);
                UpdateScore();
            }
            else
            {
                Destroy(arg1);
            }
        }

        private void UpdateScore()
        {
            _score      += 1;
            Score1.text =  _score.ToString();
            Score2.text =  _score.ToString();
            Score3.text =  _score.ToString();

            if (EnableVibration)
            {
                Handheld.Vibrate();
            }

            if (_score >= Grid.InitialGridFill)
            {
                EndGame(true);
            }
        }

        private void OnFingerLift()
        {
            Invoke(nameof(LoadBall), 1f);
        }

        public void LoadBall()
        {
            if (_currentBallCount == TotalShootBallCount)
            {
                EndGame(false);
            }
            else
            {
                _currentBallCount += 1;
                GameObject obj = Grid.GetBall();
                obj.transform.position = InitialShootBallPosition;
                ShootBallController.AssignBall(obj);
                ShootBallController.enabled = true;
                ShootBallCountText.text     = (TotalShootBallCount - _currentBallCount).ToString();
            }
        }

        public void StartGame()
        {
            MainMenu.SetActive(false);
            LoserMenu.SetActive(false);
            WinnerMenu.SetActive(false);
            foreach (var ball in FindObjectsOfType<Collider2D>(true))
            {
                Destroy(ball.gameObject);
            }

            Grid.EmptyGridBalls();
            Grid.GenerateGrid();
            Grid.FillGridWithBalls();
            ShootBallCountText.text = TotalShootBallCount.ToString();
            ResetScore();
            _currentBallCount = 0;
            Invoke(nameof(LoadBall), 0.1f);
        }

        private void ResetScore()
        {
            _score      = 0;
            Score1.text = _score.ToString();
            Score2.text = _score.ToString();
            Score3.text = _score.ToString();
        }

        public void EndGame(bool isWin)
        {   
            Debug.Log(isWin);
            ShootBallController.enabled =  false;
            _totalScore                 += _score;
            TotalScore1.text            =  _totalScore.ToString();
            TotalScore2.text            =  _totalScore.ToString();
            TotalScore3.text            =  _totalScore.ToString();
            if (isWin)
            {
                ShowWinnerMenu();
            }
            else
            {
                ShowLoserMenu();
            }
        }

        public void ShowSettings()
        {
            SettignsMenu.SetActive(true);
        }

        public void HideSettings()
        {
            SettignsMenu.SetActive(false);
        }

        public void ToggleVibration(bool state)
        {
            EnableVibration = state;
        }

        public void ToogleSound(bool state)
        {
            EnableSound      = state;
            AudioSource.mute = !EnableSound;
        }

        private void ShowLoserMenu()
        {
            ShootBallController.enabled = false;
            LoserMenu.SetActive(true);
        }

        private void ShowWinnerMenu()
        {
            ShootBallController.enabled = false;
            WinnerMenu.SetActive(true);
        }

        public void ShowMenu()
        {
            ShootBallController.enabled = false;
            LoserMenu.SetActive(false);
            PauseMenu.SetActive(false);
            WinnerMenu.SetActive(false);
            MainMenu.SetActive(true);
        }

        public void ShowPauseMenu()
        {
            ShootBallController.enabled = false;
            PauseMenu.SetActive(true);
        }

        public void ResumeGame()
        {
            PauseMenu.SetActive(false);
            Invoke(nameof(EnableBallController), 0.1f);
        }

        private void EnableBallController()
        {
            ShootBallController.enabled = true;
        }
    }
}