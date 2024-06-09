using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem
{
    public record Event
    {
        /// <summary>
        /// Would be easy to call as the coder to edit or search etc, on thing per event that users can't edit
        /// </summary>
        public int Id;
        /// <summary>
        /// Humans are good at words not numbers, hence a customisable title
        /// </summary>
        public string Title;
        /// <summary>
        /// Start and end time for scheduling reasons
        /// </summary>
        public DateTime StartTime;
        public DateTime? EndTime;
        /// <summary>
        /// Type for users to know what the event is at a glace
        /// </summary>
        public EventType Type;

        /// <summary>
        /// Overrides the ToString default method
        /// Used to format record pulling once, so it looks the same and is easy to do everywhere else in the program
        /// </summary>
        /// <returns>String for printing to console</returns>
        public override string ToString()
        {
            string? et = EndTime.ToString();
            if (EndTime == null)
            {
                et = "Not Set";
            }

            int typelength = Type.ToString().Length;
            int padleft = (int)Math.Floor((float)((12 - typelength) / 2)) + typelength;
            string tp = Type.ToString().PadLeft(padleft, ' ').PadRight(12, ' ');

            string id = Id.ToString().PadLeft(4, '0');

            return $"Id: ({id}), Type: ({tp}), Title: ({Title}), Start Time: ({StartTime}), End Time: ({et})";
        }
    }

    /// <summary>
    /// enum so the type doesnt have to be stored in a string and parsed or messed with
    /// </summary>
    public enum EventType 
    {
        Conference,
        Concert,
        PublicSpeech
    };
}
