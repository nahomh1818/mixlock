namespace MixLock;

public class AudioSessionEvents : IAudioSessionEvents
{
    public void OnDisplayNameChanged(IntPtr NewDisplayName, ref Guid EventContext)
    {
        Console.WriteLine("Display name changed");
    }

    public void OnIconPathChanged(IntPtr NewIconPath, ref Guid EventContext)
    {
        Console.WriteLine("Icon path changed");
    }

    public void OnSimpleVolumeChanged(float NewVolume, bool NewMute, ref Guid EventContext)
    {
        Console.WriteLine($"Volume changed: {NewVolume * 100:0}% Muted: {NewMute}");
    }

    public void OnChannelVolumeChanged(uint ChannelCount, IntPtr NewChannelVolumeArray, uint ChangedChannel, ref Guid EventContext)
    {
        Console.WriteLine("Channel volume changed");
    }

    public void OnGroupingParamChanged(ref Guid NewGroupingParam, ref Guid EventContext)
    {
        Console.WriteLine("Grouping changed");
    }

    public void OnStateChanged(int NewState)
    {
        Console.WriteLine($"State changed: {NewState}");
    }

    public void OnSessionDisconnected(int DisconnectReason)
    {
        Console.WriteLine($"Session disconnected: {DisconnectReason}");
    }
}