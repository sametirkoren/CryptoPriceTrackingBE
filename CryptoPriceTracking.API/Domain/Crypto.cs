namespace CryptoPriceTracking.API;

public class Crypto
{
    public int Id { get; set; }
    
    public string Symbol { get; set; }
    
    public string PriceChangePercent { get; set; }
    
    public string LastPrice { get; set; }
    
    public DateTime CloseDate { get; set; }
    
    public string Name { get; set; }
}