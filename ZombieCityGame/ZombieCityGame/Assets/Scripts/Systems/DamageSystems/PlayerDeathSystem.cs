using Leopotam.Ecs;
public class PlayerDeathSystem : IEcsRunSystem
{
    private EcsFilter<Player, DeathEvent> deadPlayers;
    private RuntimeData runtimeData;
    private UI ui;

    public void Run()
    {
        foreach (var i in deadPlayers)
        {
            ui.deathScreen.Show();
            runtimeData.gameOver = true;

            deadPlayers.GetEntity(i).Destroy();
        }
    }
}