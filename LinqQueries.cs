
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

    public IEnumerable<Books> LibrosAfter2000() {
        //Queri extension method
        //return librosCollections.Where( book => book.PublishedDate.Year > 2000 );

        //Queri expression
        return from book in librosCollections where book.PublishedDate.Year > 2000 select book;
    }

    public IEnumerable<Books> BooksPage() {
        //Query extension Method
        //return librosCollections.Where( book => book.PageCount > 250 && book.Title.Contains("in Action"));

        //Query expression
        return from book in librosCollections where book.PageCount > 250 && book.Title.Contains("in Action") select book;
    }

    public bool ThereAreAll() {
        return librosCollections.All( book => book?.Status != string.Empty );
    }

    public bool  ThereAreAny(int year) {
        return librosCollections.Any( book => book.PublishedDate.Year == year );
    }

    public IEnumerable<Books> AreCategory(string category) {
        return librosCollections.Where( book => book.Categories.Contains( category ) );
    }

    public IEnumerable<Books> OrderBy(string category) {
        var books = AreCategory( category );
        return books.OrderBy( book => book.Title);
    }

    public IEnumerable<Books> OrderBy2(string category) {
        return from book in librosCollections where book.Categories.Contains( category ) orderby book.Title, book.PageCount select book;
    }

    public IEnumerable<Books> Take(string category, int amount) {
        IEnumerable<Books> books = AreCategory( category ).OrderByDescending( book => book.PublishedDate );
        return books.Take( amount );

    }

    public IEnumerable<Books> Skip(int pages){
        return librosCollections.Where( book => book.PageCount > pages )
            .Skip( 2 )
            .Take( 2 );
    }

    public IEnumerable<Books> Select() {
        return librosCollections.Take( 3 )
                                .Select( book => new Books {
                                    Title = book.Title,
                                    PageCount = book.PageCount
                                });
    }
}