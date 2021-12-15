using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnvironment : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] tile;
    public Transform playerTransform;
    private float lenght = 55f;
    private float spawnZ = 0.0f;
    private float safeZone;
    private int amnTileOnScreen = 12;
    private GameObject newobst;
    private List<GameObject> activeTiles;
    private List<GameObject> activeObstacles;
    public GameObject[] Obstacles;
    private float spawnobst;
    [HideInInspector] public CharacterMovement speed;
    
    private void Start()
    {
        speed = FindObjectOfType<CharacterMovement>();    
        safeZone = lenght*4f;
        activeTiles = new List<GameObject>();
        activeObstacles = new List<GameObject>();
        spawnobst=0f;
        for(int i=0;i<11;i++)
        {
            SpawnTile();
        }
        for(int i=0;i<17;i++){
            SpawnObstacle();
        }
    }

    private void Update(){
        if(playerTransform.position.z - safeZone> (spawnZ-(amnTileOnScreen*lenght))){
            SpawnTile();
            SpawnObstacle();
            StartCoroutine(DeleteTile());
            if(speed.FwdSpeed < 50f ) speed.FwdSpeed +=0.30f;
        }
    }

    private void SpawnTile(){
        int Tilenum = Random.Range(0,6);
        // int Tilenum = 0;
        GameObject go = Instantiate(tile[Tilenum]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += lenght;
        activeTiles.Add(go);
        
    }

    private void SpawnObstacle(){
        spawnobst += 40f + (speed.FwdSpeed - 25f);
        int obstacleLane = Random.Range(-1,2);
        int obstacleNum = Random.Range(0,7);
        while(Obstacles[obstacleNum].tag == "2Way" && obstacleLane == -1){
            obstacleLane = Random.Range(0,2);
        }
        while(Obstacles[obstacleNum].tag == "3Way" && obstacleLane != 0){
            obstacleLane = 0;
        }
        Vector3 spawnpoint = new Vector3(obstacleLane*14f, 0.0f, spawnobst) ;
        
        if(spawnobst > 45f) {
            newobst = Instantiate(Obstacles[obstacleNum], spawnpoint, Quaternion.identity ) as GameObject;
        } 
        activeObstacles.Add(newobst);
    }

    public IEnumerator DeleteTile(){
        yield return new WaitForSeconds(1.7f);
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
        if(playerTransform.position.z - 30f > activeObstacles[1].transform.position.z)
        {
            Destroy(activeObstacles[1]);
            activeObstacles.RemoveAt(1);
        }
    }
}
