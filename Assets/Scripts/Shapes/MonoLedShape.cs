using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class MonoLedShape : LedShape
{

    public override List<Vector3> GeneratePositions()
    {
        return new List<Vector3>() { this.transform.position };
    }

}