using System;
using System.Collections.Generic;

namespace Capstone_2___Task_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\tWelcome To The Task Manager");


            List<CheckList> taskList = new List<CheckList>
            {
                new CheckList("Spongebob ", "make a bubbleman", DateTime.Parse("01/30/2020")),
                new CheckList("Patrick Star", "find his rock home", DateTime.Parse("01/31/2020")),
                new CheckList("Squidward","become a professional clarinet player", DateTime.Parse("03/24/2020")),
                new CheckList("Mrs. Puff", "finally teach spongebob how to drive", DateTime.Parse("03/1/2020")),
                new CheckList("Plankton", "steal the crabby patty recipe", DateTime.Parse("01/21/2021")),
            };

            SelectListOptions(taskList);
        }

        public static void SelectListOptions(List<CheckList> taskList)
        {
            int choice = 0;

            //chose this loop so that the user as long as they dont hit 5 to quit, then everything in the do will continue. 
            do
            {
                //avoid system break since user input should only be a number. 
                try
                {
                    DisplayMenu();
                    choice = int.Parse(CheckList.GetUserInput(CheckList.numberChoice));
                    Console.Clear();

                    switch (choice)
                    {
                        //used a switch statement since menu will be the same, it will be easier to access the menu through the switch statemnent.
                        case 1:
                            //created a seperate methods (below the main) for both case 1 and 2 to keep the switch statement a bit easier to read. 
                            //displays the list of characters above. 
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            DisplayAllTasks(taskList);
                            break;

                        case 2:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            AddToList(taskList);
                            break;

                        case 3:

                            if (taskList.Count < 1)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.WriteLine("There are no tasks in the list to delete!");
                            }
                            else
                            {

                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.WriteLine("\t\t\t\t\t\tDELETE A TASK");

                                Program.DisplayAllTasks(taskList);
                                int deleteTask = GetTaskIndex("\nWhat task would you like to delete? Enter a number: ", taskList);

                                //kept this in the loop so that before the user deletes something, they are asked if "they are sure to continue" for better user experience. 
                                Console.ForegroundColor = ConsoleColor.Red;
                                string userChoice = CheckList.GetUserInput("\nAre you sure you would like to delete this task? Enter y/n: ").ToLower();
                                if (userChoice == "y")
                                {
                                    //setting the property.
                                    taskList.RemoveAt(deleteTask - 1);
                                    Console.Clear();

                                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                    Console.WriteLine("\nYour selection has been REMOVED from your list!");

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    DisplayAllTasks(taskList);
                                }
                                else if (userChoice == "n")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nInvalid entry please enter y/n if you would like to delete your chosen task: ");
                                    continue;
                                }
                            }
                            break;

                        case 4:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            if (taskList.Count < 1)
                            {
                                Console.WriteLine("There are no tasks on the list for you to complete.");
                            }
                            else
                            {
                                Console.WriteLine("\t\t\t\t\t CHANGE COMPLETION STATUS");

                                DisplayAllTasks(taskList);
                                int completeTask = GetTaskIndex("\nWhich task have you completed? Please Enter a number: ", taskList);
                                Console.Clear();
                                Console.WriteLine("The STATUS of your selection is updated!");

                                //since completion was set to false in CheckList class, set it to true here to mark user's completion of task. 
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                taskList[completeTask - 1].Completion = true;
                                DisplayAllTasks(taskList);
                            }
                            break;

                        case 5:
                            Console.WriteLine("Thank you for playing! Have a great day!");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid input, please select a number listed in the display menu.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    //used a format exception to specify that only numbers were allowed inorder to access the checklist tasklist.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input. Please Try Again.");

                }
            } while (choice != 5);
        }
        public static void DisplayMenu()
        {
            //List task, add tasks, delete tasks, mark task complete, quit
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\nWhat would you like to do? ");
            Console.WriteLine("\n1. List All Tasks \n2. Add New Task \n3. Delete A Task \n4. Change Completion Status \n5. Quit ");

        }

        public static void DisplayAllTasks(List<CheckList> taskList)
        {
            Console.WriteLine("\n\t\t\t\t\tHere is your Task List!");
            //This will list out the full task list, and any adding/ deleting of tasks will be updated to the task list. 
            if (taskList.Count >= 1)
            {
                Console.WriteLine("\n   Member(s)\t\tDue Date(s) \t      Completed?\t\t      The Task(s)");
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                for (int i = 0; i < taskList.Count; i++)
                {
                    //I wanted only the date to be displayed and not the time, so I added the ToShortDateString
                    Console.WriteLine($"\n{i + 1}. {taskList[i].MemberName}\t  \t{taskList[i].DueDate.ToShortDateString()}\t\t{taskList[i].Completion}\t\t {taskList[i].Description}");
                }
            }
            else
            {
                //added this part in because in case if there are no task listed, the user will know that there are no tasks to be done. 
                Console.WriteLine("There are no tasks in the list.");
            }
        }

        public static void AddToList(List<CheckList> taskList)
        {
            Console.WriteLine("\n\t\t\tADD A NEW TASK\n");

            //specifically structured it b/c of capstone, but also easier to read added tasks.
            string teamMemberName = CheckList.GetUserInput("Team Member's name: ");
            string description = CheckList.GetUserInput("What's the Task: ").ToLower();
            DateTime dueDate = DateTime.Parse(CheckList.GetUserInput("When is the Due Date (mm/dd/yyyy): "));

            //Added this portion because I want to make sure that any new tasks added to the list are to be set in the future. 
            //It doesn't makes sense to create a new task and set the due date in the past. 
            if (dueDate >= DateTime.Now)
            {
                //executes and displays any added information to the 1.Lists.  
                taskList.Add(new CheckList(teamMemberName, description, dueDate));
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine("\nYour selection has been ADDED to the list!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Program.DisplayAllTasks(taskList);

            }
            else if (dueDate <= DateTime.Now)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThere is an ERROR with your date format!\n(Please make sure you are adding a future date for this task)");
            }
        }

        public static int GetTaskIndex(string message, List<CheckList> taskList)
        {
            //used -1 to counter any variances with counting each variable on this list and how we as people count.
            int index = -1;

            //again used a do while to only continue if user enters a number that is consistent with the amount
            //I used this method in case 3 to return the index.  
            do
            {
                try
                {
                    //picking something that is less than 0 wouldn't make sense and would crash the program 
                    //so I created this to make sure numbers selected are consistent to the tasklist.
                    index = int.Parse(CheckList.GetUserInput(message));
                    if (index > taskList.Count || index < 0)
                    {
                        Console.WriteLine("\nInvalid entry! Please choose a number that is on the list. ");
                    }
                }
                catch
                {
                    Console.WriteLine("\nInvalid entry! Please choose a number that is on the list. ");
                }
            }
            while (index > taskList.Count || index < 0);
            //used in case 3.
            return index;
        }
    }
}




