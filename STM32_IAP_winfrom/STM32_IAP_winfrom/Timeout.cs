using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STM32_IAP_winfrom
{
    class Timeout
    {
        private double TimeoutInterval;
        public long lastTime;
        public long elapsedTime;

        public Timeout(double Interval)
        {
            TimeoutInterval = Interval;
            lastTime = DateTime.Now.Ticks; //在实例化的时候获取最初的时间
        }
        public void updataLastTime()
        {
            lastTime = DateTime.Now.Ticks; //在实例化的时候获取最初的时间
        }
        public bool IsTimeout()
        {
            elapsedTime = DateTime.Now.Ticks - lastTime;
            TimeSpan span = new TimeSpan(elapsedTime);
            double diff = span.TotalSeconds;
            if (diff > TimeoutInterval)
                return true;
            else
                return false;
        }
    }
}
