using System.Runtime.InteropServices;

[DllImport("Powrprof.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvents);

bool _hibernate = false, _forceSuspend = false, _enableWakeEvents = false;
int? _seconds = null;

if (args.Length > 0)
    ArgsHandler(args);

if (_seconds is null)
{
    Console.Write("Seconds to wait before sleeping: ");
    string userInput = Console.ReadLine() ?? "";
    ArgsHandler(userInput.Split(" "), true);
}

for (int i = 0; i < _seconds; i++)
{
    Console.Write("\rSleeping computer in {0} seconds...", _seconds - i);
    Thread.Sleep(1000);
}
Console.Write("\rSleeping computer... Goodnight!");
SetSuspendState(_hibernate, _forceSuspend, !_enableWakeEvents);

void ArgsHandler(string[] argsArray, bool requireSeconds = false)
{
    if (requireSeconds) _seconds = GetRequiredSeconds(argsArray[0]);
    else _seconds = ToInt(argsArray[0]);

    if (_seconds is null || argsArray.Length > 1)
    {
        string flagString = ""; // Support -h -f -d and -hfd
        foreach (string flag in _seconds.HasValue ? argsArray.Skip(1) : argsArray)
        {
            if (!flag.StartsWith("-"))
                throw new ArgumentException($"Invalid argument: {flag}");
            flagString += flag[1..];
        }
        string[] activeFlags = new string[flagString.Length];
        foreach (char flagChar in flagString.ToCharArray())
            activeFlags[flagString.IndexOf(flagChar)] = SetFlag(flagChar);
        Console.WriteLine("Runnings with flags: " + string.Join(", ", activeFlags));
    }
}

string SetFlag(char arg)
{
    switch (arg)
    {
        case 'h': _hibernate = true; return "Hibernate";               // Enter hibernation instead of sleep
        case 'f': _forceSuspend = true; return "Force suspend";        // Force suspend regardless of running applications
        case 'e': _enableWakeEvents = true; return "Wake with events"; // Allow Task Scheduler to wake computer
        default: throw new ArgumentException($"Invalid argument: {arg}");
    }
}

int GetRequiredSeconds(string consoleInput)
{
    if (consoleInput == "")
        return 30;
    int? consoleSeconds = ToInt(consoleInput);
    if (consoleSeconds is null)
        throw new ArgumentException($"Invalid argument: {consoleInput}");
    return consoleSeconds.Value;
}

int? ToInt(string? intString) => int.TryParse(intString, out int i) ? i : null;
