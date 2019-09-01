using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SpaceObject : MonoBehaviour
{ 
    public string IdTag;
    public float Speed;
    public float RotationSpeed;
    
    protected float[] Boundaries = {-10, 10, -10, 40};

   

    

    protected bool OutOfBoundaries()
    {
        return transform.position.x < Boundaries[0]
                  || transform.position.x > Boundaries[1]
                  || transform.position.y < Boundaries[2]
                  || transform.position.y > Boundaries[3];
           
    }


}
