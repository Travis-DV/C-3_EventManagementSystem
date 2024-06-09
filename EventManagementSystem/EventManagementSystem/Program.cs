/// <summary>
/// Has two modes 
///     1 for what was listed in the requirements where logs would print out immediately
///     2 for what I want where it would print them all at once onpon user request
///I did this because I want to make sure I still have the requirements but I did not want to deal with them while making the actual program
///I also feel like the user experiance is better
/// </summary>
namespace EventManagementSystem
{
    public class Program
    {

        /// <summary>
        /// Where the program starts sets up the very basics
        /// </summary>
        public static void Main()
        {
            EventManager eventManager = new EventManager();


#if BETTERLOGGING
            /// <summary>
            /// Sets up the better logger and event manager
            /// </summary>
            Logger logger = new Logger();
            eventManager.notifyEvent += logger.Log; //Doesn't need lambda? What did I do wrong?
            ProgramFR pr = new ProgramFR(eventManager, logger);

#elif !BETTERLOGGING
            /// <summary>
            /// Sets up the normal logger and event manager
            /// </summary>
            eventManager.notifyEvent += Logger.Log; //Doesn't need lambda? What did I do wrong?
            ProgramFR pr = new ProgramFR(eventManager);

#endif

            pr.Menu();
        }
    }

    public class ProgramFR
    {

        private EventManager eventManager;

#if BETTERLOGGING
        private Logger logger;

        /// <summary>
        /// Sets up the basics in this class
        /// </summary>
        /// <param name="eventManager">the basic event manager used for most of the logic for the program</param>
        /// <param name="logger">the logger set up to save it</param>
        public ProgramFR(EventManager eventManager, Logger logger)
        {
            this.eventManager = eventManager;
            this.logger = logger;
        }
#elif !BETTERLOGGING
        /// <summary>
        /// Sets up the basics in this class
        /// </summary>
        /// <param name="eventManager">the basic event manager used for most of the logic for the program</param>
        public ProgramFR(EventManager eventManager)
        {
            this.eventManager = eventManager;
        }
#endif

        /// <summary>
        /// The main menu where all the logic starts
        /// I decided that the console would clear all the time so that the user wouldn't get lost in typo's or past menu's
        /// </summary>
        public void Menu()
        {
            Console.Clear();
            Console.WriteLine(@"1. Add Event
2. List Events
3. Search Events
4. Exit"

#if BETTERLOGGING
/// <summary>
/// here so that the user can call to have all the logs printed
/// </summary>
+ "\n5. List Logs"
#endif
);

            ///<summary>
            /// Gets the user input on where in the menu they want to go
            /// </summary>
            string? key = Console.ReadLine();
            int num = -1;

            if (key == null || !int.TryParse(key, out num))
            {
                Menu();
            }

            switch (num)
            {
                /// <summary>
                /// if the user typed 1, they wanted to add an event
                /// </summary>
                case 1:
                    eventManager.AddEvent(this.MakeEvent());
                    Menu(); 
                    break;
                /// <summary>
                /// if the user typed 2, they wanted to read a list of all events
                /// </summary>
                case 2: 
                    this.ListEvents();
                    Menu(); 
                    break;
                /// <summary>
                /// if the user typed 1, they wanted to search their events
                /// </summary>
                case 3:
                    this.MakeSearch();
                    Menu(); 
                    break;
                /// <summary>
                /// if the user typed 1, they wanted to exit the app
                /// </summary>
                case 4:
                    Environment.Exit(0);
                    break;
#if BETTERLOGGING
                /// <summary>
                /// if the user typed 1, they wanted to read the logs
                /// </summary>
                case 5:
                    this.ListLogs();
                    Menu(); 
                    break;
#endif


            }

        }

        /// <summary>
        /// makes an event through user input
        /// </summary>
        /// <returns>a fully made event</returns>
        private Event MakeEvent()
        {

            #region Get Id
            /* OLD Users shouldn't be able to set id
            int Id;
            string? id = null;

            while (id == null || !int.TryParse(id, out Id))
            {
                Console.Clear();
                Console.Write("Input Event Id (int): ");
                id = Console.ReadLine();
            }
            */
            int Id = eventManager.Events.Count;

            #endregion

            #region Get Title
            string Title = CheckString("Input Event Title");
            #endregion

            #region Get Start Time
            /// <summary>
            /// Get whether the user wanted to set the start time themselves or not
            /// If they didnt set it to DateTime.Now
            /// if they did then let them, but dont let them past until they get one that formats correctly
            /// </summary>
            int CurrentOrCustom = OneOrTwo("Set Date Time to Current", "Custom Date Time");

            DateTime StartTime = DateTime.Now;
            switch (CurrentOrCustom)
            {
                case 1:
                    StartTime = DateTime.Now;
                    break;
                case 2:
                    string? startTime = null;

                    while (startTime == null || !DateTime.TryParse(startTime, out StartTime))
                    {
                        Console.Clear();
                        Console.Write("Give Date and Time (MM-DD-YYYY HH:MM:SS): ");
                        startTime = Console.ReadLine();
                    }
                    break; 
            }
            #endregion

            #region Get End Time
            int NullOrCustom = OneOrTwo("Set no End Time", "Custom Date Time");

            /// <summary>
            /// Get whether the user wanted to set the end time or not
            /// If they didnt set it to null
            /// if they did then let them, but dont let them past until they get one that formats correctly
            /// </summary>
            DateTime? EndTime = null;
            DateTime _EndTime = DateTime.Now;
            switch (NullOrCustom)
            {
                case 1:
                    EndTime = null;
                    break;
                case 2:
                    string? endTime = null;

                    while (endTime == null || !DateTime.TryParse(endTime, out _EndTime))
                    {
                        Console.Clear();
                        Console.Write("Give Date and Time (MM-DD-YYYY HH:MM:SS): ");
                        endTime = Console.ReadLine();
                    }

                    EndTime = _EndTime;
                    break;
            }
            #endregion

            #region Get Type
            EventType eventType = getType();
            #endregion

            /// <summary>
            /// actually make the event
            /// </summary>
            return new Event()
            {
                Id = Id,
                Title = Title,
                StartTime = StartTime,
                EndTime = EndTime,
                Type = eventType,
            };
        }

