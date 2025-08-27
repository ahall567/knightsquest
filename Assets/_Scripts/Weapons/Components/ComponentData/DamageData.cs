namespace KnightsQuest.Weapons.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        protected override void SetComponentDependency()
        {
            // Set Component Dependency to Damage
            ComponentDependency = typeof(Damage);
        }
    }
}