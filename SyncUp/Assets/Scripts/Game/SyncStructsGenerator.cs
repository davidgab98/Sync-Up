using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public struct Estructures{
    public List<GameObject> estructures;
    public int[]            apparitionProbability;
}
*/

public class SyncStructsGenerator : MonoBehaviour {

    public List<GameObject> syncs; //Syncs can be generated 
    public List<GameObject> saws; //Saws can be generated   
    //public int[] syncsProbabilities;
    //public int[] sawsProbabilities;

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
        UpdateSawsInTable();
        GenerateSyncWithSmall();
        GenerateSyncWithoutSmall();
    }

    void GenerateSyncWithSmall() {
        //CALCULAR CUANTOS SYNCS CON SMALL GENERAR y hacer for (segun nivel y randomRange)
        GameObject syncToGenerate = getNextSyncToGenerate();
        if(syncToGenerate != null) {
            List<int> freePositionsIndex = GetFreePositionsIndex();
            if((syncToGenerate.tag != "teleSync" && freePositionsIndex.Count > 0) || freePositionsIndex.Count > 1) { //Si no es teleSync y queda al menos 1 posicion libre O si quedan mas de 1 posicion libre

                if(syncToGenerate.tag == "teleSync") {
                    InstantiateTeleSync(syncToGenerate, freePositionsIndex);
                } else {
                    InstantiateNormalSync(syncToGenerate, freePositionsIndex);
                }
            }
        }
    }

    void GenerateSyncWithoutSmall() {
        GameObject sawToGenerate = getNextSawToGenerate();
        List<int> freePositionsIndex = GetFreePositionsIndex();
        if(freePositionsIndex.Count > 0) {
            GameObject sawGenerated = InstantiateNormalSync(sawToGenerate, freePositionsIndex);
            //Cogemos el componente saw y le ponemos los valores correspondientes de numero maximo en mesa (segun nivel y randomRange)
            //sawGenerated.GetComponent<Saw>().maxSyncsInTable = 2;
        }
    }

    //Incrementa en 1 el numero de sincronizaciones que llevan las saws en mesa y elimina las correspondientes
    void UpdateSawsInTable() {
        //Recorremos los objetos en mesa
        for(int i = 0; i < takenPositions.Length; i++) {
            //Comprobamos los que sean saws
            if(takenPositions[i] != null && takenPositions[i].transform.CompareTag("sawEstructure")) {
                Saw sawComponent = takenPositions[i].GetComponent<Saw>();
                //Incrementamos en 1 su contador de sincronizaciones
                sawComponent.countSyncsInTable++;
                //Destruimos los que hayan llegado a su maximo de sincronizaciones
                if(sawComponent.countSyncsInTable >= sawComponent.maxSyncsInTable) {
                    Destroy(takenPositions[i]);
                    takenPositions[i] = null;
                }
            }
        }
    }

    void InstantiateTeleSync(GameObject syncToGenerate, List<int> freePositionsIndex) { 
        int firstRandomIndexPosition  = freePositionsIndex[Random.Range(0, freePositionsIndex.Count)]; //A random position of freePositionsIndex
        int secondRandomIndexPosition = firstRandomIndexPosition;

        while(firstRandomIndexPosition == secondRandomIndexPosition) {
            secondRandomIndexPosition = freePositionsIndex[Random.Range(0, freePositionsIndex.Count)];
        }

        //First TeleSync 
        float firstPositionToGenerate = defaultPositions[firstRandomIndexPosition];
        GameObject teleSync1Clone = Instantiate(syncToGenerate, new Vector3(0, firstPositionToGenerate, 0), Quaternion.identity);
        takenPositions[firstRandomIndexPosition] = teleSync1Clone; 

        //Second TeleSync
        float secondPositionToGenerate = defaultPositions[secondRandomIndexPosition];
        GameObject teleSync2Clone = Instantiate(syncToGenerate, new Vector3(0, secondPositionToGenerate, 0), Quaternion.identity);
        takenPositions[secondRandomIndexPosition] = teleSync2Clone; 

    }

    GameObject InstantiateNormalSync(GameObject syncToGenerate, List<int> freePositionsIndex) { 
        int randomIndexPosition  = freePositionsIndex[Random.Range(0, freePositionsIndex.Count)]; //A random position of freePositionsIndex
        float positionToGenerate = defaultPositions[randomIndexPosition]; 

        GameObject syncClone = Instantiate(syncToGenerate, new Vector3(0, positionToGenerate, 0), Quaternion.identity);

        takenPositions[randomIndexPosition] = syncClone; //Put the new position taken to true

        return syncClone;
    }

    GameObject getNextSyncToGenerate() {
        GameObject nextSyncToGenerate = null;

        //Temporarily it's random, in future will be with probabilities for each type of sync
        int num = Random.Range(0, syncs.Count);
        nextSyncToGenerate = syncs[num];

        return nextSyncToGenerate;
    }

    GameObject getNextSawToGenerate() {
        GameObject nextSawToGenerate = null;

        //Temporarily it's random, in future will be with probabilities for each type of sync
        int num = Random.Range(0, saws.Count);
        nextSawToGenerate = saws[num];

        return nextSawToGenerate;
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
