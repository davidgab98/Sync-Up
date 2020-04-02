using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncStructsGenerator : MonoBehaviour {

    public List<GameObject> syncs; //Syncs can be generated

    public float[] defaultPositions; //Default positions for syncs
    GameObject[] takenPositions;           //Indicates if the position is taken //null = free position
    int lastPositionDestroyedIndex;       //Indicate the last position where a sync was destroyed for avoid generate one there because player may be there

    void Start() {
        takenPositions = new GameObject[defaultPositions.Length];
        lastPositionDestroyedIndex = -1;
    }

    //This method update the last position of sync destroyed for avoid to generate a new sync in that position because player may be there
    public void updateLastPositionDestroyed(GameObject syncDestroyed) { 
        for(int i = 0; i < defaultPositions.Length; i++) {
            if(defaultPositions[i] == syncDestroyed.transform.position.y) { 
                lastPositionDestroyedIndex = i;
                takenPositions[i] = null;
                break;
            }
        }
    }

    public void GenerateSyncs() {
        GameObject syncToGenerate = getNextSyncToGenerate();

        if(syncToGenerate != null) {
            List<int> freePositionsIndex = GetFreePositionsIndex();
            if((syncToGenerate.tag != "teleSync" && freePositionsIndex.Count > 0) || freePositionsIndex.Count > 1) { //Si no es teleSync y queda al menos 1 posicion libre O si quedan mas de 1 posicion libre
                
                if(syncToGenerate.tag == "teleSync") {
                    InstantiateTeleSync(freePositionsIndex);
                } else {
                    InstantiateNormalSync(syncToGenerate, freePositionsIndex);
                }
            }
        }
    }

    void InstantiateTeleSync(List<int> freePositions) { 

    }

    void InstantiateNormalSync(GameObject syncToGenerate, List<int> freePositionsIndex) { 
        int randomIndexPosition  = freePositionsIndex[Random.Range(0, freePositionsIndex.Count)]; //A random position of freePositionsIndex
        float positionToGenerate = defaultPositions[randomIndexPosition]; 

        Instantiate(syncToGenerate, new Vector3(0, positionToGenerate, 0), Quaternion.identity);

        takenPositions[randomIndexPosition] = syncToGenerate; //Put the new position taken to true
    }

    GameObject getNextSyncToGenerate() {
        GameObject nextSyncToGenerate = null;

        //Temporarily it's random, in future will be with probabilities for each type of sync
        int num = Random.Range(0, syncs.Count);
        nextSyncToGenerate = syncs[num];

        return nextSyncToGenerate;
    }

    List<int> GetFreePositionsIndex() {
        List<int> freePositionsIndex = new List<int>();

        for(int i = 0; i < takenPositions.Length; i++) {
            if(takenPositions[i] == null && i != lastPositionDestroyedIndex) {
                freePositionsIndex.Add(i);
            }
        }

        return freePositionsIndex;
    }
}
