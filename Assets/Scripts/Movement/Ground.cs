using System.Collections;
using UnityEngine;

public enum GroundType
{
    Asphalt,
    Grass,
    Metal
}

public class Ground : MonoBehaviour
{
    public GroundType groundType;
}
