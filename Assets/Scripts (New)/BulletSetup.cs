using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

[CreateAssetMenu(fileName = "Bullet", menuName = "Object/Bullet")]
public class BulletSetup : ScriptableObject
{
    [Header("Sprites and Idk")]
    public Sprite bulletSprite;
    public GameObject particlePrefab;
    public GameObject exposionPrefab;
    [Header("Floats and Values")]
    public float speed = 70f;
    public UpgradableType bulletDamage;
    public float freezeTowerSlowdown = 0;
    public float explosionDamage_subtractfrombulletdamage = -10f;
    public float explosionRadius = 5;
    public float freezeDuration = 0;
    public float realTNTFuse;
    [Header("Bools")]
    public bool followTarget = true;
    public bool canSlowEnemies = false;
    public bool hasParticles = false;
    public bool stopMoving;
    public bool isTNT = false;
    public bool otherbullets = true;
    public bool hasExplosion = false;
    [Header("Upgrades")]
    public List<TypeOfUpgrade> upgrades = new List<TypeOfUpgrade>();
    [Header("Damage Type and Chance")]

    public bool canDealDankDmg;
    [ShowIf(nameof(canDealDankDmg), true)]
    [SerializeField] float _dankDmgChance = 0;
    public float dankDmgChance => canDealDankDmg? _dankDmgChance: 0;
    [ShowIf(nameof(canDealDankDmg), true)]
    public float dankDmgMultiplier;

    public bool canDealSurrealDmg;
    [ShowIf(nameof(canDealSurrealDmg), true)]
    [SerializeField] float _surrealDmgChance = 0;
    public float surrealDmgChance => canDealSurrealDmg? _surrealDmgChance: 0;
    [ShowIf(nameof(canDealSurrealDmg), true)]
    public float surrealDmgMultiplier;

    public bool canDealNoscopeDmg;
    [ShowIf(nameof(canDealNoscopeDmg), true)]
    [SerializeField] float _noscopeDmgChance = 0;
    public float NoscopeDmgChance => canDealNoscopeDmg? _noscopeDmgChance: 0;
    [ShowIf(nameof(canDealNoscopeDmg), true)]
    public float noscopeDmg = Mathf.Infinity;
}
