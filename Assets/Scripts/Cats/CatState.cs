using UnityEngine;

public abstract class CatState {
    protected int happiness_change;
    protected int hunger_change;
    protected float fatigue_change;

    protected Animation animation;

    protected virtual void SetVariables(
        int happiness, int hunger, int fatigue) {

        this.happiness_change = happiness;
        this.hunger_change = hunger;
        this.fatigue_change = fatigue;
    }

    public virtual void Cycle(Cat cat) {
        throw new System.NotImplementedException();
    }

    protected virtual void SetAnimation(Animation animation) {
        this.animation = animation;
    }
}