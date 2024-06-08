using apbd_kolokwium.Context;
using apbd_kolokwium.DTOs;
using apbd_kolokwium.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apbd_kolokwium.Controllers;

[Route("api/")]
[ApiController]
public class SubscriptionController : ControllerBase
{
    private readonly AppDbContext _context;

    public SubscriptionController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("client/{idClient}")]
    public async Task<IActionResult> GetClientAsync(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.Payments)
            .ThenInclude(p => p.IdSubscriptionNavigation)
            .Select(c => new ClientDTO()
            {
                firstName = c.FirstName,
                lastName = c.LastName,
                email = c.Email,
                phone = c.Phone,
                Subscriptions = c.Payments.Select(p => new SubscriptionDTO()
                {
                    IdSubscription = p.IdSubscriptionNavigation.IdSubscription,
                    Name = p.IdSubscriptionNavigation.Name,
                    TotalPaidAmount = c.Payments.Sum(pay => pay.IdSubscriptionNavigation.Price)
                })
            })
            .FirstOrDefaultAsync();

        if (client is null) return NotFound("Podany klient nie istnieje");
        
        return Ok(client);
    }

    [HttpPut("payment")]
    public async Task<IActionResult> PaySubscriptionAsync(PaySubDTO paySubDto)
    {
        var client = await _context.Clients
            .FindAsync(paySubDto.IdClient);
        if (client is null) return Conflict("Podany klient nie istnieje");

        var subscription = await _context.Subscriptions
            .FindAsync(paySubDto.IdSubscription);

        if (subscription is null) return Conflict("Podana subskrypcja nie istnieje");

        if (subscription.EndTime < DateTime.Now) return Conflict("Podana subskrypcja jest nieaktywna");
        
        //TODO: walidacja subskrypcji za ten okres

        var subscriptionPrice = subscription.Price;
        var discounts = await _context.Discounts
            .Where(d => d.DateFrom < DateTime.Now
                        && d.DateTo > DateTime.Now
                        && d.IdSubscription == subscription.IdSubscription)
            .ToListAsync();

        if (discounts.Count != 0)
        {
            var discountValue = discounts.Max(d => d.Value);
            subscriptionPrice -= subscriptionPrice * (discountValue / 100);
        }
            
        if (subscriptionPrice != paySubDto.Payment) return Conflict("Podana kwota nie jest zgodna z ceną subskrypcji");

        var newPayment = new Payment()
        {
            Date = DateTime.Now,
            IdClient = client.IdClient,
            IdSubscription = subscription.IdSubscription
        };
        await _context.Payments.AddAsync(newPayment);
        await _context.SaveChangesAsync();
        
        return Ok(newPayment.IdPayment);
    }
}