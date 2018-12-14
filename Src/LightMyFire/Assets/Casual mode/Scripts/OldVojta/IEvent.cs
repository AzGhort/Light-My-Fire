using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent
{
    bool ShouldHappen(int location);
    void Invoke();
}
