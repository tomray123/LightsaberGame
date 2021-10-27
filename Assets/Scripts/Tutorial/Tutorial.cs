using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Help Texts")]
    public GameObject moveAroundText;
    public GameObject reflectText;
    public GameObject tryAgainText;
    public GameObject scoreHelpText;
    public GameObject getHitText1;
    public GameObject getHitText2;
    public GameObject otherEnemyReflectText;
    public GameObject wallReflectText;
    public GameObject trowSaberText;
    public GameObject finishText;

    [Space]

    [Header("UI")]
    public GameObject hpBar;
    public GameObject throwSaberIcon;
    public GameObject scoreText;
    public GameObject comboScoreIndicator;
    public GameObject playerShadow;
    private IconCooldown trowCooldown;

    [Space]

    [Header("Lines")]
    public GameObject greenLine1;
    public GameObject redLine1;
    public GameObject greenLine2;
    public GameObject redLine2;
    public GameObject greenLine3;
    public GameObject redLine3;

    [Space]

    [Header("Arrows")]
    public GameObject scoreArrow;
    public GameObject comboArrow;
    public GameObject healthArrow;

    [Space]

    [Header("Timings")]
    public float timeToMoveAround = 4f;
    public float timeToFreezeBullets = 1f;
    public float timeToTryAgain = 2f;
    public float timeAfterFirstKill = 2f;
    public float timeToShowScoreAndCombo = 10f;
    public float timeBeforeTwoHitEnemies = 2f;
    public float timeForGetHitTutor = 10f;
    public float timeAfterSecondKill = 2f;
    public float timeAfterThirdKill = 2f;
    public float finishTime = 5f;

    [Space]
    [Header("Other")]
    public BulletCatcher bulletCatcher;
    public GameObject reflectWall;
    public GameObject doubleTap;

    // Other variables
    private bool isMoved = false;
    private bool stopTime = false;
    private bool isTimeStopped = false;
    private bool allowToHitPlayer = false;
    private bool isPlayerHit = false;
    private Spawner spawner;
    private List<GameObject> enemies = new List<GameObject>();
    private float targetRotation = 90f;
    private GameObject player;
    private PauseController pauseController;

    // Start is called before the first frame update
    void Start()
    {
        BaseInitialization();
        StartCoroutine(Algorithm());
    }

    private IEnumerator Algorithm()
    {
        // --------------------------------------- 1. How to move and rotate ---------------------------------------
        // Waiting for player's movement.
        yield return new WaitUntil(() => isMoved == true);
        yield return new WaitForSeconds(timeToMoveAround);
        moveAroundText.SetActive(false);

        // --------------------------------------- 2. How to reflect ---------------------------------------
        // Spawning an enemy.
        enemies.Add(spawner.SpawnNext());
        stopTime = true;

        // Waiting for time freezing. 
        yield return new WaitUntil(() => isTimeStopped == true);
        yield return new WaitForSeconds(timeToFreezeBullets);
        targetRotation = 90f;
        reflectText.SetActive(true);
        playerShadow.SetActive(true);
        playerShadow.transform.rotation = Quaternion.Euler(0, 0, targetRotation);
        greenLine1.SetActive(true);
        redLine1.SetActive(true);

        // Pause for player.
        yield return new WaitForSeconds(1f);

        // Waiting for good player's position for reflection. 
        float currentRotation = player.transform.rotation.eulerAngles.z;
        while (currentRotation >= targetRotation + 5 || currentRotation <= targetRotation - 5)
        {
            currentRotation = player.transform.rotation.eulerAngles.z;
            yield return null;
        }

        // Pause for player.
        yield return new WaitForSeconds(0.5f);
        stopTime = false;
        isTimeStopped = false;
        UnFreezeEnemies();
        StartCoroutine(bulletCatcher.UnFreezeBullets(timeToFreezeBullets));

        // Pause for player.
        yield return new WaitForSeconds(2.5f);

        // Waiting for killing an enemy.
        Enemy enemy1 = enemies[0].GetComponent<Enemy>();

        if (enemy1.hp > 0)
        {
            reflectText.SetActive(false);
            tryAgainText.SetActive(true);
            while (enemy1.hp > 0)
            {
                yield return null;
            }
        }

        // If enemy is killed.
        playerShadow.SetActive(false);
        tryAgainText.SetActive(false);
        reflectText.SetActive(false);
        greenLine1.SetActive(false);
        redLine1.SetActive(false);

        // Clearing lists of bullets and enemies.
        bulletCatcher.ClearBullets();
        enemies.Clear();

        // --------------------------------------- 3. About score system ---------------------------------------
        yield return new WaitForSeconds(timeAfterFirstKill);
        scoreHelpText.SetActive(true);
        scoreArrow.SetActive(true);
        comboArrow.SetActive(true);

        // Waiting until player will get how combo system works.
        yield return new WaitForSeconds(timeToShowScoreAndCombo);
        scoreHelpText.SetActive(false);
        scoreArrow.SetActive(false);
        comboArrow.SetActive(false);
        getHitText1.SetActive(true);

        // --------------------------------------- 4. How to get hit ---------------------------------------
        // Waiting for 2 enemies.
        yield return new WaitForSeconds(timeBeforeTwoHitEnemies);

        // Spawning enemies.
        enemies.Add(spawner.SpawnNext());
        enemies.Add(spawner.SpawnNext());
        allowToHitPlayer = true;     

        // Waiting for player hit. 
        yield return new WaitUntil(() => isPlayerHit == true);
        yield return new WaitForSeconds(0.5f);

        FreezeEnemies();
        StartCoroutine(bulletCatcher.FreezeBullets(0f));
        getHitText1.SetActive(false);
        getHitText2.SetActive(true);
        comboArrow.SetActive(true);
        healthArrow.SetActive(true);

        // Waiting until get hit tutorial will finish.
        yield return new WaitForSeconds(timeForGetHitTutor);

        // Destroying all enemies.
        foreach (GameObject e in enemies)
        {           
            if (e.activeSelf)
            {
                Enemy enm = e.GetComponent<Enemy>();
                enm.hp = 0;
                enm.DestroyWhenDead();
            }
        }

        getHitText2.SetActive(false);
        comboArrow.SetActive(false);
        healthArrow.SetActive(false);

        // Clearing lists of bullets and enemies.
        bulletCatcher.ClearBullets();
        enemies.Clear();

        // --------------------------------------- 5. How to reflect bullet at another enemy ---------------------------------------
        // Healing the player.
        BasePlayerSettings playerSettings = player.GetComponent<BasePlayerSettings>();
         playerSettings.Heal(playerSettings.maxHealth);

        // Spawning enemies.
        enemies.Add(spawner.SpawnNext());
        enemies.Add(spawner.SpawnNext());
        allowToHitPlayer = false;

        // Disabling enemy on second spawn point.
        Enemy enemy2 = enemies[0].GetComponent<Enemy>();
        enemy2.StopAllCoroutines();
        enemy2.startLoop = false;

        stopTime = true;

        // Waiting for time freezing. 
        yield return new WaitUntil(() => isTimeStopped == true);
        yield return new WaitForSeconds(timeToFreezeBullets);

        // When time is stopped.
        otherEnemyReflectText.SetActive(true);
        greenLine2.SetActive(true);
        redLine2.SetActive(true);
        targetRotation = 270f;
        playerShadow.SetActive(true);
        playerShadow.transform.rotation = Quaternion.Euler(0, 0, targetRotation);

        // Waiting for good player's position for reflection. 
        currentRotation = player.transform.rotation.eulerAngles.z;
        while (currentRotation >= targetRotation + 5 || currentRotation <= targetRotation - 5)
        {
            currentRotation = player.transform.rotation.eulerAngles.z;
            yield return null;
        }

        // Second enemy must be immortal.
        Enemy enemy3 = enemies[1].GetComponent<Enemy>();
        enemy3.hp = 10000;

        // Pause for player.
        yield return new WaitForSeconds(1f);
        stopTime = false;
        isTimeStopped = false;
        UnFreezeEnemies(1);
        StartCoroutine(bulletCatcher.UnFreezeBullets(timeToFreezeBullets));

        // Pause for player.
        yield return new WaitForSeconds(2.5f);

        // Waiting for killing an enemy.
        if (enemy2.hp > 0)
        {
            otherEnemyReflectText.SetActive(false);
            tryAgainText.SetActive(true);
            while (enemy2.hp > 0)
            {
                // Second enemy must be immortal.
                enemy3.hp = 10000;
                yield return new WaitForSeconds(1f);
            }
        }

        // If enemy is killed.
        playerShadow.SetActive(false);
        otherEnemyReflectText.SetActive(false);
        tryAgainText.SetActive(false);
        greenLine2.SetActive(false);
        redLine2.SetActive(false);

        // Destroying other enemy.
        enemy3 = enemies[1].GetComponent<Enemy>();
        enemy3.hp = 0;
        enemy3.DestroyWhenDead();


        // Clearing lists of bullets and enemies.
        bulletCatcher.ClearBullets();
        enemies.Clear();

        yield return new WaitForSeconds(timeAfterSecondKill);

        // --------------------------------------- 6. How to reflect bullets with reflect walls ---------------------------------------
        wallReflectText.SetActive(true);
        reflectWall.SetActive(true);

        // Spawning enemy.
        enemies.Add(spawner.SpawnNext());
        allowToHitPlayer = false;

        // Making enemy immortal until bullet will reflect from the wall.
        Enemy enemy4 = enemies[0].GetComponent<Enemy>();
        enemy4.hp = 10000;
        enemy4.OnTutorHit += CheckEnemyDestroyability;

        stopTime = true;

        // Waiting for time freezing. 
        yield return new WaitUntil(() => isTimeStopped == true);
        yield return new WaitForSeconds(timeToFreezeBullets);

        // When time is stopped.
        greenLine3.SetActive(true);
        redLine3.SetActive(true);
        targetRotation = 290f;
        playerShadow.SetActive(true);
        playerShadow.transform.rotation = Quaternion.Euler(0, 0, targetRotation);

        // Waiting for good player's position for reflection. 
        currentRotation = player.transform.rotation.eulerAngles.z;
        while (currentRotation >= targetRotation + 5 || currentRotation <= targetRotation - 5)
        {
            currentRotation = player.transform.rotation.eulerAngles.z;
            yield return null;
        }

        // Pause for player.
        yield return new WaitForSeconds(1f);
        stopTime = false;
        isTimeStopped = false;
        UnFreezeEnemies();
        StartCoroutine(bulletCatcher.UnFreezeBullets(timeToFreezeBullets));

        // Pause for player.
        yield return new WaitForSeconds(2.5f);

        if (enemy4.hp > 0)
        {
            wallReflectText.SetActive(false);
            tryAgainText.SetActive(true);
            while (enemy4.hp > 0)
            {
                yield return null;
            }
        }

        // If enemy is killed.
        playerShadow.SetActive(false);
        wallReflectText.SetActive(false);
        reflectWall.SetActive(false);
        tryAgainText.SetActive(false);
        greenLine3.SetActive(false);
        redLine3.SetActive(false);

        // Clearing lists of bullets and enemies.
        bulletCatcher.ClearBullets();
        enemies.Clear();

        yield return new WaitForSeconds(timeAfterThirdKill);

        // --------------------------------------- 7. How to throw saber ---------------------------------------
        trowSaberText.SetActive(true);
        trowCooldown.isFinishedCooldown = true;

        // Spawning enemies.
        enemies.Add(spawner.SpawnNext());

        // Disabling enemy on tenth spawn point.
        Enemy enemy5 = enemies[0].GetComponent<Enemy>();
        enemy5.StopAllCoroutines();
        enemy5.startLoop = false;

        doubleTap.SetActive(true);

        while (enemy5.hp > 0)
        {
            yield return null;
        }

        // If enemy is killed.
        trowSaberText.SetActive(false);
        doubleTap.SetActive(false);

        // Clearing lists of enemies.
        enemies.Clear();

        // Little pause.
        yield return new WaitForSeconds(0.5f);

        finishText.SetActive(true);
        yield return new WaitForSeconds(finishTime);

        pauseController.LoadLevel(1);
    }

    private void BaseInitialization()
    {
        trowCooldown = throwSaberIcon.GetComponent<IconCooldown>();
        trowCooldown.isFinishedCooldown = false;

        // Texts.
        moveAroundText.SetActive(true);
        reflectText.SetActive(false);
        tryAgainText.SetActive(false);
        scoreHelpText.SetActive(false);
        getHitText1.SetActive(false);
        getHitText2.SetActive(false);
        otherEnemyReflectText.SetActive(false);
        wallReflectText.SetActive(false);
        trowSaberText.SetActive(false);
        finishText.SetActive(false);

        // Lines.
        greenLine1.SetActive(false);
        redLine1.SetActive(false);
        greenLine2.SetActive(false);
        redLine2.SetActive(false);
        greenLine3.SetActive(false);
        redLine3.SetActive(false);

        // Arrows.
        scoreArrow.SetActive(false);
        comboArrow.SetActive(false);
        healthArrow.SetActive(false);

        // Other.
        reflectWall.SetActive(false);
        playerShadow.SetActive(false);
        doubleTap.SetActive(false);

        spawner = Spawner.instance;
        pauseController = GetComponent<PauseController>();
        player = LevelManager.instance.player;
        player.GetComponent<BasePlayerSettings>().OnHit += OnPlayerHit;
    }

    public void CheckEnemyDestroyability(int reflectNum)
    {
        // If bullet was reflected from the wall.
        if (reflectNum > 1)
        {
            Enemy enemy = enemies[0].GetComponent<Enemy>();
            enemy.hp = enemy.startHp;
            enemy.OnTutorHit -= CheckEnemyDestroyability;
        }
    }

    public void StartMovement()
    {
        isMoved = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for bullet layer.
        if (collision.gameObject.layer == 8)
        {
            if (stopTime)
            {
                FreezeEnemies();
                StartCoroutine(bulletCatcher.FreezeBullets(timeToFreezeBullets));
                isTimeStopped = true;
            }
        }
    }

    private void FreezeEnemies()
    {
        foreach (GameObject e in enemies)
        {
            Enemy enemy = e.GetComponent<Enemy>();
            enemy.StopAllCoroutines();
            enemy.startLoop = false;
        }
    }

    private void UnFreezeEnemies()
    {
        foreach (GameObject e in enemies)
        {
            Enemy enemy = e.GetComponent<Enemy>();
            enemy.startLoop = true;
            enemy.isJustBorn = true;
        }
    }

    private void UnFreezeEnemies(int enemyIndex)
    { 
        Enemy enemy = enemies[enemyIndex].GetComponent<Enemy>();
        enemy.startLoop = true;
        enemy.isJustBorn = true;
    }

    private void OnPlayerHit()
    {
        BasePlayerSettings playerSettings = player.GetComponent<BasePlayerSettings>();
        if (!allowToHitPlayer)
        {
            playerSettings.Heal(playerSettings.maxHealth);
        }
        else
        {
            isPlayerHit = true;
        }
    }
}
