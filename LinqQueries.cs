
public class LinqQueries {
    private List<Books> librosCollections = new List<Books>();
    public LinqQueries() {
        using(StreamReader reader = new StreamReader("Books.json")){
            string json = reader.ReadToEnd();
            this.librosCollections = System.Text.Json.JsonSerializer.
                                        Deserialize<List<Books>>( json , 
                                            new System.Text.Json.JsonSerializerOptions() 
                                                {PropertyNameCaseInsensitive = true})
                                                ??
                                                Enumerable.Empty<Books>().ToList();
        }
    }

    public IEnumerable<Books> TodaLaColeccion() {
        return librosCollections;
    }
}