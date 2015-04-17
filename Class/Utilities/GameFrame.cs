using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tap
{
    class GameFrame
    {
        public const int MAXIMUM_SAMPLES = 60;

        private Queue<float> sampleBuffer = new Queue<float>();
        private int ticks = 0;

        public void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (sampleBuffer.Count > MAXIMUM_SAMPLES)
            {
                sampleBuffer.Dequeue();
                AverageFramesPerSecond = sampleBuffer.Average(i => i);
            } 
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
            TotalSeconds += deltaTime;
        }

        public Boolean Wait100ms()
        {
            if (ticks++ >= this.AverageFramesPerSecond / 20)
            {
                ticks = 0;
                return true;
            }
            return false;
        }

        public long TotalFrames 
        { 
            get; 
            private set; 
        }

        public float TotalSeconds 
        {
            get; 
            private set; 
        }

        public float AverageFramesPerSecond 
        { 
            get; 
            private set; 
        }

        public float CurrentFramesPerSecond 
        { 
            get; 
            private set; 
        }
    }
}
