using System;

public static class UnitEvent
{
    public static event Action<UnitController> OnUnitDie;

    public static void UnitDie(UnitController unitController)
    {
        OnUnitDie?.Invoke(unitController);
    }
}
