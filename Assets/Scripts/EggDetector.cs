using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggDetector : MonoBehaviour
{
    Egg egg;

    private void OnTriggerEnter(Collider other) {
		egg = other.gameObject.GetComponent<Egg>();
    }

    private void OnTriggerExit(Collider other) {
        egg = null;
    }

    public Egg GetEgg(){
        return egg;
    }
}
