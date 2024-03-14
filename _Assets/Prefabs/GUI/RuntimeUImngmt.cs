using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RuntimeUImngmt // a way for various code to receive GO and such
{
    public static Func<GameObject> ReceivePlateUi = null;
}
