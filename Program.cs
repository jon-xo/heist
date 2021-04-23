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
        static bool recruitLoop(ref bool result)
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
        static void MakeRecruit(int id, List<Recruit> TeamTarget)
            {
                // int loopPass = 1;                
                bool continueLoop = true;
                
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

                    string HeistMember = String.Concat("HeistMember", id);

                    Recruit TeamMember = new Recruit (
                        inputName,
                        inputSkill,
                        inputCourage
                    );

                    TeamTarget.Add(TeamMember);

                    // loopPass++;

                    recruitLoop(ref continueLoop);
                }
                    
            }

            static int luckRate(ref int bankRating)
            {
                var randomizer = new Random();
                int luckScore = randomizer.Next(-10, 10);

                return bankRating + luckScore;
            }
            static void successRate(int bankRating, List<Recruit> TeamList, ref Dictionary<string, int> dict)
            {
                int totalTeamSkill = TeamList.Select(t => t.SkillLevel).Sum();

                int finalLuck = luckRate(ref bankRating);

                Console.WriteLine("            Heist Report:               ");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"    Total Team Skill: {totalTeamSkill} ");
                Console.WriteLine($"   Bank Diffculty Level: {finalLuck}   ");
                Console.WriteLine("");

                if (finalLuck >= bankRating)
                {
                    Console.WriteLine("**Heist Success** 100% You GOT this!");
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
                    Console.WriteLine("**Heist Failed** You ain't got this!");
                    if(dict.ContainsKey("fail")){
                        dict["fail"] += 1;
                    }
                    else
                    {
                        dict.Add("fail", 1);
                    }
                }
            }

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
            
            static int BankChallenge()
            {
                Console.WriteLine("");
                Console.Write("Choose Bank diffculty level [1-100]: ");
                int bankRating = Convert.ToInt32(Console.ReadLine());
                return bankRating;
            }

            static void HeistCalculator(int loopNumber, int bankLevel, List<Recruit> TeamList)
            {
                Dictionary<string, int> outcomeRate = new Dictionary<string, int>();
                for (int i = 0; i < loopNumber; i++)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"    Scenario # {i+1}    ");
                    Console.WriteLine("");
                    successRate(bankLevel, TeamList, ref outcomeRate);
                    
                }
                
                Console.WriteLine("");
                Console.WriteLine("Simulated outcomes: ");
                foreach (var rate in outcomeRate)
                {
                    switch (rate.Key)
                    {
                        case "pass":
                            Console.Write("Successful Attempts: ");
                            break;
                        case "fail":
                            Console.Write("Failed Attempts: ");
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
            List<Recruit> TeamHeist = new List<Recruit>();

            
            Console.WriteLine("Plan Your Heist!");
            Console.WriteLine("");
            int BankDifficultyLevel = BankChallenge();

            MakeRecruit(1, TeamHeist);
            int runCount = HeistScenarios();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("");

            int teamCount = TeamHeist.Count();
            
            
            Console.WriteLine($"** Total Team Members: {teamCount} **");
            Console.WriteLine("");

            HeistCalculator(runCount, BankDifficultyLevel, TeamHeist);
            
        }
    }
}
