using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone_2___Task_List
{
    class CheckList
    {
        public const string numberChoice = "\nWhat would you like to do? Please Enter a number: "; //allows the user to only pick numbers
        public const string addTask = "ADD TASK";
        //fields
        private string teamMemberName;
        private string description;
        private DateTime dueDate;
        private bool completion;

        public string TeamMemberName
        {
            get { return teamMemberName; }
            set { teamMemberName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime DueDate
        {
            get { return dueDate; }
            set { dueDate = value; }
        }

        public bool Completion
        {
            get { return completion; }
            set { completion = value; }
        }

        public CheckList()
        {

        }

        public CheckList(string _teamMemberName, string _description, DateTime _dueDate) //took out bool completion because creating a seperate method. 
        {
            teamMemberName = _teamMemberName;
            description = _description;
            dueDate = _dueDate;
            completion = false;
        }

        public static string GetUserInput(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
        public override string ToString()
        {
            return teamMemberName + "\t" + description + "\t" + dueDate + "\t" + completion;
        }
    }






}
