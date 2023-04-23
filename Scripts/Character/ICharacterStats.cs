using Unity.Netcode;

public interface ICharacterStats 
{
    NetworkVariable<int> CurrentHealth { get; set; }
    
    NetworkVariable<int> MaxHealth { get; set; }

    NetworkVariable<int> Damage { get; set; }

    NetworkVariable<int> Level { get; set; }

    NetworkVariable<int> MaxLevel { get; set; }

    NetworkVariable<int> CurrentExperience { get; set; }

    NetworkVariable<int> NeededExperienceToLevelUp { get; set; }

    NetworkVariable<float> Speed { get; set; }

}