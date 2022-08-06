using Microsoft.EntityFrameworkCore;
using CryptoPriceTracking.API.Hubs;
using CryptoPriceTracking.API.Persistence;
using Microsoft.AspNetCore.SignalR;

namespace CryptoPriceTracking.API.Services;

public class CryptoService : ICryptoService
{
    private readonly CryptoPriceTrackingContext _context;
    private readonly IHubContext<CryptoPriceTrackingHub> _hubContext;

    public CryptoService(CryptoPriceTrackingContext context, IHubContext<CryptoPriceTrackingHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task SaveCryptos(List<Crypto> cryptoList)
    {
        await _context.Cryptos.AddRangeAsync(cryptoList);
        await _context.SaveChangesAsync();
        await _hubContext.Clients.All.SendAsync("ReceiveCryptoPriceInformationList", await GetCryptoList());
    }

    public async Task<List<Crypto>> GetCryptoList()
    {
        return await _context.Cryptos.Where(i => i.CloseDate == _context.Cryptos.Max(x => x.CloseDate)).ToListAsync();
    }
}