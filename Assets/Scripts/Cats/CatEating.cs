﻿using UnityEngine;

class CatEating : CatState {
    public CatEating(RuntimeAnimatorController animator, string animation) {
        // TODO: Make eating vary depending on food
        this.SetVariables(0.02f, -0.30f, 0.03f);
        this.SetAnimator(animator);
        this.Animation = animation;
    }
}