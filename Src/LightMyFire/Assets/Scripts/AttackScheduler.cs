using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    // auxiliary class, list extension methods
    static class ListExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    class ShotConfiguration
    {
        public ShotType Type;
        public int Spawn;
        public float Duration;
    }
    class AttackScheduler
    {
        // types of shots are decided in advance
        private List<ShotConfiguration> futureShots = new List<ShotConfiguration>();
        // double at index I corresponds to the respective enum of ShotType
        private float[] shotTypeProbabilities;
        private float[] shotTypeDurations;
        private int shotSpawns = 3;
        private float difficulty;
        private static System.Random random = new Random();

        /// <summary>
        /// Returns a new scheduler.
        /// </summary>
        /// <param name="difficulty">Difficulty - float in between 0 (default) and 1 (max).</param>
        /// <param name="shotSpawns">Number of shot spawns. </param>
        public AttackScheduler(float difficulty, int shotSpawns)
        {
            // we suppose that targeted are generally more difficult than untargeted
            shotTypeProbabilities = new float[] { 0.3f, 0.05f, 0.3f, 0.05f, 0.25f, 0.05f };
            shotTypeDurations = new float[] { 4, 2, 3, 2, 3, 2};
            for (int i=0; i<6; i++)
            {
                shotTypeDurations[i] += 3 * difficulty;
                float coef = 0;
                // straight target
                if (i == 0)
                {
                    coef = 0.2f;
                }
                else if (i == 2)
                {
                    coef = 0.4f;
                }
                else if (i == 4)
                {
                    coef = 0.4f;
                }
                shotTypeProbabilities[i] += coef * difficulty;
            }
            this.shotSpawns = shotSpawns;
            this.difficulty = difficulty;
        }
        public ShotConfiguration ScheduleShooter()
        { 
            if (futureShots.Count != 0) 
            // return next attack
            {
                var future = futureShots[futureShots.Count - 1];
                futureShots.RemoveAt(futureShots.Count - 1);
                return future;
            }
            else 
            // prepare new batch of attacks!
            {
                ShotConfiguration config = new ShotConfiguration();
                // shot spawns should be random
                List<int> shotspawns = new List<int>();
                // e.g. 1 2 3 1 2 3 1 2 3 ...
                for (int i = 0; i < 12/shotSpawns; i++)
                {
                    for (int j = 0; j < shotSpawns; j++) shotspawns.Add(j); 
                }
                shotspawns.Shuffle();
                for (int i = 0; i < 12; i++)
                {
                    config = new ShotConfiguration();

                    // random shot
                    float r = (float)random.NextDouble()*(1+difficulty);
                    float probBuffer = shotTypeProbabilities[0];
                    int index = 0;
                    while (probBuffer < r)
                    {
                        index++;
                        probBuffer += shotTypeProbabilities[index];
                    }
                    config.Type = (ShotType)index;
                    config.Duration = shotTypeDurations[index];
                    config.Spawn = shotspawns[i];
                    if (i != 11) futureShots.Add(config);
                }
                return config;
            }
        }
        public void ScheduleAttack()
        {

        }
    }
}
