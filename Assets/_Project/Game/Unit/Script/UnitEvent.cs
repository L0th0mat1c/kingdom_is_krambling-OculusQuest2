using System;

public static class UnitEvent
{
    public static event Action<BaseUnitController> OnUnitDie;

    public static void UnitDie(BaseUnitController unitController)
    {
        OnUnitDie?.Invoke(unitController);
    }
}
