class CatEating : CatState {
    public CatEating() {
        // Make eating vary depending on food
        this.SetVariables(0.02f, -0.30f, 0.03f);
    }
}