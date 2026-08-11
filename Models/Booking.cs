using resisafe.Data;

namespace resisafe.Models;

public enum SlotType { FullDay, Daytime, Nighttime }
public enum BookingStatus { Pending, Confirmed, Completed, Cancelled }

public class Booking
{
    public int Id { get; set; }

    public int PropertyId { get; set; }
    public Property? Property { get; set; }

    public string GuestId { get; set; } = "";
    public ApplicationUser? Guest { get; set; }

    public DateTime CheckInDate { get; set; }
    public SlotType Slot { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    public decimal AmountHeld { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}