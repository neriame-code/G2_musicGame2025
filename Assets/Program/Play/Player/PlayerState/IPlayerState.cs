public interface IPlayerState
{
    // ステートが変化したときに呼び出す
    public void EnterState();

    public void Update();

    // ステートを変化させるとき呼び出す
    public void ExitState();
}
