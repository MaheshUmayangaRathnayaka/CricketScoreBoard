using CricketScoreboard;

public class PlayerTreeNode
{
    public CricketPlayer Player { get; set; }
    public PlayerTreeNode? Left { get; set; }
    public PlayerTreeNode? Right { get; set; }

    public PlayerTreeNode(CricketPlayer player)
    {
        Player = player;
        Left = null;
        Right = null;
    }
}
