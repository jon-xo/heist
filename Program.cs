using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{

    public class Recruit
    {
        public string Name { get; set; }

        public int SkillLevel { get; set; }

        public double Courage { get; set; }

        public Recruit() {}

        public Recruit( string name, int skill, double courage)
        {
            this.Name = name;
            this.SkillLevel = skill;
            this.Courage = courage; 
        }

        // private static string Concat(string v)
        // {
        //     throw new NotImplementedException();
        // }
    }
    class Program
    {
        // RecruitLoop method accepts a boolean as parameter and returns
        // true / false to the reference type to update the 
        // parameter value.
        static bool RecruitLoop(ref bool result)
        {
            Console.WriteLine("");
            Console.Write($"Add another team member [Y / N]:");
            string loopAnswer = Console.ReadLine().ToLower();
            if (loopAnswer == "y")
            {
                return result = true;
            }
            else
            {
                return result = false;                
            }
        }

        // MakeRecruit Method accepts a List of custom types as parameter,
        // Prompts user to enter in team member details,
        // Converts numeric values to int / double,
        // creates a new Recruit using values stored from Console.ReadLine()
        // and adds the object to the List passed as paramemter.
        
        static void MakeRecruit(List<Recruit> TeamTarget)
            {
                    
                bool continueLoop = true;
                
                // While loop continues until RecruitLoop method is called
                // once user enters in "N", cotinueLoop value is updated to false
                // and loop breaks.
                while (continueLoop == true) {
                    Console.WriteLine("");
                    Console.WriteLine("Add a team member: ");
                    Console.WriteLine("");
                    Console.Write("Name: ");
                    string inputName = Console.ReadLine();
                    Console.WriteLine("");
                    Console.Write("Skill Level [1-99]: ");
                    int inputSkill = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("");
                    Console.Write("Courage Level [1.0-10.0]: ");
                    double inputCourage = Convert.ToDouble(Console.ReadLine());

                    Recruit TeamMember = new Recruit (
                        inputName,
                        inputSkill,
                        inputCourage
                    );

                    TeamTarget.Add(TeamMember);

                    RecruitLoop(ref continueLoop);
                }
                    
            }

            // luckRate method is a randomizer which updates bankRating parameter
            // passed as reference-type variable.
            static int luckRate(ref int bankRating)
            {
                var randomizer = new Random();
                int luckScore = randomizer.Next(-10, 10);

                return bankRating + luckScore;
            }

            // 

            // SuccessRate method receives multiple parameters:
            // - bankRating = diffculty score of Bank Challenge
            // - TeamList = List of Heist Team Members
            // - dict = reference-type dictionary which is used to store heist pass/fail outcomes.
            static void SuccessRate(int bankRating, List<Recruit> TeamList, ref Dictionary<string, int> dict)
            {
                // variable stores the total skill level of all heist team members
                int totalTeamSkill = TeamList.Select(t => t.SkillLevel).Sum();

                // finalLuck variable stores the adjusted bankRating as calculated by luckRate randomizer
                int finalLuck = luckRate(ref bankRating);

                // User receives report with both values

                Console.WriteLine("            Heist Report:               ");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"    Total Team Skill: {totalTeamSkill} ");
                Console.WriteLine($"   Bank Diffculty Level: {finalLuck}   ");
                Console.WriteLine("");

                // if/else statement provides user indictation on sucessrate,
                // if team rating, adjusted for luck is greater than or equal to bankRating.

                // nested conditional for each outcome updates a dictionary value with number
                // of pass/fail attempts.
                
                if (finalLuck >= bankRating)
                {
                    Console.WriteLine("**Heist Outcome:** Success! 100%");
                    if(dict.ContainsKey("pass")){
                        dict["pass"] += 1;
                    }
                    else
                    {
                        dict.Add("pass", 1);
                    }
                }
                else
                {
                    Console.WriteLine("**Heist Failed:** Failed! 0%");
                    if(dict.ContainsKey("fail")){
                        dict["fail"] += 1;
                    }
                    else
                    {
                        dict.Add("fail", 1);
                    }
                }
            }

            // HeistScenarios method prompts user to submit the number of times
            // to simulate heist outcome. Condition checks to ensure user entry is in the correct range.
            // If value is outside of the range, user is prompted to re-enter value.
            // Method returns user enter value.
            static int HeistScenarios() {
                    
                    Console.WriteLine("");
                    Console.Write("Number of times to run propability scenario [1-10]:  ");
                    int scenarioNum = Convert.ToInt32(Console.ReadLine());

                    while (scenarioNum > 11 || scenarioNum <= 1)
                    {
                        Console.WriteLine("");
                        Console.Write("Number of times to run propability scenario [1-10]:  ");
                        scenarioNum = Convert.ToInt32(Console.ReadLine());
                    }
                    
                    return scenarioNum;
            }
            
            // BankChallenge method prompts the user to select a value between 1-100,
            // and returns value.
            static int BankChallenge()
            {
                Console.WriteLine("");
                Console.Write("Choose Bank diffculty level [1-100]: ");
                int bankRating = Convert.ToInt32(Console.ReadLine());
                return bankRating;
            }

            // HeistCalculator method accepts multiple values:
            // - loopNumber = number of times to run heist outcome
            // - bankLevel = value stored for Bank Difficulty
            // - TeamList = List of Team Member objects
            static void HeistCalculator(int loopNumber, int bankLevel, List<Recruit> TeamList)
            {
                // outcomeRate is declared as new dictionary to store outcome key and number of pass/fail attempts.
                Dictionary<string, int> outcomeRate = new Dictionary<string, int>();
                // for loop iterates loop the number of times declared in loopNumber
                for (int i = 0; i < loopNumber; i++)
                {
                    Console.WriteLine("");
                    // Scenario # is printed to console to identify attempt number
                    Console.WriteLine($"    Scenario # {i+1}    ");
                    Console.WriteLine("");
                    // SuccessRate method is and passed the outcomeRate dictionary
                    // as a reference-type.
                    SuccessRate(bankLevel, TeamList, ref outcomeRate);
                    // if statement prints divider for each iteration until the final loop.
                    if (i < loopNumber-1)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("----------------------------------------");
                        Console.WriteLine("----------------------------------------");
                    }
                }
                
                Console.WriteLine("");
                Console.WriteLine("Simulated Outcomes: ");
                Console.WriteLine("");
                // for each loop interates through the outcomeRate dictionary
                foreach (var rate in outcomeRate)
                {
                    // Switch statemet is declared for both outcomeRate keys
                    switch (rate.Key)
                    {
                        case "pass":
                            Console.Write("Successful Attempts: ");
                            break;
                        case "fail":
                            Console.Write("Failed Attempts:     ");
                            break;
                        default:
                            Console.WriteLine("");
                            break;
                    }
                    Console.Write($"{rate.Value}");
                    Console.WriteLine("");
        
                }
            }
        static void Main(string[] args)
        {
            // TeamHeist List is declared to store all user enter recruits
            List<Recruit> TeamHeist = new List<Recruit>();

            Console.WriteLine("Plan Your Heist!");
            Console.WriteLine("");

            // User entered BankChallenge is stored as variable
            int BankDifficultyLevel = BankChallenge();

            // MakeRecruit method is envoked and passed ther TeamHeist List
            MakeRecruit(TeamHeist);

            // User entered value from HeistScenarios method is stored in runCount variable
            int runCount = HeistScenarios();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("");

            // teamCount variable stores total number of objects contained in TeamHeist list
            int teamCount = TeamHeist.Count();
            
            
            Console.WriteLine($"** Total Team Members: {teamCount} **");
            Console.WriteLine("");

            // HeistCalulator method is called and passed required variables
            HeistCalculator(runCount, BankDifficultyLevel, TeamHeist);
            
        }
    }
}
