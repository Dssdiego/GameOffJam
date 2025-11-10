namespace GameOffJam;

// NOT WORKING YET!
public class DEPRECATED_CoroutineScheduler
{
    private readonly List<DEPRECATED_Coroutine> routines = new();
    
    private DEPRECATED_Coroutine? currentRoutine;
    private int currentIndex;
    
    private bool mExecuteInSequence = false;

    public void Add(in DEPRECATED_Coroutine deprecatedCoroutine)
    {
        routines.Add(deprecatedCoroutine);
    }

    public void Clear()
    {
        routines.Clear();
    }
    
    public void RunCurrent()
    {
        currentRoutine = routines[currentIndex];
    }

    public void Run(int index)
    {
        currentRoutine = routines[index];
        currentRoutine.Start();
    }

    /// <summary>
    /// Runs all coroutines in the list sequentially
    /// </summary>
    public void RunAll()
    {
        Run(0);
        mExecuteInSequence = true;
    }

    public void Run(string name)
    {
        foreach (var routine in routines.Where(t => t.Name == name))
        {
            currentRoutine = routine;
            break;
        }
    }

    /// <summary>
    /// Advance to the next coroutine in the list and run it
    /// </summary>
    public void Advance()
    {
        if (currentIndex >= routines.Count - 1)
        {
            return;
        }
        
        currentIndex++;
        RunCurrent();
    }
    
    public void Update()
    {
        if (currentRoutine == null)
        {
            return;
        }
        
        currentRoutine.Update();

        if (mExecuteInSequence && currentRoutine.Finished)
        {
            Advance();
        }
    }
}