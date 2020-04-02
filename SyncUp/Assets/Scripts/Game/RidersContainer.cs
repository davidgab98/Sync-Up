using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidersContainer : MonoBehaviour {

    float horizontalLimitLeft, horizontalLimitRight;

    Transform transfChild1, transfChild2;

    // Start is called before the first frame update
    void Start() {
        //Get the limits from de GameController
        horizontalLimitLeft = GameController.instance.horizontalLimitLeft;
        horizontalLimitRight = GameController.instance.horizontalLimitRight;

        //Get the child's transforms
        transfChild1 = transform.GetChild(0);
        transfChild2 = transform.GetChild(1);

        AssignOppositePositions();      //To childs 
    }


    void AssignOppositePositions() {
        //Assign randomly the position of child 1 and 2. One to center and the other one to the limit
        //This switch is a shit, change for something more eficient and elegant
        int num = Random.Range(0, 4);
        switch(num) {
            case 0:
                transfChild1.position = new Vector3(horizontalLimitLeft, transfChild1.position.y, transfChild1.position.z);
                break;
            case 1:
                transfChild1.position = new Vector3(horizontalLimitRight, transfChild1.position.y, transfChild1.position.z);
                break;
            case 2:
                transfChild2.position = new Vector3(horizontalLimitLeft, transfChild2.position.y, transfChild2.position.z);
                break;
            case 3:
                transfChild2.position = new Vector3(horizontalLimitRight, transfChild2.position.y, transfChild2.position.z);
                break;
        }
    }
}
