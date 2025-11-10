namespace GameOffJam;

// NOT WORKING YET!

/// <summary>
/// Coroutine class to handle IEnumerator routines
/// Yielding 0 indicates the end of the coroutine
/// </summary>
public class DEPRECATED_Coroutine
{
    public string Name;
    public bool Finished;
    
    private IEnumerator<int> mRoutine;
    private bool mDebug = false;
    
    public DEPRECATED_Coroutine(string name, in IEnumerator<int> routine)
    {
        Name = name;
        
        mRoutine = routine;
    }

    public void Start()
    {
        Finished = false;
    }
    
    public void Update()
    {
        if (Finished)
        {
            return;
        }
        
        mRoutine.MoveNext();

        LogDbg("Running, yielded: " + mRoutine.Current);

        if (mRoutine.Current == 0)
        {
            Finished = true;

            LogDbg("Finished");
        }
    }
    
    private void LogDbg(string message)
    {
        if (mDebug)
        {
            Log.Info("[Coroutine " + Name + "] " + message);
        }
    }
}