        /// <summary>
        /// Clear the console then print every event and hold it there until the user presses enter 
        /// </summary>
        private void ListEvents()
        {
            Console.Clear();
            eventManager.ListEvents();
            Console.ReadLine();
        }
        
        /// <summary>
        /// Find what the user wants to search then search it and print it out and hold that there until they hit enter
        /// </summary>
        private void MakeSearch()
        {
            int TypeOrTitle = OneOrTwo("Type", "Title", "Do you want to search by");

            string query = "";
            switch (TypeOrTitle)
            {
                case 1:
                    query = getType().ToString();
                    break;
                case 2:
                    query = CheckString("Input Event Title");
                    break;
            }

            eventManager.PrintEvents(eventManager.SearchEvents(query));
            Console.ReadLine();
        }

#if BETTERLOGGING
        /// <summary>
        /// clear console, print out every log and hold it there until they hit enter
        /// </summary>
        private void ListLogs()
        {
            Console.Clear();
            logger.ListLogs();
            Console.ReadLine();
        }
#endif
        
        /// <summary>
        /// genaric code for getting the user to specify what event type they want
        /// </summary>
        /// <returns>returns the event type the user wanted</returns>
        private EventType getType()
        {
            EventType eventType = EventType.Concert;
            int Index;
            string? index = null;

            while (index == null || !int.TryParse(index, out Index))
            {
                Console.Clear();
                Console.WriteLine(@"What type of event is it? 
1. Conference
2. Concert
3. Public Speech");
                index = Console.ReadLine();
            }

            switch (Index)
            {
                case 1:
                    eventType = EventType.Conference;
                    break;
                case 2:
                    eventType = EventType.Concert;
                    break;
                case 3:
                    eventType = EventType.PublicSpeech;
                    break;
            }

            return eventType;
        }

        /// <summary>
        /// Ganaric code for when the user has two opions and they need to choose 1 in int form
        /// </summary>
        /// <param name="message1">the first option the users have</param>
        /// <param name="message2">the second option the users have</param>
        /// <param name="message3">used once but was easyer to add then try and fix a clearing problem, used to specify what the user is choosing between</param>
        /// <returns>an int of 1 or 2</returns>
        private int OneOrTwo(string message1, string message2, string message3 = "")
        {
            string questionString = $@"1. {message1}
2. {message2}";
            if (message3 != "")
            {
                questionString = $@"{message3}
1. {message1}
2. {message2}";
            }
            
            string? input = null;
            int output;
            while (input == null || !int.TryParse(input, out output))
            {
                Console.Clear();
                Console.WriteLine(questionString);
                input = Console.ReadLine();
            }

            return output;
        }

        /// <summary>
        /// ganaric code to make sure the user inputed something into the read line before passing it anywhere
        /// </summary>
        /// <param name="message">what the code needs to ask the user</param>
        /// <returns>the string the user specified</returns>
        private string CheckString(string message)
        {
            string? CheckString = null;

            while (CheckString == null || !CheckString.Any(Char.IsLetterOrDigit))
            {
                Console.Clear();
                Console.Write($"{message} (string): ");
                CheckString = Console.ReadLine();
            }
            return CheckString;
        }
        
        /// <summary>
        /// Was requred, I have no idea what it would be used for 
        /// </summary>
        /// <param name="Id">the id of the event that should be pulled from the list</param>
        /// <returns>a tuple with everything from the event split out again why?</returns>
        public Tuple<int, string, string, DateTime, DateTime?> GetEventInfo(string Id)
        {
            int id;
            bool pass = int.TryParse(Id, out id);

            var eventToUpdate = eventManager.Events.FirstOrDefault(e => e.Id == id);
            if (eventToUpdate != null)
            {
                return new Tuple<int, string, string, DateTime, DateTime?>(eventToUpdate.Id, eventToUpdate.Title, eventToUpdate.Type.ToString(), eventToUpdate.StartTime, eventToUpdate.EndTime);
            }
            else
            {
                return new Tuple<int, string, string, DateTime, DateTime?>(-1, "INVALID ID", "ERROR", DateTime.Now, null);
            }
        }
    }
}
