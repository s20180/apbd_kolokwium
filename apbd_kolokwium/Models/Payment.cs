namespace apbd_kolokwium.Models;

public class Payment
{
    public int IdPayment { get; set; }
    public DateTime Date { get; set; }
    public int IdClient { get; set; }
    public int IdSubscription { get; set; }
    public virtual Client IdClientNavigation { get; set; }
    public virtual Subscription IdSubscriptionNavigation { get; set; }
}