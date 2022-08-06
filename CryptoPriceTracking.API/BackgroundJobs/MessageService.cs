using System.Text.Json;
using CryptoPriceTracking.API.Services;
using CryptoPriceTracking.API.ViewModel;

namespace CryptoPriceTracking.API.BackgroundJobs;

public class MessageService
{
    private IServiceProvider _serviceProvider;

    public MessageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task GetCryptoInformation()
    {
        List<Crypto> cryptoList = new List<Crypto>();
        HttpClient client = new HttpClient();
        var response = await client.GetStringAsync("https://api.binance.com/api/v3/ticker?symbols=[\"AAVEUSDT\",\"ACHUSDT\",\"ACMUSDT\",\"ADAUSDT\",\"ADXUSDT\",\"AGLDUSDT\",\"AIONUSDT\",\"AKROUSDT\",\"ALCXUSDT\",\"ALGOUSDT\",\"ALICEUSDT\",\"ALPACAUSDT\",\"ALPHAUSDT\",\"AMPUSDT\",\"ANCUSDT\",\"ANKRUSDT\",\"ANTUSDT\",\"ANYUSDT\",\"API3USDT\",\"ARDRUSDT\",\"ARPAUSDT\",\"ARUSDT\",\"ASRUSDT\",\"ATAUSDT\",\"ATMUSDT\",\"ATOMUSDT\",\"AUDIOUSDT\",\"AUTOUSDT\",\"AVAUSDT\",\"AVAXUSDT\",\"AXSUSDT\",\"BADGERUSDT\",\"BAKEUSDT\",\"BALUSDT\",\"BANDUSDT\",\"BARUSDT\",\"BATUSDT\",\"BCCUSDT\",\"BCHUSDT\",\"BEAMUSDT\",\"BELUSDT\",\"BIFIUSDT\",\"BLZUSDT\",\"BNBUSDT\",\"BNTUSDT\",\"BNXUSDT\",\"BONDUSDT\",\"BTCSTUSDT\",\"BTCUSDT\",\"BTGUSDT\",\"BTSUSDT\",\"ERNUSDT\",\"ETCUSDT\",\"ETHUSDT\",\"EURUSDT\",\"FARMUSDT\",\"FETUSDT\",\"FIDAUSDT\",\"FILUSDT\",\"FIOUSDT\",\"FIROUSDT\",\"FISUSDT\",\"FLMUSDT\",\"FLOWUSDT\",\"FLUXUSDT\",\"FORTHUSDT\",\"FORUSDT\",\"FRONTUSDT\",\"FTMUSDT\",\"FTTUSDT\",\"FUNUSDT\",\"FXSUSDT\",\"GALAUSDT\",\"GALUSDT\",\"GBPUSDT\",\"GHSTUSDT\",\"GNOUSDT\",\"GRTUSDT\",\"GTCUSDT\",\"GTOUSDT\",\"GXSUSDT\",\"HARDUSDT\",\"HBARUSDT\",\"HCUSDT\"]&windowSize=1d");
        var cryptoConverted = JsonSerializer.Deserialize<List<CryptoVM>>(response);
        foreach (var item in cryptoConverted)
        {
            cryptoList.Add(new Crypto()
            {
                Symbol = item.symbol,
                LastPrice = item.lastPrice,
                PriceChangePercent = item.priceChangePercent,
                CloseDate = DateTimeOffset.FromUnixTimeMilliseconds(item.closeTime).UtcDateTime,
                Name = item.symbol.Split("USDT")[0].ToLower(),
            });
            
        }
        
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var _cryptoService = scope.ServiceProvider.GetRequiredService<ICryptoService>();
            await _cryptoService.SaveCryptos(cryptoList);
        }
        
    }
    
}