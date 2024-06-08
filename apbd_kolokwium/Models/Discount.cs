namespace apbd_kolokwium.Models;

public class Discount
{
    public int IdDiscount { get; set; }
    public int Value { get; set; }
    public int IdSubscription { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public virtual Subscription IdSubscriptionNavigation { get; set; }
}