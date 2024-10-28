namespace _72220577_FEUTS.Model
{
    public class course
    {
        [ConfigurationKeyName("courseId")]
        public int courseId { get; set; }
        public string name { get; set; }
        public string imageName { get; set; }
        public int duration { get; set; }
        public string description { get; set; }

        // Foreign Key to Categories
        public category category { get; set; }

    }
}
