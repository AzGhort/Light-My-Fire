using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    enum ShotType
    {
        STRAIGHT_TARGETED, STRAIGHT_UNTARGETED, SINE_TARGETED, SINE_UNTARGETED, RANDOM_TARGETED, RANDOM_UNTARGETED
    }

    class ShotController
    {
        List<GameObject> currentShots = new List<GameObject>();
        List<Transform> shotSpawns = new List<Transform>();
        List<float> shootingEnds = new List<float>();
        List<Shooter> shooters = new List<Shooter>();
        private float nextFire = 0;
        private float fireRate = 0.2f;
        
        // number of shooters currently shooting
        public int ShootersCount()
        {
            return currentShots.Count;
        }
        // time left until the end of last shooting
        public float FutureShootingTime()
        {
            return shootingEnds.Max() - Time.time;
        }

        public ShotController()
        {

        }
        public void Shoot()
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                FireAllShooters();
            }
        }
        public void AddShooter(Shooter shooter, GameObject shot, Transform shotSpawn, float shootingDuration)
        {
            shooters.Add(shooter);
            currentShots.Add(shot);
            shotSpawns.Add(shotSpawn);
            //shotTypes.Add(shotType);
            shootingEnds.Add(Time.time + shootingDuration);
        }
        private void RemoveShooter(int index)
        {
            shooters.RemoveAt(index);
            currentShots.RemoveAt(index);
            shotSpawns.RemoveAt(index);
            //shotTypes.RemoveAt(index);
            shootingEnds.RemoveAt(index);
        }
        private void FireAllShooters()
        {
            List<int> deadShooters = new List<int>();
            for (int i = 0; i < currentShots.Count; i++)
            {
                GameObject.Instantiate(currentShots[i], shotSpawns[i].position, shotSpawns[i].rotation);
                if (shootingEnds[i] < Time.time) deadShooters.Add(i);
            }
            for (int i = deadShooters.Count - 1; i >= 0; i--)
            {
                RemoveShooter(i);
            }
        }
    }
}
