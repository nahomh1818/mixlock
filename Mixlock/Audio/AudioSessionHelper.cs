using MixLock;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Mixlock;

public class AudioSessionHelpers{
    public static IMMDevice GetDefaultPlaybackDevice(){
        IMMDeviceEnumerator enumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
        enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out IMMDevice device);

        return device;
    }

    public static IAudioSessionManager2 GetSessionManager(IMMDevice device){
        Guid sessionManagerGuid = typeof(IAudioSessionManager2).GUID;
        device.Activate(ref sessionManagerGuid, CLSCTX.CLSCTX_ALL, IntPtr.Zero, out object sessionManagerObj);
        IAudioSessionManager2 manager = (IAudioSessionManager2)sessionManagerObj;

        return manager;
    }

    public static IAudioSessionEnumerator GetSessionEnumerator(IAudioSessionManager2 manager){
        manager.GetSessionEnumerator(out IAudioSessionEnumerator sessionEnumerator);

        return sessionEnumerator;
    }

    public static IAudioSessionControl2 GetSessionControl(IAudioSessionEnumerator sessionEnumerator, int index){
        sessionEnumerator.GetSession(index, out IAudioSessionControl sessionObj);
        IAudioSessionControl2 session = (IAudioSessionControl2)sessionObj;

        return session;
    }

    public static String GetSessionIdentifier(IAudioSessionControl2 session){
        session.GetSessionIdentifier(out IntPtr identifierPtr);
        String sessionIdenifier = Marshal.PtrToStringUni(identifierPtr) ?? "";

        return sessionIdenifier;
    }

    public static String GetSessionInstanceIdentifier(IAudioSessionControl2 session){
        session.GetSessionInstanceIdentifier(out IntPtr identifierPtr);
        String instanceIdentifier = Marshal.PtrToStringUni(identifierPtr) ?? "";
        Marshal.FreeCoTaskMem(identifierPtr);

        return instanceIdentifier;
    }

    public static String GetDisplayName(IAudioSessionControl2 session){
        session.GetDisplayName(out IntPtr namePtr);

        return Marshal.PtrToStringUni(namePtr) ?? "";
    }

    public static String GetProcessName(IAudioSessionControl2 session){
        session.GetProcessId(out uint processId);
        Process process = Process.GetProcessById((int)processId);

        return process.ProcessName;
    }

    public static ISimpleAudioVolume GetSimpleAudioVolume(IAudioSessionControl2 session){
        return (ISimpleAudioVolume)session;
    }

    public static float GetVolume(IAudioSessionControl2 session){
        ISimpleAudioVolume volume = GetSimpleAudioVolume(session);

        volume.GetMasterVolume(out float level);

        return level;
    }

    public static void SetVolume(IAudioSessionControl2 session, float level){
        ISimpleAudioVolume volume = GetSimpleAudioVolume(session);

        Guid eventContext = Guid.NewGuid();

        volume.SetMasterVolume(level, ref eventContext);
    }
}