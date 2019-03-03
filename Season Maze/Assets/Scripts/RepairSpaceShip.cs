using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSpaceShip : MonoBehaviour
{
    private GameObject SpaceShip;
    private int repairNumber = 0;

    void Awake()
    {
        ElementController.checkProcess.AddListener(RepairOnePiece);
        SpaceShip = this.gameObject;
    }

    void RepairOnePiece(char c)
    {
        if (repairNumber == 0)
        {
            Vector3 tempPosition = SpaceShip.transform.GetChild(0).transform.position;
            SpaceShip.transform.GetChild(0).SetPositionAndRotation(tempPosition, Quaternion.identity);
        }
        repairNumber++;
        Vector3 tempPosition2 = SpaceShip.transform.GetChild(repairNumber).transform.position;
        SpaceShip.transform.GetChild(repairNumber).SetPositionAndRotation(tempPosition2, Quaternion.identity);
    }
}
