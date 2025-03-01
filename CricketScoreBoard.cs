using CricketScoreboard;

public class CricketScoreBoard
{
    private PlayerTreeNode? playersRoot;
    private string battingTeam;
    private string bowlingTeam;
    private int firstInningRuns;
    private int firstInningWickets;
    private int secondInningRuns;
    private int secondInningWickets;

    public CricketScoreBoard()
    {
        playersRoot = null;
    }

    public void SetBattingAndBowlingTeams(string batting, string bowling)
    {
        battingTeam = batting;
        bowlingTeam = bowling;
    }
    private PlayerTreeNode InsertIntoTree(PlayerTreeNode? root, PlayerTreeNode newNode)
    {
        if (root == null)
        {
            return newNode;  
        }

        if (newNode.Player.Id < root.Player.Id)
        {
            root.Left = InsertIntoTree(root.Left, newNode);
        }
        else
        {
            root.Right = InsertIntoTree(root.Right, newNode);
        }

        return root;
    }
     
    public void AddPlayer(int id, string name, string team, string role, string inning)
    {
        var player = new CricketPlayer
        {
            Id = id,
            Name = name,
            Team = team
        };

        var newNode = new PlayerTreeNode(player);
        playersRoot = InsertIntoTree(playersRoot, newNode);   
         
        if (inning == "FirstInning")
        {
            if (role == "Batter" && team == battingTeam)
                player.IsBatterInFirstInning = true;
            else if (role == "Bowler" && team == bowlingTeam)
                player.IsBowlerInFirstInning = true;
        }
        else if (inning == "SecondInning")
        {
            if (role == "Batter" && team == bowlingTeam)
                player.IsBatterInSecondInning = true;
            else if (role == "Bowler" && team == battingTeam)
                player.IsBowlerInSecondInning = true;
        }
    }

    public void UpdateBattingStats(int id, string inning, int runs, int ballsFaced)
    {
        var current = playersRoot;
        while (current != null)
        {
            if (current.Player.Id == id)
            {
                current.Player.Runs += runs;
                current.Player.BallsFaced += ballsFaced;

                if (inning == "FirstInning")
                {
                    current.Player.IsBatterInFirstInning = true;
                    firstInningRuns += runs;
                }
                else
                {
                    current.Player.IsBatterInSecondInning = true;
                    secondInningRuns += runs;
                }

                return;
            }
            else if (id < current.Player.Id)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }
    }
     
    public void UpdateBowlingStats(int id, string inning, int wickets, int ballsBowled, int runsConceded)
    {
        var current = playersRoot;
        while (current != null)
        {
            if (current.Player.Id == id)
            {
                current.Player.Wickets += wickets;
                current.Player.BallsBowled += ballsBowled;
                current.Player.RunsConceded += runsConceded;

                if (inning == "FirstInning")
                {
                    current.Player.IsBowlerInFirstInning = true;
                    firstInningWickets += wickets;
                }
                else
                {
                    current.Player.IsBowlerInSecondInning = true;
                    secondInningWickets += wickets;
                }

                return;
            }
            else if (id < current.Player.Id)
            {
                current = current.Left;
            }
            else
            {
                current = current.Right;
            }
        }
    }
     
    private void InOrderTraversal(PlayerTreeNode? root, string team, string inning, string role)
    {
        if (root != null)
        {
            InOrderTraversal(root.Left, team, inning, role);

            var player = root.Player;

            if (player.Team == team &&
                ((role == "Batter" && ((inning == "FirstInning" && player.IsBatterInFirstInning) || (inning == "SecondInning" && player.IsBatterInSecondInning))) ||
                 (role == "Bowler" && ((inning == "FirstInning" && player.IsBowlerInFirstInning) || (inning == "SecondInning" && player.IsBowlerInSecondInning)))))
            {
                if (role == "Batter")
                {
                    Console.WriteLine($"ID: {player.Id}, Name: {player.Name}, Runs: {player.Runs}, Balls Faced: {player.BallsFaced}, Strike Rate: {player.StrikeRate:F2}");
                }
                else if (role == "Bowler")
                {
                    Console.WriteLine($"ID: {player.Id}, Name: {player.Name}, Wickets: {player.Wickets}, Balls Bowled: {player.BallsBowled}, Runs Conceded: {player.RunsConceded}, Economy: {player.EconomyRate:F2}");
                }
            }

            InOrderTraversal(root.Right, team, inning, role);
        }
    }
     
    public void DisplayScoreCard()
    {
        Console.WriteLine("\n=== Scorecard ===");

        Console.WriteLine($"\nFirst Innings: {battingTeam}");
        Console.WriteLine("--- Batting Stats ---");
        InOrderTraversal(playersRoot, battingTeam, "FirstInning", "Batter");

        Console.WriteLine("--- Bowling Stats ---");
        InOrderTraversal(playersRoot, bowlingTeam, "FirstInning", "Bowler");

        Console.WriteLine($"\nSecond Innings: {bowlingTeam}");
        Console.WriteLine("--- Batting Stats ---");
        InOrderTraversal(playersRoot, bowlingTeam, "SecondInning", "Batter");

        Console.WriteLine("--- Bowling Stats ---");
        InOrderTraversal(playersRoot, battingTeam, "SecondInning", "Bowler");
    }
     
    public void DisplayMatchSummary()
    {
        Console.WriteLine("\n=== Match Summary ===");
        Console.WriteLine($"{battingTeam} (First Innings): {firstInningRuns}/{firstInningWickets}");
        Console.WriteLine($"{bowlingTeam} (Second Innings): {secondInningRuns}/{secondInningWickets}");

        if (firstInningRuns > secondInningRuns)
        {
            int runDifference = firstInningRuns - secondInningRuns;
            Console.WriteLine($"{battingTeam} won the match by {runDifference} runs.");
        }
        else if (secondInningRuns > firstInningRuns)
        {
            int wicketDifference = 10 - secondInningWickets;
            Console.WriteLine($"{bowlingTeam} won the match by {wicketDifference} wickets.");
        }
        else
        {
            Console.WriteLine("The match is drawn.");
        }
    }
}
