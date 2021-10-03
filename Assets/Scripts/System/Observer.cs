using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Observer 
{
    public delegate void GameEvent();
    public static GameEvent Forward; 
}
