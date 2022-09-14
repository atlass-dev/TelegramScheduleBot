namespace ScheduleParsing
{
    public class Pair
    {
        public int N { get; set; }
        public string time { get; set; }
        public int isCurrentPair { get; set; }
        public List<SchedulePair> schedulePairs { get; set; }
    }
}