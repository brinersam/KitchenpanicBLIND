using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeUImngmt_helper : MonoBehaviour
{
    [SerializeField] private GameObject PlateUi;
    void Start()
    {
        RuntimeUImngmt.ReceivePlateUi = () => PlateUi;
    }
}
