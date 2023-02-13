
LinqQueries queries = new LinqQueries();

//Todos los Libros
//ImprimirValores( queries.TodaLaColeccion() );

//Libros despues del 2000
//ImprimirValores( queries.LibrosAfter2000() );

//Libros con mas de 250 paginas y con el texto inclido In action
//ImprimirValores( queries.BooksPage() );

//if Status have ALL
//Console.WriteLine( queries.ThereAreAll() );
//if year is have on ANY
//Console.WriteLine( queries.ThereAreAny( 2045 ) );

//Return if match with Category
ImprimirValores( queries.AreCategory("Java") );

void ImprimirValores( IEnumerable<Books> listaLibros){
    Console.WriteLine( "{0, -70} {1, 15}   {2, 11}\n", "Titulo", "N. Paginas", "F. Publicacion" );

    foreach(var item in listaLibros){
        Console.WriteLine( "{0, -70} {1, 15}   {2, 11}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString() );
    }
}
