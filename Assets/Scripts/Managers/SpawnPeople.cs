using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPeople : MonoBehaviour
{
    [Header("People Spawn Position")]
    public GameObject peoplePrefab;
    string normalPeopleTag;
    public GameObject lazyPeoplePrefab;
    string lazyPeopleTag;
    public GameObject reactionaryPrefab;
    string reactionaryTag;
    public GameObject SpawnPos;
    [SerializeField] private float SpawnPosOffset = 2.0f;

    [Header("Crowdness Control")]
    public int initialNpcCount = 5;
    public float spawnWidth = 5f;
    public float spacing = 1f;
    public int npcCrowdness = 3;
    [SerializeField] int npcCrowdnessBias = 3;
    [SerializeField] float npcSpawnTimeSlot = 0.5f;
    [SerializeField] int maxPeopleSpawn = 500;
    float npcSpawnTimer = 0.0f;

    [Header("Difficulty Contorl")]
    [SerializeField] float lazyPeopleProbability = 0.05f; 
    [SerializeField] int maxLazyPeopleSpawn = 5;
    [SerializeField] float reactionaryProbability = 0.01f;
    [SerializeField] int maxreactonaryProbability = 2;

    private void Awake()
    {
        normalPeopleTag = peoplePrefab.tag;
        lazyPeopleTag = lazyPeoplePrefab.tag;
        reactionaryTag = reactionaryPrefab.tag;
    }
    void Start()
    {
        for (int i = 0; i < initialNpcCount; i++)
        {
            SpawnNewNPC();
        }
    }
    private void Update()
    {
        npcSpawnTimer += Time.deltaTime;
        if (npcSpawnTimer > npcSpawnTimeSlot)
        {
            int npcnubmer = Random.Range(-npcCrowdnessBias, npcCrowdnessBias) + npcCrowdness;
            for (int i = 0; i < npcnubmer; i++)
            {
                SpawnNewNPC();
            }
            npcSpawnTimer = 0;
        }


    }
    void SpawnNewNPC()
    {
        //decide positon to spawn
        float x = Random.Range(-spawnWidth / 2f, spawnWidth / 2f);
        //float y = -10f + i * spacing; // spawn from below
        float y = SpawnPos.transform.position.y + Random.Range(-SpawnPosOffset, SpawnPosOffset);
        Vector3 spawnPos = new Vector3(x, y, 0);

        //decide people type to spawn
        float probability = Random.Range(0.0f,1.0f);
        if (PeopleCounter.GetActive(lazyPeopleTag) < maxLazyPeopleSpawn && probability <= lazyPeopleProbability)
        {
            PoolManager.Instance.Spawn(lazyPeopleTag, spawnPos, Quaternion.identity);
        }
        else if (PeopleCounter.GetActive(reactionaryTag) < maxreactonaryProbability && probability <= lazyPeopleProbability + reactionaryProbability) {
            PoolManager.Instance.Spawn(reactionaryTag, spawnPos, Quaternion.identity);
        }
        else if (PeopleCounter.GetActive(normalPeopleTag) < maxPeopleSpawn)
        {
            var go = PoolManager.Instance.Spawn(normalPeopleTag, spawnPos, Quaternion.identity);
        }
        //Instantiate(npcPrefab, spawnPos, Quaternion.identity);
    }
    
}
