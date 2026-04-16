using UnityEngine;

/// <summary>
/// For components that are affected by stat changes
/// </summary>
public interface IAffectedByStats
{
    public void HandleStatsChanged();
}
