﻿using System;
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

    class ShootingController
    {
        List<GameObject> currentShots = new List<GameObject>();
        List<Vector3> shotSpawns = new List<Vector3>();
        List<float> shootingEnds = new List<float>();
        private float nextFire = 0;
        private float fireRate = 0.5f;
        
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

        public void Shoot()
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                FireAllShooters();
            }
        }
        public void AddShooter(GameObject shot, Vector3 shotSpawn, float shootingDuration)
        {
            currentShots.Add(shot);
            shotSpawns.Add(shotSpawn);
            shootingEnds.Add(Time.time + shootingDuration);
        }
        private void RemoveShooter(int index)
        {
            currentShots.RemoveAt(index);
            shotSpawns.RemoveAt(index);
            shootingEnds.RemoveAt(index);
        }
        private void FireAllShooters()
        {
            List<int> deadShooters = new List<int>();
            for (int i = 0; i < currentShots.Count; i++)
            {
                GameObject.Instantiate(currentShots[i], shotSpawns[i], Quaternion.identity);
                if (shootingEnds[i] < Time.time) deadShooters.Add(i);
            }
            for (int i = deadShooters.Count - 1; i >= 0; i--)
            {
                RemoveShooter(i);
            }
        }
    }
}
