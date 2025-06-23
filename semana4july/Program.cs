Console.WriteLine("2525 - ESTRUCTURA DE DATOS – UEA / SEMANA 04");

// Agenda de contactos
Agenda agenda = new Agenda(10); // Capacidad para 10 contactos

// Agregar contactos
agenda.AgregarContacto(new ContactoPersonal("David Jumbo", "0989776661", "Papá"));
agenda.AgregarContacto(new ContactoProfesional("Leonardo Navarro", "0991235667", "Compañero trabajo"));
agenda.AgregarContacto(new ContactoProfesional("Davicho Alvarez", "0998934560", "Jefe"));
agenda.AgregarContacto(new ContactoPersonal("Luz Calva", "0986778853", "Mamá"));

// Ordenar Agenda
agenda.OrdenarPorNombre();

// Mostrar todos los contactos
agenda.MostrarContactos();

// Buscar un contacto
Console.WriteLine("\nBuscar contacto por nombre:");
Console.Write("Ingrese nombre a buscar: ");
string nombre = Console.ReadLine();
agenda.BuscarContacto(nombre);
