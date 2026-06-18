using System;
using System.Runtime.InteropServices;

namespace MixLock;

public enum EDataFlow
{
    eRender = 0,
    eCapture = 1,
    eAll = 2
}

public enum ERole
{
    eConsole = 0,
    eMultimedia = 1,
    eCommunications = 2
}

public enum CLSCTX : uint
{
    CLSCTX_ALL = 23
}

[ComImport]
[Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
public class MMDeviceEnumerator
{
}

[ComImport]
[Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMMDeviceEnumerator
{
    void EnumAudioEndpoints();

    void GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
}

[ComImport]
[Guid("D666063F-1587-4E43-81F1-B948E807363F")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IMMDevice
{
    void Activate(ref Guid iid, CLSCTX dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
}

[ComImport]
[Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAudioSessionManager2
{
    // From IAudioSessionManager
    void GetAudioSessionControl(IntPtr AudioSessionGuid, uint StreamFlags, out IntPtr SessionControl);

    void GetSimpleAudioVolume(IntPtr AudioSessionGuid, uint StreamFlags, out IntPtr AudioVolume);

    // From IAudioSessionManager2
    void GetSessionEnumerator(
        out IAudioSessionEnumerator SessionEnum
    );
}

[ComImport]
[Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAudioSessionEnumerator
{
    int GetCount();

    void GetSession(int SessionCount,out IAudioSessionControl Session);
}

[ComImport]
[Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAudioSessionControl
{
    void GetState(out int pRetVal);
    void GetDisplayName(out IntPtr pRetVal);
    void SetDisplayName(string value, Guid eventContext);
    void GetIconPath(out IntPtr pRetVal);
    void SetIconPath(string value, Guid eventContext);
    void GetGroupingParam(out Guid pRetVal);
    void SetGroupingParam(Guid value, Guid eventContext);
    void RegisterAudioSessionNotification(IntPtr NewNotifications);
    void UnregisterAudioSessionNotification(IntPtr NewNotifications);
}

[ComImport]
[Guid("BFB7FF88-7239-4FC9-8FA2-07C950BE9C6D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAudioSessionControl2
{
    void GetState(out int pRetVal);
    void GetDisplayName(out IntPtr pRetVal);
    void SetDisplayName(string value, Guid eventContext);
    void GetIconPath(out IntPtr pRetVal);
    void SetIconPath(string value, Guid eventContext);
    void GetGroupingParam(out Guid pRetVal);
    void SetGroupingParam(Guid value, Guid eventContext);
    void RegisterAudioSessionNotification(IAudioSessionEvents NewNotifications);
    void UnregisterAudioSessionNotification(IntPtr NewNotifications);
    void GetSessionIdentifier(out IntPtr pRetVal);
    void GetSessionInstanceIdentifier(out IntPtr pRetVal);
    void GetProcessId(out uint pRetVal);
    void IsSystemSoundsSession();
    void SetDuckingPreference(bool optOut);
}

[ComImport]
[Guid("24918ACC-64B3-37C1-8CA9-74A66E9957A8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IAudioSessionEvents
{
    void OnDisplayNameChanged(IntPtr NewDisplayName, ref Guid EventContext);
    void OnIconPathChanged(IntPtr NewIconPath, ref Guid EventContext);
    void OnSimpleVolumeChanged(float NewVolume, bool NewMute, ref Guid EventContext);
    void OnChannelVolumeChanged(uint ChannelCount, IntPtr NewChannelVolumeArray, uint ChangedChannel, ref Guid EventContext);
    void OnGroupingParamChanged(ref Guid NewGroupingParam, ref Guid EventContext);
    void OnStateChanged(int NewState);
    void OnSessionDisconnected(int DisconnectReason);
}

[ComImport]
[Guid("87CE5498-68D6-44E5-9215-6DA47EF883D8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ISimpleAudioVolume
{
    void SetMasterVolume(
        float fLevel,
        ref Guid EventContext
    );

    void GetMasterVolume(
        out float pfLevel
    );

    void SetMute(
        bool bMute,
        ref Guid EventContext
    );

    void GetMute(
        out bool pbMute
    );
}