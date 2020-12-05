
public struct TimerData
{
    public TimerData(float seconds, float minutes)
    {
        this.seconds = seconds;
        this.minutes = minutes;
    }

    public float seconds { get; set; }
    public float minutes { get; set; }

    public void addSeconds(float sec)
    {
        if (seconds + sec >= 60)
        {
            minutes++;
            seconds = seconds + sec - 60;
        } 
        else
            seconds += sec;
    }
}
