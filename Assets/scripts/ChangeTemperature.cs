using UnityEngine;

public class ChangeTemperature : MonoBehaviour
{
    public enum TempSelection
    {
        Temp5, // Temp5
        Temp35, // Temp35
        Temp100  // Temp100
    }

    public TempSelection selectedOption; // Choose in Inspector

    void Awake()
    {
        // Convert selection to ObjectState
        Scripton.currentTempState = selectedOption switch
        {
            TempSelection.Temp5 => Scripton.ObjectState.Temp5,
            TempSelection.Temp35 => Scripton.ObjectState.Temp35,
            TempSelection.Temp100 => Scripton.ObjectState.Temp100,
            _ => Scripton.ObjectState.Temp5
        };

        StartCode.currentTempState = selectedOption switch
        {
            TempSelection.Temp5 => StartCode.ObjectState.Temp5,
            TempSelection.Temp35 => StartCode.ObjectState.Temp35,
            TempSelection.Temp100 => StartCode.ObjectState.Temp100,
            _ => StartCode.ObjectState.Temp5
        };

        ProgressBarTemp.currentTempState = selectedOption switch
        {
            TempSelection.Temp5 => ProgressBarTemp.ObjectState.Temp5,
            TempSelection.Temp35 => ProgressBarTemp.ObjectState.Temp35,
            TempSelection.Temp100 => ProgressBarTemp.ObjectState.Temp100,
            _ => ProgressBarTemp.ObjectState.Temp5
        };

         AttractAndAttach.currentTempState = selectedOption switch
        {
            TempSelection.Temp5 => AttractAndAttach.ObjectState.Temp5,
            TempSelection.Temp35 => AttractAndAttach.ObjectState.Temp35,
            TempSelection.Temp100 => AttractAndAttach.ObjectState.Temp100,
            _ => AttractAndAttach.ObjectState.Temp5
        };
        
    }
}
