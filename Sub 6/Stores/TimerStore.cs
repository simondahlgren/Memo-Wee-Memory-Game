using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Sub_6.Stores
{
    public class TimerStore
    {
        public readonly Timer _timer;

        public DateTime _endTime;


        //https://youtu.be/44O5o-Gg6DM?t=269
        // Gives the difference between endtime and current time in seconds.
        public double EndTimeCurrentTimeSecondsDifference => TimeSpan.FromTicks(_endTime.Ticks).TotalSeconds - TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
        // Prevents timer from showing negative values.
        public double RemainingSeconds => EndTimeCurrentTimeSecondsDifference > 0 ? EndTimeCurrentTimeSecondsDifference : 0;

    public event Action RemainingSecondsChange;

        public TimerStore()

        {
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
            //Start(120);

        }


        public void Start(int durationInSeconds)
        {
            _timer.Start();        
            _endTime = DateTime.Now.AddSeconds(durationInSeconds);
            OnRemainingSecondsChange();

        }

        public void StopTime()
        {
            _timer.Stop();

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnRemainingSecondsChange();
        }

        private void OnRemainingSecondsChange()
        {
            RemainingSecondsChange?.Invoke();
        }

     
    }
}
