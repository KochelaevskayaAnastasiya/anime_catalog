namespace anime_catalog
{
    public class Anime
    {
        public int id;
        public string name;
        public string description;
        public int year;
        public double score;
        public string author;
        public int count;
        public string[] genres;
        public string[] tags;
        public string age;

        public Anime(int id, string name, string description, int year, double score, string author,
            int count, string[] genres, string[] tags, string age)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.year = year;
            this.score = score;
            this.author = author;
            this.count = count;
            this.genres = genres;
            this.tags = tags;
            this.age = age;
        }
    }
}
