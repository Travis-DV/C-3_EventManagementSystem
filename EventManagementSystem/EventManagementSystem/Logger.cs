using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Has two modes 
///     1 for what was listed in the requirements where logs would print out immediately
///     2 for what I want where it would print them all at once onpon user request
///I did this because I want to make sure I still have the requirements but I did not want to deal with them while making the actual program
///I also feel like the user experiance is better
/// </summary>
namespace EventManagementSystem
{
#if !BETTERLOGGING
    internal static class Logger
    {
        /// <summary>
        /// Print a message to the console
        /// </summary>
        /// <param name="message">the message to be printed to console</param>
        public static void Log(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }

#elif BETTERLOGGING
    public class Logger
    {
        List<String> logs = new List<String>();

        /// <summary>
        /// Add a new message to the list of logs
        /// </summary>
        /// <param name="message">the message to add to the list</param>
        public void Log(string message)
        {
            logs.Add(message);
        }

        /// <summary>
        /// print every log all at once
        /// </summary>
        public void ListLogs()
        {
            logs.ForEach(l => Console.WriteLine(l.ToString()));
        }
    }
#endif
}





