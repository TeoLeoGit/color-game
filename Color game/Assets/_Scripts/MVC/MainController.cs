using System;

public class MainController
{
    public static event Action<SoundType> OnPlaySound;

    public static void PlaySound(SoundType soundType)
    {
        OnPlaySound?.Invoke(soundType);
    }
}
