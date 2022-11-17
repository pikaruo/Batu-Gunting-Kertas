using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;

public class BotDifficultyManager : MonoBehaviour
{
    [SerializeField] Bot bot;
    [SerializeField] int selectedDiffculty;
    [SerializeField] BotStats[] botDifficulties;

    [Header("Remote Config Parameters:")]
    [SerializeField] bool enableRemotConfig = false;
    [SerializeField] string difficultyKey = "Difficulty";

    struct userAttribute { };
    struct appAttribute { };
    IEnumerator Start()
    {
        // tunggu bot selesai set up
        yield return new WaitUntil(() => bot.IsReady);

        // set stats default dari difficulty manager
        // sesuai selectedDifficulty dari inspector
        var newStats = botDifficulties[selectedDiffculty];
        bot.SetStats(newStats, true);

        // Ambil difficulty dari remote config kalau enabled
        if (enableRemotConfig == false)
        {
            yield break;
        }

        // tapi tunggu dulu sampe unity services siap
        yield return new WaitUntil(() => UnityServices.State == ServicesInitializationState.Initialized && AuthenticationService.Instance.IsSignedIn);

        // TODO daftar dulu untuk event fetch complete
        RemoteConfigService.Instance.FetchCompleted += OnRemoteConfigFetched;

        // TODO lalu fetch disini. cukup sekali diawal permainan
        RemoteConfigService.Instance.FetchConfigs(
            new userAttribute(), new appAttribute());
    }

    private void OnDestroy()
    {
        // TODO jangan lupa untuk unregister event untuk menghindari memory leak
        RemoteConfigService.Instance.FetchCompleted -= OnRemoteConfigFetched;
    }

    // TODO setiap kali data baru didapatkan (melalui fetch) fungsi ini akan dipanggil 2 refrence
    private void OnRemoteConfigFetched(ConfigResponse response)
    {
        if (RemoteConfigService.Instance.appConfig.HasKey(difficultyKey) == false)
        {
            Debug.LogWarning($"Diffculty Key {difficultyKey} not found on remote config server!");
            return;
        }

        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:
            case ConfigOrigin.Cached:
                break;
            case ConfigOrigin.Remote:
                selectedDiffculty = RemoteConfigService.Instance.appConfig.GetInt(difficultyKey);
                selectedDiffculty = Mathf.Clamp(selectedDiffculty, 0, botDifficulties.Length - 1);
                var newStats = botDifficulties[selectedDiffculty];
                bot.SetStats(newStats, true);
                break;
        }
    }
}