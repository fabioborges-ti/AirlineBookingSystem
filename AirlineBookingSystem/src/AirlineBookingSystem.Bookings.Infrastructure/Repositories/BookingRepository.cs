using AirlineBookingSystem.Bookings.Core.Entities;
using AirlineBookingSystem.Bookings.Core.Interfaces.Repositories;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace AirlineBookingSystem.Bookings.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly IDatabase _redisDatabase;

    private const string RedisKeyPrefix = "booking_";

    public BookingRepository(IConnectionMultiplexer redisConnection)
    {
        _redisDatabase = redisConnection.GetDatabase();
    }

    public async Task AddBookingAsync(Booking booking)
    {
        var data = JsonConvert.SerializeObject(booking);

        await _redisDatabase.StringSetAsync($"{RedisKeyPrefix}{booking.Id}", data);
    }

    public async Task<Booking?> GetBookingByIdAsync(Guid id)
    {
        var data = await _redisDatabase.StringGetAsync($"{RedisKeyPrefix}{id}");

        return string.IsNullOrEmpty(data) ? null : JsonConvert.DeserializeObject<Booking>(data!);
    }
}
