using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
  // use this for initialization
  void start() {

  }

  //update is called once per frame
  void update() {
        transform.position += Time.deltaTime * transform.forward *2;
  }
}
