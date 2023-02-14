
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
//ImprimirValores( queries.AreCategory("Java") );

//Return by category in order
//ImprimirValores( queries.OrderBy("Java") );

//ImprimirValores( queries.OrderBy2("Java") );


//Return by category order by Date and take amount
//ImprimirValores( queries.Take("Java", 3) );

//ImprimirValores( queries.Skip( 400 ) );

//ImprimirValores( queries.Select() );

//Operator of AGREGATION Count
//Console.WriteLine( queries.CountAmoundPages() );

//Operator Min
//Console.WriteLine( queries.MinDatePublished() );
//Operator MAX
//Console.WriteLine( queries.MaxAmoungPages() );
//Operator MinBy
//Console.WriteLine( queries.MinByAmoungPages().PageCount);
//Operator MaxBy
//Console.WriteLine( queries.MaxByDatePublished().PublishedDate);

//Operator Sum
//Console.WriteLine( queries.SumPages() );

//Operator Aggregate
// foreach(var item in queries.AgregateLastPublished()){
//     Console.WriteLine(item);
// }

//Operator Average
//Console.WriteLine( queries.AverageTitleLength() );

//ImprimirGrupo( queries.GroupByYear() );

//LOOKUP
// var filterBooks = queries.LookUpTitleChar();
// ImprimirDiccionario(filterBooks, 'S');

//JOIN
ImprimirValores( queries.JoinBook() );

void ImprimirValores( IEnumerable<Books> listaLibros){
    Console.WriteLine( "{0, -70} {1, 15}   {2, 11}\n", "Titulo", "N. Paginas", "F. Publicacion" );

    foreach(var item in listaLibros){ 
        Console.WriteLine( "{0, -70} {1, 15}   {2, 11}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString() );
    }
}

void ImprimirGrupo(IEnumerable<IGrouping<int,Books>> ListadeLibros)
{
    foreach(var grupo in ListadeLibros)
    {
        Console.WriteLine("");
        Console.WriteLine($"Grupo: { grupo.Key }");
        Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
        foreach(var item in grupo)
        {
            Console.WriteLine("{0,-60} {1, 15} {2, 15}",item.Title,item.PageCount,item.PublishedDate.Date.ToShortDateString()); 
        }
    }
}

void ImprimirDiccionario(ILookup<char, Books> ListadeLibros, char letra)
{
   Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
   foreach(var item in ListadeLibros[letra])
   {
         Console.WriteLine("{0,-60} {1, 15} {2, 15}",item.Title,item.PageCount,item.PublishedDate.Date.ToShortDateString()); 
   }
}
