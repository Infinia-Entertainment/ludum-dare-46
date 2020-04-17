using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using System;

namespace WeaponSystem.Projectile
{
    public abstract class ProjectileBase
    {
        /// <summary>
        /// Appearance prefab
        /// </summary>
        [OdinSerialize] public GameObject Appearance { get; set; }

        /// <summary>
        /// Base damage before fallout
        /// </summary>
        [OdinSerialize]
        public int BaseDamage { get; set; }

        /// <summary>
        /// Falling damage of projectile over distance in unit
        /// </summary>
        [OdinSerialize]
        public AnimationCurve FalloutCurve { get; set; }

        /// <summary>
        /// Falling Y of projectile over distance in unit / second in unity
        /// </summary>
        [OdinSerialize]
        public AnimationCurve BulletDropCurve { get; set; }

        /// <summary>
        /// Speed of the bullet in unit/second
        /// </summary>
        [OdinSerialize]
        public float BulletSpeed { get; set; }

        /// <summary>
        /// Event played on hit
        /// </summary>
        public Action<Collision> OnHit { get; set; }

        /// <summary>
        /// Initialize position data
        /// </summary>
        public abstract void InitializePositionData(Transform transform);

        /// <summary>
        /// Get position to translate to this frame
        /// </summary>
        public abstract Vector3 GetNextPosition(Transform transform);

        public abstract ProjectileBase Clone();

    }
}
