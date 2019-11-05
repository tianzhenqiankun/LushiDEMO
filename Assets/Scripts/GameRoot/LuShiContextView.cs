using strange.extensions.context.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuShiContextView : ContextView {

    void Awake()
    {
        this.context = new LuShiContext(this);
    }
}
