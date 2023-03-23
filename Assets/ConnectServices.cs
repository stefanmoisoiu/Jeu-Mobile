using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;
public class ConnectServices : MonoBehaviour
{
    public static ConnectServices Instance { get; private set; }
    public static System.Action onConnected;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            ConnectToServices();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            onConnected?.Invoke();
            Destroy(gameObject);
        }
    }
    private async void ConnectToServices()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        onConnected?.Invoke();
    }
}
