using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnvironment : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tile;
    public Transform playerTransform;
    private float lenght = 344.9f;
    private float spawnZ = 0.0f;
    private float safeZone;
    private int amnTileOnScreen = 3;
    private List<GameObject> activeTiles;
    private List<GameObject> activeObstacles;
    public GameObject[] Obstacles;
    private float spawnobst;
    public CharacterMovement speed;
    private void Start()
    {
        speed = FindObjectOfType<CharacterMovement>();    
        safeZone = lenght*2f;
        activeTiles = new List<GameObject>();
        SpawnTile();
        SpawnTile();
    }

    private void Update(){
        if(playerTransform.position.z - safeZone> (spawnZ-(amnTileOnScreen*lenght))){
            SpawnTile();
            DeleteTile();
            speed.FwdSpeed +=2;
        }
    }

    private void SpawnTile(){
        // int Tilenum = Random.Range(0,2);
        int Tilenum = 0;
        GameObject go = Instantiate(tile[Tilenum]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnobst = 32.2f + spawnZ;
        spawnZ += lenght;
        activeTiles.Add(go);
        for(int i=0;i<9;i++){
            int obstacleLane = Random.Range(-1,2);
            int obstacleNum = Random.Range(0,9);
            while(Obstacles[obstacleNum].tag == "2Way" && obstacleLane == -1){
                obstacleLane = Random.Range(0,2);
            }
            while(Obstacles[obstacleNum].tag == "3Way" && obstacleLane != 0){
                obstacleLane = 0;
            }
            Vector3 spawnpoint = new Vector3(obstacleLane*11f, 0.0f, spawnobst) ;
            Instantiate(Obstacles[obstacleNum], spawnpoint, Quaternion.identity, go.transform );
            spawnobst += 32.2f; 
        }

    //Fuck Finally Happened :(
    }

    private void DeleteTile(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
