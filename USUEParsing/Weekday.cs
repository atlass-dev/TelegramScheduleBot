namespace ScheduleParsing
{
    public class Weekday
    {
        public string date { get; set; }
        public string weekDay { get; set; }
        public int isCurrentDate { get; set; }
        public List<Pair> pairs { get; set; }
    }
}