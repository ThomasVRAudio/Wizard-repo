using UnityEngine;

public interface ITargettable {
    Transform Target { get; }
    float StoppingDistance { get; }

    public GameObject GameObject {get;}
}
