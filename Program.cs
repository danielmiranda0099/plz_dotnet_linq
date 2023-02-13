
LinqQueries queries = new LinqQueries();

ImprimirValores( queries.TodaLaColeccion() );

void ImprimirValores( IEnumerable<Books> listaLibros){
    Console.WriteLine( "{0, -70} {1, 15}   {2, 11}\n", "Titulo", "N. Paginas", "F. Publicacion" );

    foreach(var item in listaLibros){
        Console.WriteLine( "{0, -70} {1, 15}   {2, 11}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString() );
    }
}
