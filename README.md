# Assignment 1 : Event Management System

The first assignment for C# 3 independent study.

## Requirements

>### Step 1: Define the Event Class
>
> - [x] Create a new console application project named EventManagementSystem.
> - [x] Define the Event Record by creating a new file called Event.cs.
> - [x] Define a record named Event with properties for Id, Title, StartTime, EndTime, and Type. The EndTime should be a nullable DateTime to allow for events that have not yet been scheduled to end.
> - [x] Inside the Event.cs file, define an enum EventType with values Conference, Concert, and PublicSpeech.
>
> ### Step 2: Define the EventManager Class
>
> - [x] In a new file EventManager.cs, define a class EventManager.
> - [x] Inside the class, define a delegate EventNotification that takes a string message as a parameter.
> - [x] Add a `List<Event>` to store events.
> - [x] Create methods AddEvent(Event e) and StartEvent(int eventId) that use the list to manage events.
> - [x] In AddEvent, after adding, invoke the Notify event to log the addition.
> - [x] In StartEvent, search for the event and log its start; if not found, log an error.
> - [x] Implement a method called ListEvents() that prints all current events on the console. This method should neatly format the output to display relevant details such as Event ID, Title, Type, Start Time, and End Time (if available).
> - [x] Add a method SearchEvents(string query) to search events by title or type. Use LINQ to filter events based on the query string matching the title or type of the events.
>
> ### Part 3: Develop the Logger Utility
>
> - [x] Define a static class Logger in a new file Logger.cs.
> - [x] Add a static extension method Log to the EventNotification delegate, which outputs the message to the console.
>
> ### Part 4: Implement Main Application Logic
>
> - [x] In the Program.cs file, instantiate the EventManager and subscribe to its Notify event using a lambda expression that forwards messages to the Logger.Log method.
> - [x] Implement a method GetEventInfo that returns a tuple containing details about the next event. This method will be a practical application of using tuples to return multiple values.
> - [x] Enhance the menu in the Main() method to include options for adding events, listing events, and searching for events. Implement corresponding methods to handle user inputs for these operations.
> - [x] Prompt the user to enter details for a new event: title, type, startTime, and optional endTime. Validate user inputs, particularly ensuring the start time and end time are correctly formatted dates. Use DateTime.TryParse() for date inputs.
> - [x] Create a new Event instance using these details and call AddEvent().
> - [x] For listing events, simply call the ListEvents() method.
> - [x] To search, prompt the user for a search query and call the SearchEvents(string query) method. Display the results in a format similar to the list.
>
> ### Part 5: Documentation
>
> - [x] Add XML comments to all public members and methods, describing their purpose, parameters, and return types.
>
> Menu Example
>
> ``` text
>
> Event Management System 
> 
> 1. Add Event
> 2. List Events
> 3. Search Events
> 4. Exit
>
> ```
