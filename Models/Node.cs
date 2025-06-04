namespace GridHackConsoleApp.Models
{
    public class Node
    {
        public string Id { get; }
        public bool Failed { get; }
        public bool Isolated { get; set; }

        public Node(string id, bool failed)
        {
            Id = id;
            Failed = failed;
            Isolated = false;
        }
    }
}
