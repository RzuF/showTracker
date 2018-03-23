namespace showTracker.Model.API.Dto
{
    public class ShowDto
    {
        // Mapping
        public string Name { get; set; }
        public int Runtime { get; set; }

        public override string ToString()
        {
            return $"{Name} with {Runtime} minutes";
        }
    }
}
