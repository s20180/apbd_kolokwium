namespace apbd_kolokwium.DTOs;

public class ClientDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public IEnumerable<SubscriptionDTO> Subscriptions { get; set; }
}

public class SubscriptionDTO
{
    public int IdSubscription { get; set; }
    public string Name { get; set; }
    public int TotalPaidAmount { get; set; }
}