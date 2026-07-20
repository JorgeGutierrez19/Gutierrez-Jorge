// See https://aka.ms/new-console-template for more information
using System.IO;
using System;
bool salir=false;
Random random = new Random();
int total = 0;
int validas = 0;
int invalidas = 0;

int visa = 0;
int mastercard = 0;
int amex = 0;
int discover = 0;
int desconocida = 0;
do{
String tarjeta;
Console.WriteLine("=== Validador de tarejetas ===");
Console.WriteLine("1 Validar una tarjeta ");
Console.WriteLine("2 Validar desde un archivo");
Console.WriteLine("3 Generar un numero valido");
Console.WriteLine("4 Estadisticas");
Console.WriteLine("5 Salir");
Console.WriteLine("Digite la opcion que desea realizar");
int op;
try
{
    op = int.Parse(Console.ReadLine());
}
catch
{
    Console.WriteLine("Debe ingresar un número del 1 al 5.");
    continue;
}
switch (op)
{
case 1: 
try{
Console.WriteLine("");
Console.WriteLine("=== Usted desea validar una tarjeta ===");
Console.Write("digite el numero de su tarjeta: ");
tarjeta=Console.ReadLine();
Console.Write("Estado: ");
ValidarLunh(tarjeta);
Console.Write("Marca: ");
Console.WriteLine(ValidarMarca(tarjeta));
ContarMarca(tarjeta);
Console.WriteLine("");
            }
            catch
            {
                Console.WriteLine("La tarjeta ingresada no es valida.");
            }

break;

case 2:
Console.WriteLine("=== Usted desea validar Tarjetas desde un documento de texto");
Console.WriteLine("Digite la ruta del documento de texto");
String ruta=Console.ReadLine();
ValidarTarjetasTXT(ruta);
break;

case 3:
Console.WriteLine("=== Tarjeta generada con éxito ===");

Console.WriteLine("Tarjeta generada: " + GenerarTarjeta());
break;

case 4: 
MostrarEstadisticas();
break;

case 5:
Console.WriteLine("Gracias, vuelva pronto");
salir = true;
break;

}


}while (!salir);


void ValidarLunh(String numero)
{
    int suma = 0;
    bool duplicar=false;
    for (int i = numero.Length - 1; i>= 0; i-- )
    {
        int digito = int.Parse(numero[i].ToString());
        if (duplicar == true)
        {
            digito *=2;
            if (digito>9)
            {
                digito-=9;
            }
        }
        suma+=digito;
        duplicar= !duplicar;


    }
    if (suma % 10 == 0)
    {
        Console.WriteLine("Tarjeta Valida");
        validas++;
        total++;
    }
    else
    {
        Console.WriteLine("Tarjeta Invalida");
        invalidas++;
        total++;
    }

}

string ValidarMarca(string numero)
{
    try
    {
        int longitud = numero.Length;
        int primerDigito = int.Parse(numero.Substring(0, 1));
        int dosDigitos = int.Parse(numero.Substring(0, 2));
        int tresDigitos = int.Parse(numero.Substring(0, 3));
        int cuatroDigitos = int.Parse(numero.Substring(0, 4));
        int seisDigitos = int.Parse(numero.Substring(0, 6));

        if (primerDigito == 4 && (longitud == 13 || longitud == 16))
            return "Visa";

        if ((dosDigitos >= 51 && dosDigitos <= 55) && longitud == 16)
            return "Mastercard";

        if ((cuatroDigitos >= 2221 && cuatroDigitos <= 2720) && longitud == 16)
            return "Mastercard";

        if ((dosDigitos == 34 || dosDigitos == 37) && longitud == 15)
            return "American Express";

        if ((cuatroDigitos == 6011 ||
            (seisDigitos >= 622126 && seisDigitos <= 622925) ||
            (tresDigitos >= 644 && tresDigitos <= 649) ||
            dosDigitos == 65) &&
            (longitud >= 16 && longitud <= 19))
            return "Discover";

        return "Desconocida";
    }
    catch
    {
        return "Desconocida";
    }
}
String GenerarTarjeta()
{
    string numeroR = "";


for (int i = 0; i < 15; i++)
{
    numeroR += random.Next(0, 10);
}
int suma = 0;
bool duplicar = true;

for (int i = numeroR.Length - 1; i >= 0; i--)
{
    int digito = int.Parse(numeroR[i].ToString());

    if (duplicar)
    {
        digito *= 2;

        if (digito > 9)
        {
            digito -= 9;
        }
    }

    suma += digito;
    duplicar = !duplicar;
}

int ultimoDigito = (10 - (suma % 10)) % 10;
numeroR += ultimoDigito;

return numeroR;

}

void ValidarTarjetasTXT(string ruta)
{
    try
    {
        StreamReader archivo = new StreamReader(ruta);

        string numero;

        while ((numero = archivo.ReadLine()) != null)
        {
            if (numero.Trim() == "")
                continue;

            Console.WriteLine("==============================");
            Console.WriteLine("Tarjeta: " + numero);
            Console.Write("Marca: ");
            Console.WriteLine(ValidarMarca(numero));
            ValidarLunh(numero);
            ContarMarca(numero);
        }

        archivo.Close();
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("El archivo no existe.");
    }
    catch (DirectoryNotFoundException)
    {
        Console.WriteLine("La carpeta no existe.");
    }
    catch
    {
        Console.WriteLine("Ocurrió un error al leer el archivo.");
    }
}


void MostrarEstadisticas()
{
    Console.WriteLine("========== ESTADÍSTICAS ==========");
    Console.WriteLine("Total de tarjetas: " + total);
    Console.WriteLine("Tarjetas válidas: " + validas);
    Console.WriteLine("Tarjetas inválidas: " + invalidas);
    Console.WriteLine();
    Console.WriteLine("Visa: " + visa);
    Console.WriteLine("Mastercard: " + mastercard);
    Console.WriteLine("American Express: " + amex);
    Console.WriteLine("Discover: " + discover);
    Console.WriteLine("Marca desconocida: " + desconocida);
    Console.WriteLine("");

}


void ContarMarca(string numero)
{
    string marca = ValidarMarca(numero);

    if (marca == "Visa")
    {
        visa++;
    }
    else if (marca == "Mastercard")
    {
        mastercard++;
    }
    else if (marca == "American Express")
    {
        amex++;
    }
    else if (marca == "Discover")
    {
        discover++;
    }
    else
    {
        desconocida++;
    }
}