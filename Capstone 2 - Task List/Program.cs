using System;
using System.Collections.Generic;

namespace Capstone_2___Task_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To The Task Manager");

            List<CheckList> taskList = new List<CheckList>
            {
                new CheckList("Spongebob ", "make a bubbleman", DateTime.Parse("1/30/2020")),
                new CheckList("Patrick Star", "find his rock home", DateTime.Parse("1/31/2020")),
                new CheckList("Squidward","become a professional clarinet player", DateTime.Parse("3/24/2020")),
                new CheckList("Mrs. Puff", "finally teach spongebob how to drive", DateTime.Parse("3/1/2020")),
                new CheckList("Plankton", "steal the crabby patty recipe", DateTime.Parse("1/21/2021")),
            };
           
            int choice = 0;

            //chose this loop so that the user as long as they dont hit 5 to quit, then everything in the do will continue. 
            do
            {
                //avoid system break since user input should only be a number. 
                try
                {
                    Program.DisplayMenu();
                    choice = int.Parse(CheckList.GetUserInput(CheckList.numberChoice));

                    switch (choice)
                    {
                        //used a switch statement since menu will be the same, it will be easier to access the menu through the switch statemnent.
                        case 1:
                            //created a seperate methods (below the main) for both case 1 and 2 to keep the switch statement a bit easier to read. 
                            //displays the list of characters above. 
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            ShowTasks(taskList);
                            break;

                        case 2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            AddToList(taskList);
                            break;

                        case 3:
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("DELETE TASK\n");

                            int DeleteIndex = GetTaskIndex("What task would you like to delete? Enter a number: ", taskList);

                            //kept this in the loop so that before the user deletes something, they are asked if "they are sure to continue" for better user experience. 
                            string userChoice = CheckList.GetUserInput("Are you sure you would like to delete this task? Enter y/n: ").ToLower();
                            if (userChoice == "y")
                            {
                                taskList.RemoveAt(DeleteIndex - 1);
                                ShowTasks(taskList);
                            }
                            else if (userChoice == "n")
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid entry please enter y/n if you would like to delete your chosen task: ");
                                continue;
                            }
                            break;

                        case 4:
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("TASK COMPLETION\n");

                            int completeTask = GetTaskIndex("Click on which number to see if the task is completed or not: ", taskList);

                            //since completion was set to false in CheckList class, set it to true here to mark user's completion of task. 
                            taskList[completeTask - 1].Completion = true;
                            ShowTasks(taskList);
                            break;

                        case 5:
                            Console.WriteLine("Thank you for playing! Have a great day!");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nInvalid input, please enter a number from the display");
                            break;
                    }
                }
                catch (FormatException)
                {
                    //used a format exception to specify that only numbers were allowed inorder to access the checklist tasklist.
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input, please enter  ONLY a number.");

                }
            } while (choice != 5);
        }

        public static void DisplayMenu()
        {
            //List task, add tasks, delete tasks, mark task complete, quit
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n1.List tasks \n2.Add tasks \n3.Delete Tasks \n4.Task completion \n5.Quit ");
        }

        public static void ShowTasks(List<CheckList> taskList)
        {
            //lists out taskList up top but any adding of tasks and deletion of task will be done because of this loop/
            if (taskList.Count >= 1)
            {
                for (int i = 0; i < taskList.Count; i++)
                {
                    Console.WriteLine($"\n{i + 1}. {taskList[i].TeamMemberName}\t{taskList[i].DueDate}\t{taskList[i].Completion}\t{taskList[i].Description}");
                }
            }
            else
            {
                //added this part in because in case if there are no task listed, the user will know that there are no tasks to be done. 
                Console.WriteLine("There are no tasks listed");
            }
        }

        public static void AddToList(List<CheckList> taskList)
        {
            Console.WriteLine("\nADD TASK\n");

            //specifically structured  it b/c of capstone but also easier to read added tasks.
            string teamMemberName = CheckList.GetUserInput("Team Member's name: ");
            string description = CheckList.GetUserInput("What's their Task: ");
            DateTime dueDate = DateTime.Parse(CheckList.GetUserInput("When is their Due Date? Enter mm/dd/yyyy: "));
            
            //executes and displays any added information to the 1.Lists.  
            taskList.Add(new CheckList(teamMemberName, description, dueDate));
        }

        public static int GetTaskIndex(string message, List<CheckList> taskList)
        {
            //used -1 to counter any variances with counting each variable on this list and how we as people count.
            int index = -1;

            //again used a do while to only continue if user enters a number that is consistent with the amount of
            //I used this method in case 3 to return the index.  
            do
            {
                try
                {
                    //pick something that is less than 0 wouldn't make sense and would crash the program so created this try catch to keep numbers that are consistent to the tasklist.
                    index = int.Parse(CheckList.GetUserInput(message));
                    if (index > taskList.Count || index < 0)
                    {
                        Console.WriteLine("Invalid entry! Please choose a number that is on the list. ");
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid entry! Please choose a number that is on the list. ");
                }
            }
            while (index > taskList.Count || index < 0);
            //used in case 3.
            return index;
        }
    }
}




