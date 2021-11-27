# WinSleepDelay
A simple tool to send your Windows computer to sleep with a delay.

---

To use simply run the exe and be prompted or use command-line to pass in arguments.

Arguments should be: `[seconds] [flags]`. For example: `30 -hfe`, `1 -f -e`, `60`.\
When using command-line you may pass in flags without a number: `.\WinSleepDelay.exe -e`. You will be prompted for seconds.

The core of this script is 
[SetSuspendState](https://docs.microsoft.com/en-us/windows/win32/api/powrprof/nf-powrprof-setsuspendstate, "Don't believe it's lies about force not working").
You can control the bools passed in with the following flags:

Flag | Description
---- | -----------
`-h` | Enter hibernation instead of sleep.
`-f` | Force suspend regardless of running applications.
`-e` | Allow Task Scheduler events to wake the computer.
