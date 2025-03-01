namespace CricketScoreboard;
public class Program
{
    public static void Main(string[] args)
    {
        CricketScoreBoard scoreboard = new CricketScoreBoard();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n==== Cricket Scoreboard ====");
            Console.WriteLine("1. Set Batting and Bowling Teams");
            Console.WriteLine("2. Add Player");
            Console.WriteLine("3. Update Batting Stats");
            Console.WriteLine("4. Update Bowling Stats");
            Console.WriteLine("5. Display Match Summary");
            Console.WriteLine("6. Display Scorecard");
            Console.WriteLine("7. Exit");

            Console.Write("Choose an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter Batting Team: ");
                    string battingTeam = Console.ReadLine();
                    Console.Write("Enter Bowling Team: ");
                    string bowlingTeam = Console.ReadLine();
                    scoreboard.SetBattingAndBowlingTeams(battingTeam, bowlingTeam);
                    break;

                case 2:
                    Console.Write("Enter Player ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Player Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Team: ");
                    string team = Console.ReadLine();
                    Console.Write("Enter Role (Batter/Bowler): ");
                    string role = Console.ReadLine();
                    Console.Write("Enter Inning (FirstInning/SecondInning): ");
                    string inning = Console.ReadLine();
                    scoreboard.AddPlayer(id, name, team, role, inning);
                    break;

                case 3:
                    Console.Write("Enter Player ID: ");
                    id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Inning (FirstInning/SecondInning): ");
                    inning = Console.ReadLine();
                    Console.Write("Enter Runs Scored: ");
                    int runs = int.Parse(Console.ReadLine());
                    Console.Write("Enter Balls Faced: ");
                    int ballsFaced = int.Parse(Console.ReadLine());
                    scoreboard.UpdateBattingStats(id, inning, runs, ballsFaced);
                    break;

                case 4:
                    Console.Write("Enter Player ID: ");
                    id = int.Parse(Console.ReadLine());
                    Console.Write("Enter Inning (FirstInning/SecondInning): ");
                    inning = Console.ReadLine();
                    Console.Write("Enter Wickets Taken: ");
                    int wickets = int.Parse(Console.ReadLine());
                    Console.Write("Enter Balls Bowled: ");
                    int ballsBowled = int.Parse(Console.ReadLine());
                    Console.Write("Enter Runs Conceded: ");
                    int runsConceded = int.Parse(Console.ReadLine());
                    scoreboard.UpdateBowlingStats(id, inning, wickets, ballsBowled, runsConceded);
                    break;

                case 5:
                    scoreboard.DisplayMatchSummary();
                    break;

                case 6:
                    scoreboard.DisplayScoreCard();
                    break;

                case 7:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
