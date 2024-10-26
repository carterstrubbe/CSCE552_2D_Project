using UnityEngine;
using TMPro;

[System.Serializable]
public class Round {
    public int roundIndex;
    public int lowEnemyCount;
    public int medEnemyCount;
    public int hiEnemyCount;
    public Round(int newIndex, int newLowEnemyCount, int newMedEnemyCount, int newHiEnemyCount) {
        roundIndex = newIndex;
        lowEnemyCount = newLowEnemyCount;
        medEnemyCount = newMedEnemyCount;
        hiEnemyCount = newHiEnemyCount;
    }
}
public class EnemyManager : MonoBehaviour
{
    public AudioSource src;
    public AudioClip roundCompleteSound;
    public GameObject winMessage;
    public TextMeshProUGUI roundUI;
    public TextMeshProUGUI nextRoundCountdown;
    public GameObject lowTierEnemy;
    public GameObject midTierEnemy;
    public GameObject hiTierEnemy;
    [SerializeField] Round[] rounds;
    private int currentRound = 1;
    private bool roundInProgress = false;
    private bool breakTime = true;
    private const float timeToBreakConst = 10.0f;
    private float timeToBreak = timeToBreakConst;
    [SerializeField] Transform[] spawnPoints;
    bool allEnemiesSpawned;
    private GameObject[] activeEnemies;
    public int totalEnemiesActive = 0;

    // Start is called before the first frame update
    void Start()
    {
        activeEnemies = new GameObject[0];
        allEnemiesSpawned = false;
        roundUI.SetText("Round: 0");
        nextRoundCountdown.SetText("Next Round in: 30 sec");
        int i = 0;
        foreach (Round round in rounds) {
            ++i;
            round.roundIndex = i;
            if (i < 3) {
                round.lowEnemyCount = i * 5;
            } else if (i >= 3 && i < 10) {
                round.lowEnemyCount = i * 7;
                round.medEnemyCount = i * 2;
            } else {
                round.lowEnemyCount = i * 10;
                round.medEnemyCount = i * 3;
                round.hiEnemyCount = Mathf.RoundToInt(i/5);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {     
        if (breakTime) {
            
            timeToBreak -= Time.deltaTime;

            if (timeToBreak <= 0.0f) {
                breakTime = false;
            }

            nextRoundCountdown.SetText("Next round in: " + Mathf.RoundToInt(timeToBreak).ToString() + " seconds");

        } else if (!roundInProgress && currentRound <= rounds.Length) {
            nextRoundCountdown.SetText("");
            StartRound(currentRound);
        } else if (roundInProgress && allEnemiesSpawned) {
            
            // Start checking for when all enemies are eliminated
            bool enemyAlive = false;
            foreach (GameObject enemy in activeEnemies) {
                if (enemy != null) {
                    enemyAlive = true;
                    break;
                }
            }
            if (!enemyAlive) {
                src.clip = roundCompleteSound;
                src.Play();
                roundInProgress = false;
                timeToBreak = timeToBreakConst;
                breakTime = true;
                ++currentRound;
            }
        }

        if (currentRound > rounds.Length) {
            winMessage.SetActive(true);
        }
    }

    void StartRound(int round) {
        Debug.Log("Current log: " + currentRound);

        roundInProgress = true;
        roundUI.SetText("Round: " + round.ToString());

        int low = rounds[round-1].lowEnemyCount;
        int med = rounds[round-1].medEnemyCount;
        int hi = rounds[round-1].hiEnemyCount;

        activeEnemies = new GameObject[low+med+hi];
        allEnemiesSpawned = false;

        int i = 0;
        int j = 0;
        while (!allEnemiesSpawned) {

            // //TEST CODE
            // allEnemiesSpawned = true;
            // break;
            // ////////

            foreach (Transform spawn in spawnPoints) {
                if (low > 0) {
                    if (i == 0) {
                        activeEnemies[j]=Instantiate(lowTierEnemy, spawn.position, Quaternion.identity);
                        ++j;
                        low -= 1;
                    }
                    ++i;
                } else if (med > 0) {
                    if (i == 1) {
                        activeEnemies[j]=Instantiate(midTierEnemy, spawn.position, Quaternion.identity);
                        ++j;
                        med -= 1;
                    }
                    ++i;
                } else if (hi > 0) {
                    if (i == 2) {
                        activeEnemies[j]=Instantiate(hiTierEnemy, spawn.position, Quaternion.identity);
                        ++j;
                        hi -= 1;
                    }
                }

                if (i > 2) {
                    i = 0;
                }
            }

            if (low <= 0 && med <= 0 && hi <= 0) {
                allEnemiesSpawned = true;
            }
        }
        
        
        
    }
}
