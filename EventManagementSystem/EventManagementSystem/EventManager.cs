using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public class EventManager
    {
        /// <summary>
        /// For event handaling
        /// </summary>
        /// <param name="message">The message that needs to the logger</param>
        public delegate void EventNotification(string message); 
        public event EventNotification notifyEvent;

        /// <summary>
        /// List of events
        /// </summary>
        private List<Event> events = new();
        /// <summary>
        ///  Make it impossible to edit the list outside of this class
        /// </summary>
        public List<Event> Events { get { return events; } } 

        /// <summary>
        /// Add an event to the list and call the event hanaler
        /// </summary>
        /// <param name="e">The event that needs to be added to the list</param>
        public void AddEvent(Event e)
        {
            events.Add(e);
            notifyEvent?.Invoke($"Added item: {e}");
        }

        /// <summary>
        /// Idk why this is here, to make an event the user needs to set a start time so this could never be called
        /// this would set the start time to DateTime.Now for whatever event was specifid and call it to logging or throw an error
        /// </summary>
        /// <param name="eventId">The id of the event that needs to be started</param>
        public void StartEvent(int eventId)
        {
            var eventToUpdate = events.FirstOrDefault(e => e.Id == eventId);
            if (eventToUpdate != null)
            {
                DateTime time = DateTime.Now;
                eventToUpdate.StartTime = time;
                notifyEvent?.Invoke($"Started item: {eventToUpdate}; At {time}");
            }
            else if (eventToUpdate == null)
            {
                notifyEvent?.Invoke("ERROR! Id not found");
            }
        }
        
        /// <summary>
        /// Print every event in a list
        /// </summary>
        /// <param name="list">the list that needs to be printed</param>
        public void PrintEvents(List<Event> list) => list.ForEach(e => Console.WriteLine(e.ToString()));

        /// <summary>
        /// print everything in the main list
        /// </summary>
        public void ListEvents() => PrintEvents(events);

        /// <summary>
        /// Search by either type or title or thow an error based off a query found else were
        /// </summary>
        /// <param name="query">What to search for in the list</param>
        /// <returns>a list of every event that fits the query</returns>
        public List<Event> SearchEvents(string query)
        {

            EventType eventType;
            bool success = Enum.TryParse(query, out eventType);

            var filteredEvents = events.Where(e =>
                (success && e.Type == eventType) ||
                (!success && e.Title == query)
                ).ToList();

            if (success)
            {
                notifyEvent?.Invoke($"Searched by type");
            }
            else if (!success)
            {
                notifyEvent?.Invoke($"Searched by title");
            }

            return filteredEvents;
        }
    }
}
