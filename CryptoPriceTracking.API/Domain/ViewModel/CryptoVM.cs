namespace CryptoPriceTracking.API.ViewModel;

public class CryptoVM
{
    public string symbol { get; set; }
    
    public string priceChangePercent { get; set; }
    
    public string lastPrice { get; set; }
    

    public long closeTime { get; set; }
}