using CryptoPriceTracking.API.ViewModel;

namespace CryptoPriceTracking.API.Services;

public interface ICryptoService
{
     Task SaveCryptos(List<Crypto> cryptos);

     Task<List<Crypto>> GetCryptoList();

}