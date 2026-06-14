using Mixlock;
using MixLock;
using System.Diagnostics;
using System.Runtime.InteropServices;

IMMDevice device = AudioSessionHelpers.GetDefaultPlaybackDevice();
IAudioSessionManager2 sessionManager = AudioSessionHelpers.GetSessionManager(device);
IAudioSessionEnumerator sessionEnumerator = AudioSessionHelpers.GetSessionEnumerator(sessionManager);



Console.WriteLine($"Number of Sessions: {sessionEnumerator.GetCount()}");
IAudioSessionControl2[] controls = new IAudioSessionControl2[sessionEnumerator.GetCount()]; 

for(int i = 0; i < sessionEnumerator.GetCount(); i++){
    controls[i] = AudioSessionHelpers.GetSessionControl(sessionEnumerator, i);
}

for(int i = 0; i < controls.Length; i++){
    Console.WriteLine($"Session instance id: {AudioSessionHelpers.GetProcessName(controls[i])}");
}
