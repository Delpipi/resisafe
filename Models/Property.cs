using System.ComponentModel.DataAnnotations;
using resisafe.Data;

namespace resisafe.Models;

public class Property
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; } = "";

    [StringLength(1000)]
    public string Description { get; set; } = "";

    [Required, StringLength(100)]
    public string City { get; set; } = "";

    public decimal PricePerSlot { get; set; }

    public string? ImageUrl { get; set; }

    public int MaxOccupancy { get; set; }

    // Clé étrangère : à quel Owner appartient ce logement
    public string OwnerId { get; set; } = "";
    public ApplicationUser? Owner { get; set; }

    // Propriété de navigation : toutes les réservations liées
    public List<Booking> Bookings { get; set; } = new();
}