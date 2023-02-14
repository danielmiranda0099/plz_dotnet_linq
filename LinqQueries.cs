
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

    public int CountAmoundPages() {
        //return librosCollections.Where( book => book.PageCount >= 200 && book.PageCount <= 500 ).Count();
        return librosCollections.Count( book => book.PageCount >= 200 && book.PageCount <= 500 );
    }

    //Operator Min
    public DateTime MinDatePublished() {
        return librosCollections.Min( book => book.PublishedDate );
    }

    //Operator MAX
    public int MaxAmoungPages() {
        return librosCollections.Max( book => book.PageCount );
    }

    //MIN BY
    public Books MinByAmoungPages() {
        return librosCollections.Where( book => book.PageCount > 0 ).MinBy( book => book.PageCount );
    }

    //MAX BY
    public Books MaxByDatePublished() {
        return librosCollections.MaxBy( book => book.PublishedDate );
    }

    //Operator SUM
    public int SumPages() {
        return librosCollections.Where( book => book.PageCount > 0 && book.PageCount < 500 )
                .Sum( book => book.PageCount );
    }

    //Opreator Agregate
    public string[] AgregateLastPublished() {
        return librosCollections.Where( book => book.PublishedDate.Year > 1990).Aggregate(
            new List<string>(), (Titulos, next) => {
                if(next.Title != string.Empty){
                    Titulos.Add(next.Title);
                    return Titulos;
                }
                return Titulos;
            }
        ).ToArray();
    }
    
    //Operator AVERAGE
    public double AverageTitleLength() {
        return librosCollections.Where( book => book.Title.Length > 0 )
            .Average( book => book.Title.Length );
    }

    //Operator GroupBy
    public IEnumerable<IGrouping<int, Books>> GroupByYear() {
        //return from b in books where b.PublishedDate.Year>=2000 group b by b.PublishedDate.Year;
        return librosCollections.Where( book => book.PublishedDate.Year >= 2000 )
        .GroupBy( book => book.PublishedDate.Year );
    }

    //Operator LookUp
    public ILookup<char, Books> LookUpTitleChar() {
        //return booksCollection.ToLookup(x => x.Title[0], x => x);
        return librosCollections.ToLookup( book => book.Title[0], book => book );
    }

    //JOIN
    public IEnumerable<Books> JoinBook() {
        IEnumerable<Books> pagesBook = librosCollections.Where( book => book.PageCount >= 500 );
        IEnumerable<Books> latesDate = librosCollections.Where( book => book.PublishedDate.Year > 2005 );

        return latesDate.Join( pagesBook, ld => ld.Title, pb => pb.Title,  
            (ld, pb) => ld
        );

        // return from booksAfter2005 in books 
        //          join booksPublishedAfter2005 in books
        //          on booksAfter2005.Title equals booksPublishedAfter2005.Title
        //          where booksAfter2005.PageCount >= 500 && booksPublishedAfter2005.PublishedDate.Year > 2005
        //          select booksAfter2005;
    }
}

