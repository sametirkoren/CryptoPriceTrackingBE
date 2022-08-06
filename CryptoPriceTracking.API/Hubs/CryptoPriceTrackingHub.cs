using CryptoPriceTracking.API.Services;
using Microsoft.AspNetCore.SignalR;

namespace CryptoPriceTracking.API.Hubs;

public class CryptoPriceTrackingHub : Hub
{
    private readonly ICryptoService _cryptoService;

    public CryptoPriceTrackingHub(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    public async Task GetCryptoPriceInformationList()
    {
        await Clients.All.SendAsync("ReceiveCryptoPriceInformationList", await _cryptoService.GetCryptoList());
    }
}