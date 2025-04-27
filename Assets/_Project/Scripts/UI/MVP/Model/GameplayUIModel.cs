using System;
using System.Numerics;

namespace UI.Model
{
    public class GameplayUIModel
    {
        public event Action CurrentScoreChanged;
        public event Action CurrentLaserCountChanged;
        public event Action CooldawnLaserCurrentTimeChanged;
        public event Action CoordinatesShipChanged;
        public event Action SpeedShipChanged;
        public event Action RotationShipChanged;

        public int CurrentScore { get; private set; }
        public int MaxLaserCount { get; private set; }
        public int CurrentLaserCount { get; private set; }

        public Vector2 CoordinatesShip { get; private set; }
        public Vector2 SpeedShip { get; private set; }
        public float RotationShip { get; private set; }

        public int CooldawnLaserMaxTime { get; private set; }
        public int CooldawnLaserCurrentTime { get; private set; }

        public void SetInitialValues(int currentScore, int maxLaserCount)
        {
            CurrentScore = currentScore;

            MaxLaserCount = maxLaserCount;
            CurrentLaserCount = MaxLaserCount;
            
            CurrentScoreChanged?.Invoke();
            CurrentLaserCountChanged?.Invoke();
        }

        public void SetCurrentLaserCount(int currentLaserCount)
        {
            CurrentLaserCount = currentLaserCount;
            CurrentLaserCountChanged?.Invoke();
        }

        public void SetCooldawnLaserCurrentTime(int cooldawnLaserCurrentTime)
        {
            CooldawnLaserCurrentTime = cooldawnLaserCurrentTime;
            CooldawnLaserCurrentTimeChanged?.Invoke();
        }

        public void SetCurrentScore(int currentScore)
        {
            CurrentScore = currentScore;
            CurrentScoreChanged?.Invoke();
        }

        public void SetCoordinates(Vector2 shipCoordinates)
        {
            CoordinatesShip = shipCoordinates;
            CoordinatesShipChanged?.Invoke();
        }

        public void SetSpeed(Vector2 speed)
        {
            SpeedShip = speed;
            SpeedShipChanged?.Invoke();
        }

        public void SetRotation(float shipRotation)
        {
            RotationShip = shipRotation;
            RotationShipChanged?.Invoke();
        }
    }
}