using Leopotam.Ecs;
public interface IBonusHandler
{
    void ApplyBonus(EcsEntity playerEntity, ref BonusEffect effect);
}