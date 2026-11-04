using System;

using pk3DS.Core.Structures;

namespace pk3DS.Core.Randomizers;

public static class TrainerTiers
{
    public static readonly int[] BossClasses_USUM =
    [
        031, 049, 050, 051, 141, 164,   // Island Kahuna
        080, 107, 109, 110, 191,        // Elite Four
        076,                            // Team Skull Boss
        071, 082, 220,                  // Aether President
        183, 184,                       // Battle Legend
        198, 199, 200, 201, 202, 206,   // Rainbow Rocket Bosses
    ];

    public enum Tier
    {
        Regular,
        Important,
        Boss,
    }

    public static bool IsBoss(TrainerData7 trainer) =>
        trainer != null && Array.IndexOf(BossClasses_USUM, trainer.TrainerClass) >= 0;

    public static bool IsImportant(int index) =>
        Array.IndexOf(pk3DS.Core.Legal.ImportantTrainers_USUM, index) >= 0;

    public static bool IsBoss6(TrainerData6 trainer, bool oras) =>
        trainer != null && Array.IndexOf(oras ? pk3DS.Core.Legal.SpecialClasses_ORAS : pk3DS.Core.Legal.SpecialClasses_XY, trainer.Class) >= 0;

    public static Tier Of6(TrainerData6 trainer, int index, bool oras)
    {
        if (IsBoss6(trainer, oras)) return Tier.Boss;
        if (IsImportant(index)) return Tier.Important;
        return Tier.Regular;
    }

    public static Tier Of(TrainerData7 trainer, int index)
    {
        if (IsBoss(trainer)) return Tier.Boss;
        if (IsImportant(index)) return Tier.Important;
        return Tier.Regular;
    }

    public static T Pick<T>(TrainerData7 trainer, int index, T boss, T important, T regular) =>
        Of(trainer, index) switch
        {
            Tier.Boss => boss,
            Tier.Important => important,
            _ => regular,
        };
}
