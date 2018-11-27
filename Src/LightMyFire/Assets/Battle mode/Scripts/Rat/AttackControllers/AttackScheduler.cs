using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class ShotConfiguration
    {
        public ShotType Type;
        public int Spawn;
        public float Duration;
    }
    class AttackScheduler
    {
        // double at index I corresponds to the respective enum of ShotType
        private float[] shotTypeProbabilities;
        private float[] shotTypeDurations;
        private int shotSpawns;
        private float[] shotSpawnEnds;
        private float difficulty;
        private static System.Random random = new System.Random();

        private bool CheckShotSpawns()
        {
            bool freeSpawn = false;
            for (int i = 0; i < shotSpawns; i++)
            {
                if (shotSpawnEnds[i] < Time.time) { shotSpawnEnds[i] = 0f; freeSpawn = true; }
            }
            return freeSpawn;
        }

        /// <summary>
        /// Returns a new scheduler.
        /// </summary>
        /// <param name="difficulty">Difficulty - float in between 0 (default) and 1 (max).</param>
        /// <param name="shotSpawns">Number of shot spawns. </param>
        public AttackScheduler(float difficulty, int shotSpawns)
        {
            this.shotSpawns = shotSpawns;
            shotSpawnEnds = new float[shotSpawns];
            // we suppose that targeted are generally more difficult than untargeted
            shotTypeProbabilities = new float[] { 0.35f, 0.0f, 0.35f, 0.0f, 0.30f, 0.0f };
            shotTypeDurations = new float[] { 2, 1, 2, 1, 2, 1};
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
            // checks available shot spawns
            if (!CheckShotSpawns()) { return null; }
            ShotConfiguration config = new ShotConfiguration();
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
            int spawn = random.Next(shotSpawns);
            while (shotSpawnEnds[spawn] != 0f) { spawn = random.Next(shotSpawns); }
            config.Spawn = spawn;
            shotSpawnEnds[spawn] = Time.time + shotTypeDurations[index];            
            return config;            
        }
        public float ScheduleAttackOpening(float defaultOpen)
        {
            return defaultOpen * (1.1f - difficulty);
        }
    }
}
