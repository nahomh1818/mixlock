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

// for(int i = 0; i < controls.Length; i++){
//     Console.WriteLine($"Session instance id: {AudioSessionHelpers.GetProcessName(controls[i])}");
// }

AudioSessionEvents events = new AudioSessionEvents();

IAudioSessionControl2 spotify;

for(int i = 0; i < controls.Length; i++)
{
    if (AudioSessionHelpers.GetProcessName(controls[i]).Equals("Spotify"))
    {
        spotify = controls[i];
        spotify.RegisterAudioSessionNotification(events);
        Console.WriteLine("Registered Spotify events.");
        float volume = AudioSessionHelpers.GetVolume(spotify);
        Console.WriteLine($"{AudioSessionHelpers.GetProcessName(spotify)} master volume: {volume * 100:0}%");
        Console.WriteLine("Setting Spotify master to 50%");
        AudioSessionHelpers.SetVolume(spotify, 0.5f);
        
    }
}

// Console.WriteLine("Listening...");
// Console.ReadLine();