using UnityEngine;

public class Timer
{
    private float m_Time;

    public Timer(float timeToCount)
    {
        m_Time = timeToCount;
    }

    public bool IsFinished => m_Time <= 0f;

    public void Tick(float deltaTime)
    {
        m_Time -= deltaTime;
    }

    public void Reset(float timeToCount)
    {
        m_Time = timeToCount;
    }
}