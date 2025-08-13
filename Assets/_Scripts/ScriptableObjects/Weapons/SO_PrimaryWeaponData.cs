using UnityEngine;

[CreateAssetMenu(fileName ="newPrimaryWeaponData", menuName ="Data/Weapon Data/Primary Weapon")]
public class SO_PrimaryWeaponData : SO_WeaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;


    public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;

        movementSpeed = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}   
