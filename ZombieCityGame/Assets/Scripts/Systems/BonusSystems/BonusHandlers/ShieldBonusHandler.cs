using Leopotam.Ecs;
using UnityEngine;
public class ShieldBonusHandler : IBonusHandler
{
    private EcsWorld ecsWorld;
    private UI ui;
    public ShieldBonusHandler(EcsWorld ecsWorld, UI ui)
    {
        this.ecsWorld = ecsWorld;
        this.ui = ui;
    }

    public void ApplyBonus(EcsEntity playerEntity, ref BonusEffect effect)
    {
        if (!playerEntity.Has<Shield>())
        {
            ref var player = ref playerEntity.Get<Player>();

            // Создаем объект щита
            GameObject shieldObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            shieldObject.transform.position = player.playerTransform.position;
            shieldObject.transform.localScale = new Vector3(8, 9, 8);

            // Настраиваем коллайдер
            Collider shieldCollider = shieldObject.GetComponent<Collider>();
            shieldCollider.isTrigger = false;

            // Создаем материал
            Material shieldMaterial = new Material(Shader.Find("Standard"));
            shieldMaterial.color = new Color(0, 0, 1, 0.5f);
            shieldMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            shieldMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            shieldMaterial.SetInt("_ZWrite", 0);
            shieldMaterial.DisableKeyword("_ALPHATEST_ON");
            shieldMaterial.EnableKeyword("_ALPHABLEND_ON");
            shieldMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            shieldMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

            // Назначаем материал щиту
            Renderer shieldRenderer = shieldObject.GetComponent<Renderer>();
            shieldRenderer.material = shieldMaterial;

            // Делаем щит дочерним объектом игрока
            shieldObject.transform.SetParent(player.playerTransform);

            shieldObject.AddComponent<ShieldController>();

            playerEntity.Get<Shield>() = new Shield
            {
                shieldObject = shieldObject,
                timer = 5f
            };

            effect.applied = true;

            playerEntity.Del<BonusEffect>();

            var shieldIconEntity = ecsWorld.NewEntity();
            ref var shieldIcon = ref shieldIconEntity.Get<ShieldIcon>();
            shieldIcon.iconObject = CreateIcon("ShieldIcon");
            shieldIcon.timer = 5f;
        }
    }

    private GameObject CreateIcon(string iconName)
    {
        var iconObject = ui.gameScreen.transform.Find(iconName)?.gameObject;
        if (iconObject != null)
        {
            iconObject.SetActive(true);
            return iconObject;
        }
        return null;
    }
}