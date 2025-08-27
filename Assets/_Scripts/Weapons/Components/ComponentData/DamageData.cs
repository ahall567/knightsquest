namespace KnightsQuest.Weapons.Components
{
    public class DamageData : ComponentData<AttackDamage>
    {
        public DamageData()
        {
            // Set Component Dependency to Damage
            ComponentDependency = typeof(Damage);
        }
    }
}