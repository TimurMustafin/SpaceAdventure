using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject EnemyPrefab;
        public int AmountInPool;
        public string EnemySortKey;
    }

    #region Singleton

    public static EnemyGenerator Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [Header("List of Enemy for Generation")]
    public List<EnemyType> EnemyTypeList;

    public Dictionary<string, Queue<GameObject>> DictionaryEnemyPool;


    [Header("Generator")]
    public int[] GeneratorRange;
    float RateOfGeneration;

    
    

    string[] EnemyKeys;
    int EnemyKeysIndex = 0;

    private void Start()
    {
        RateOfGeneration = GameMaster.Instance.levelData.GeneratorRate;
        timer = 5 / RateOfGeneration;

        EnemyKeys = new string[EnemyTypeList.Count];

        DictionaryEnemyPool = new Dictionary<string, Queue<GameObject>>();

        foreach (var enemyType in EnemyTypeList)
        {
            Queue<GameObject> EnemyPool = new Queue<GameObject>();        

            for (int i = 0; i < enemyType.AmountInPool; i++)
            {
                Vector3 offset = Vector3.right * UnityEngine.Random.Range(-GeneratorRange[0], GeneratorRange[0])
                                    + Vector3.up * UnityEngine.Random.Range(-GeneratorRange[1], GeneratorRange[1]);

                GameObject enemyInPool = Instantiate(enemyType.EnemyPrefab,
                                                        transform.position + offset, Quaternion.identity);

                enemyInPool.SetActive(false);
                EnemyPool.Enqueue(enemyInPool);
            }

            EnemyKeys[EnemyKeysIndex++] = enemyType.EnemySortKey;
            DictionaryEnemyPool.Add(enemyType.EnemySortKey, EnemyPool);
        }
    }

    float timer;
    private void Update()
    {
        if (!GameMaster.Instance.GameStarted)
            return;

        if (timer < 0)
        {
            Spawn(GetRandomKey());
            timer = 5 / RateOfGeneration;
        }
        timer -= Time.deltaTime;
    }
    

    public GameObject Spawn(string enemySortTag)
    {
        Queue<GameObject> poolEnemy = DictionaryEnemyPool[enemySortTag];

        if (poolEnemy.Count != 0)
        {
            GameObject enemyFromPool = DictionaryEnemyPool[enemySortTag].Dequeue();

            Vector3 offset = Vector3.right * UnityEngine.Random.Range(-GeneratorRange[0], GeneratorRange[0])
                                    + Vector3.up * UnityEngine.Random.Range(-GeneratorRange[1], GeneratorRange[1]);

            enemyFromPool.transform.position = transform.position + offset;
            enemyFromPool.SetActive(true);
            return enemyFromPool;
        }

        return null;      
    }

    private string GetRandomKey()
    {
        int index = UnityEngine.Random.Range(0, EnemyKeys.Length);
        return EnemyKeys[index];
    }

}
