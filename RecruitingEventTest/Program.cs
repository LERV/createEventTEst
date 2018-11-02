using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalendarQuickstart
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string AppName = "RecruitingAppEvents";

        static void Main(string[] args)
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "1073272312523-j5gqimqrpkfq22cv6roej14h9ckef3a0.apps.googleusercontent.com",
                    ClientSecret = "vXBHOBqlMRCeSwIpZoQqdFiZ",
                },
                new[] { CalendarService.Scope.Calendar },
                "user",
                CancellationToken.None).Result;

            // Create the service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = AppName,
            });

            // Define parameters of request.
            //EventsResource.ListRequest request = service.Events.List("primary");
            //request.TimeMin = DateTime.Now;
            //request.ShowDeleted = false;
            //request.SingleEvents = true;
            //request.MaxResults = 10;
            //request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            //// List events.
            //Events events = request.Execute();
            //Console.WriteLine("Upcoming events:");
            //if (events.Items != null && events.Items.Count > 0)
            //{
            //    foreach (var eventItem in events.Items)
            //    {
            //        string when = eventItem.Start.DateTime.ToString();
            //        if (String.IsNullOrEmpty(when))
            //        {
            //            when = eventItem.Start.Date;
            //        }
            //        Console.WriteLine("{0} ({1})", eventItem.Summary, when);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No upcoming events found.");
            //}
            //Console.Read();


            Event newEvent = new Event()
            {
                Summary = "New Interview",
                Location = "Konrad Group CR",
                Description = "You have assigned a new interview",
                Start = new EventDateTime()
                {
                    DateTime = new DateTime(2018, 11, 02, 16, 28, 0),
                    TimeZone = "America/Costa_Rica",
                },
                End = new EventDateTime()
                {
                    DateTime = new DateTime(2018, 11, 02, 17, 28, 0),
                    TimeZone = "America/Costa_Rica",
                },
                //Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },  ///This is empty if you want a single event, a not repetitive
                //SendUpdates = { "all" },
                Attendees = new EventAttendee[] {
        new EventAttendee() { Email = "luisrova17@gmail.com" },
        new EventAttendee() { Email = "danielcastilloleiva.21@gmail.com" },
        new EventAttendee() { Email = "karizp14@gmail.com" },

    },
                Reminders = new Event.RemindersData()
                {
                    UseDefault = false,
                    Overrides = new EventReminder[] {
            new EventReminder() { Method = "email", Minutes = 0 },
            new EventReminder() { Method = "popup", Minutes = 0 },
        }
                }
            };

            String calendarId = "primary";
            //EventsResource.InsertRequest request = service.Events.Insert(newEvent, calendarId);
            //Event createdEvent = request.SendNotifications = true;
            //Event createdEvent = request.Execute();

            var recurringEvent = service.Events.Insert(newEvent, calendarId);
            recurringEvent.SendNotifications = true;
            recurringEvent.Execute();
            //Console.WriteLine("Event created: {0}", createdEvent.HtmlLink);

        }
    }
}