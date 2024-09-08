using System.Data.Common;
using Doctorly.Data.Models;
using Doctorly.Data.Repository;
using Doctorly.Data.UseCases.Events;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.Tests.ServiceTests;

public class EventServiceTests
{
    private DoctorlyDbContext _db;

    public EventServiceTests()
    {
        var builder = new DbContextOptionsBuilder<DoctorlyDbContext>();
        builder.UseInMemoryDatabase("DoctorlyDbTest");
        _db = new DoctorlyDbContext(builder.Options);
    }

    ~EventServiceTests()
    {
        _db.Events = null;
        _db.Attendees = null;
        _db.Database.EnsureDeleted();
    }

    [Fact]
    public async Task WhenAddingEvent_Success()
    {
        // arrange
        var eventItem = new Event
        {
            Attendees = new List<Attendee>
            {
                new Attendee
                {
                    Accepted = false,
                    EmailAddress = "Test@mail.com",
                    FullName = "Test Gug"
                }
            },
            Title = "Some Title of an event+",
            Description = "Event of some sort at the Doc",
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(1),
        };
        var eventService = new EventService(_db);

        // act
        var result = await eventService.AddEventAsync(eventItem);

        // assert
        using (new AssertionScope())
        {
            result.Should().NotBeNull();
            result.Attendees.Should().NotBeNullOrEmpty();
            result.Id.Should().NotBe(Guid.Empty);
        }
    }

    [Fact]
    public void WhenAddingEvent_Fail()
    {
        // arrange
        var eventService = new EventService(_db);
        
        // act & assert
        var error =Assert.ThrowsAsync<Exception>(async () =>
        {
            _ = await eventService.AddEventAsync(null);
        });
        error.Should().NotBeNull();
    }

    [Fact]
    public async Task WhenUpdatingEvent_Success()
    {
        // arrange
        var eventId = Guid.NewGuid();
        var eventItem = new Event
        {
            Id = eventId,
            Attendees = new List<Attendee>
            {
                new Attendee
                {
                    Id = Guid.NewGuid(),
                    EventId = eventId,
                    Accepted = false,
                    EmailAddress = "Test@mail.com",
                    FullName = "Test Gug"
                }
            },
            Title = "Some Title of an event+",
            Description = "Event of some sort at the Doc",
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(1),
        };
        await _db.Events.AddAsync(eventItem);
        var eventService = new EventService(_db);

        // act
        var result = await eventService.UpdateEventAsync(eventItem);

        // assert
        using (new AssertionScope())
        {
            result.Should().NotBeNull();
            result.Attendees.Should().NotBeNullOrEmpty();
            result.Id.Should().Be(eventItem.Id);
        }
    }

    [Fact]
    public async Task WhenUpdateingEvent_Fail()
    {
        // arrange
        var eventId = Guid.NewGuid();
        var eventItem = new Event
        {
            Attendees = new List<Attendee>
            {
                new Attendee
                {
                    Id = Guid.NewGuid(),
                    EventId = eventId,
                    Accepted = false,
                    EmailAddress = "Test@mail.com",
                    FullName = "Test Gug"
                }
            },
            Title = "Some Title of an event+",
            Description = "Event of some sort at the Doc",
            StartTime = DateTimeOffset.Now,
            EndTime = DateTimeOffset.Now.AddHours(1),
        };
        var eventService = new EventService(_db);

        // act & assert
        var error = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            _ = await eventService.UpdateEventAsync(eventItem);
        });

        error.Should().NotBeNull();
    }

    [Fact]
    public void WhenDeletingEvent_Success()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public void WhenDeletingEvent_Fail()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public void WhenGettingEvent_Success()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public void WhenGettingEvent_Fail()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public void WhenSearchingEvents_Success()
    {
        // arrange

        // act

        // assert
    }

    [Fact]
    public void WhenSearchingEvents_Fail()
    {
        // arrange

        // act

        // assert
    }
}