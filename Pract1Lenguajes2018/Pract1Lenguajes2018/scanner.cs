﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pract1Lenguajes2018
{
    class scanner
    {
        char caracter;
        int estadoActual = 0;
        int total = 0;
        int cantidad = 0;
        double porcentaje;
        String auxiliar = "";
      List<token> ListaT = new List<token>();
      List<error> ListaER = new List<error>();


        //Aquí está programado todo el autómata
        public void Analizador(string cadena)
        {
           
            for (int i = 0; i < cadena.Length; i++)
            {
                caracter = cadena[i];
                // Esto lo que hace es que separa la cadena que necesitás en caracteres
                switch (estadoActual)
                {
                    // Con esto te vas moviendo en el autómata que vos mismo pensás que funciona en el lenguaje
                    case 0:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 1;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);

                        }
                        else if (CaracterNumerico(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter;

                            Console.WriteLine("Así va la cadena ----> " + auxiliar);

                        }
                        else if ((Suma(caracter)) || (Resta(caracter)))
                        {
                            estadoActual = 2;
                            auxiliar += caracter;
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 6;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 0;
                        }

                        else
                        {
                            estadoActual = 0;
                            auxiliar += caracter;
                        }
                        break;

                    case 1:
                        if (CaracterMinuscula(caracter))
                        {
                            auxiliar += caracter;
                            estadoActual = 1;

                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        } else if ((Punto(caracter)) || (Coma(caracter)))
                        {
                            AgregarALista(auxiliar, "identificador");
                            auxiliar += caracter;
                            AgregarALista(auxiliar, "Signo_Puntuacion");

                        } else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            AgregarALista(auxiliar, "identificador");
                        } else if (CaracterNumerico(caracter))
                        {
                            estadoActual = 666;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 1;
                            auxiliar += caracter; 
                        }
                        break;
                    case 2:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter; 
                        } else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 2; 
                        } else
                        {
                            estadoActual = 777;
                            auxiliar += caracter; 
                        }
                        break;
                    case 3:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter; 
                        } else if(Punto(caracter)){
                            estadoActual = 4;
                            auxiliar += caracter; 

                        } else if (Coma(caracter))
                        {
                            AgregarALista(auxiliar, "Numero entero");
                            auxiliar += caracter;
                            AgregarALista(auxiliar, "Puntuacion");
                        } else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            AgregarALista(auxiliar, "Numero entero");
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter; 
                        }
                        break;
                    case 4:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 5;
                            auxiliar += caracter; 
                        } else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 4;
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter; 
                        }
                        break;
                    case 5:
                       if (CaracterNumerico(caracter))
                        {
                            estadoActual = 5;
                            auxiliar += caracter;

                        } else if ((Coma(caracter)) || (Punto(caracter)))
                        {
                            AgregarALista(auxiliar, "Numero decimal");
                            auxiliar += caracter;
                            AgregarALista(auxiliar, "Puntuacion");
                        } else if ((Espacio(caracter))||(Saltos(caracter)))
                        {
                            AgregarALista(auxiliar, "Numero decimal");
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter; 
                        }
                        break;
                    case 6:
                        // AQUÍ VAN LOS COMENTARIOS
                        if (Slash(caracter))
                        {
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        else if (Asterisco(caracter))
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);

                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 6;
                           
                        }
                        else
                        {
                            estadoActual = 888;
                            auxiliar += caracter;
                        }

                        break;
                    case 7:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)) || (CaracterNumerico(caracter)))
                        {
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        } else if(Espacio(caracter)){
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        else if (Saltos(caracter))
                        {
                            AgregarALista(auxiliar, "Comentario");
                        }
                        else
                        {
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        break; 
                    case 8:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)) || (CaracterNumerico(caracter)))
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                        }
                        else if (Saltos(caracter))
                        {
                            estadoActual = 8;
                        }
                        else if (Asterisco(caracter))
                        {
                            estadoActual = 9;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                        }
                        break; 
                    case 9:
                        if (Slash(caracter))
                        {
                            auxiliar += caracter;
                            AgregarALista(auxiliar, "ComentarioExt");
                        }
                        else
                        {
                            estadoActual = 999;
                            auxiliar += caracter;
                        }
                        break;
                    case 666:
                        //ERRORES DE IDENTIFICADORES
                        if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            AgregarAErrores(auxiliar, "IdentificadorInv");
                        }
                        else
                        {
                            estadoActual = 666;
                            auxiliar += caracter;
                        }
                        break;

                    case 777:
                        // ERROR DE NUMEROS
                        if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            AgregarAErrores(auxiliar, "NumeroInvalido");
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter;
                        }
                        break;
                    case 888:
                        // ERROR DE COMENTARIOS LINEALES
                        if (Saltos(caracter))
                        {
                            AgregarAErrores(auxiliar, "ComentarioINVALIDO");
                        }
                        else
                        {
                            estadoActual = 888;
                            auxiliar += caracter;
                        }
                        break; 
                    case 999:
                        // ERRORES DE COMENTARIOS EXTENSOS
                        if (Slash(caracter))
                        {
                            auxiliar += caracter;
                            AgregarAErrores(auxiliar, "ComentarioInval");
                        }
                        else
                        {
                            estadoActual = 999;
                            auxiliar += caracter;
                        }
                        break; 
                    
                }

            }
        }

        //Agregar a Lista de errores, limpiar y luego regresar al estado 0
        public void AgregarAErrores(string lexema, string tipo)
        {

            ListaER.Add(new error(lexema, tipo));
           
            auxiliar = "";
            estadoActual = 0;
        }

        //Agregar a Lista, limpiar y luego regresar al estado 0
        public void AgregarALista(string lexema, string tipo)
        {

            ListaT.Add(new token(lexema, tipo));
            auxiliar = "";
            estadoActual = 0;
            Console.WriteLine("Esto tiene la lista ---->"+ListaT.Count);
            
        }

        //BUSCADOR EN CADA PARTE DE LA LISTA PARA ENCONTRAR IGUALDADES

        public Boolean Similitud(string palabra)
        {
           
            for (int i = 0; i <= ListaT.Count - 1; i++)
            {
                if (palabra == ListaT[i].Lexema)
                {

                    cantidad = cantidad + 1;
                    auxiliar = "";
                    estadoActual = 0;
                    return true;



                }
                else
                {
                    auxiliar = "";
                    estadoActual = 0;
                }
            } return false; 
        }

        //BUSCADOR EN CADA PARTE DE LA LISTA PARA ENCONTRAR IGUALDADES

        public Boolean SimilitudErrores(string palabra)
        {
            
            for (int i = 0; i <= ListaER.Count - 1; i++)
            {
                if (palabra == ListaER[i].Lexema)
                {

                    cantidad = cantidad + 1;
                    auxiliar = "";
                    estadoActual = 0;
                    return true;
                    
                }
                else
                {
                    auxiliar = "";
                    estadoActual = 0;
                }
            } return false;
        }

        //GENERA UN HTML CON TODOS LOS ELEMENTOS GUARDADOS EN LAS LISTAS
        public void generarHTML()
        {
            Console.WriteLine("Cantidad en la lista" + ListaT.Count);
            StreamWriter html = new StreamWriter("salida.html");
            html.Write("<html>");
            html.Write("<head>");
            html.Write("Archivo de Salida, bienvenidos sean, UNIVERSIDAD DE SAN CARLOS DE GUATEMALA");
            html.Write("</head>");
            html.Write("<body>");
            html.Write("<p> Tabla de Tokens </p>");
            html.Write("<table>");
            html.Write("<tr>");
            html.Write("<th> Token </th>");
            html.Write("<th> Lexema </th>");
            html.Write("</tr>");
            for (int i = 0; i <= ListaT.Count -1; i++)
            {
                html.Write("<tr>");
                html.Write("<td>" + this.ListaT[i].Lexema + "</td>");

                html.Write("<td>" + this.ListaT[i].Tipo + "</td>");
                html.Write("</tr>");
            }
            html.WriteLine("</table>");
            html.Write("<p> Tabla de Errores </p>");
            html.Write("<table>");
            html.Write("<tr>");
            html.Write("<th> Token </th>");
            html.Write("<th> Lexema </th>");
            html.Write("</tr>");
            for (int j = 0; j <= ListaER.Count - 1; j++)
            {
                html.Write("<tr>");
                html.Write("<td>" + this.ListaER[j].Lexema + "</td>");

                html.Write("<td>" + this.ListaER[j].Tipo + "</td>");
                html.Write("</tr>");
            }
            html.WriteLine("</table>");
            html.Write("</body>");
            html.Write("</html>");
            html.Close();
            

        }



        //Analizar la cadena en búsqueda de plagio
        //INSERTE METODO AQUÍ
        public void Buscador(string cadena)
        {
            total = cadena.Length;
            for (int i = 0; i < cadena.Length; i++)
            {

                caracter = cadena[i];
                // Esto lo que hace es que separa la cadena que necesitás en caracteres
                switch (estadoActual)
                {
                    // Con esto te vas moviendo en el autómata que vos mismo pensás que funciona en el lenguaje
                    case 0:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)))
                        {
                            estadoActual = 1;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena ----> " + auxiliar);

                        }
                        else if (CaracterNumerico(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter;

                            Console.WriteLine("Así va la cadena ----> " + auxiliar);

                        }
                        else if ((Suma(caracter)) || (Resta(caracter)))
                        {
                            estadoActual = 2;
                            auxiliar += caracter;
                        }
                        else if (Slash(caracter))
                        {
                            estadoActual = 6;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 0;
                        }

                        else
                        {
                            estadoActual = 0;
                            auxiliar += caracter;
                        }
                        break;

                    case 1:
                        if (CaracterMinuscula(caracter))
                        {
                            auxiliar += caracter;
                            estadoActual = 1;

                            Console.WriteLine("Así va la cadena ----> " + auxiliar);
                        }
                        else if ((Punto(caracter)) || (Coma(caracter)))
                        {
                            Similitud(auxiliar);
                            porcentaje = cantidad / total;

                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            Similitud(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else if (CaracterNumerico(caracter))
                        {
                            estadoActual = 666;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 1;
                            auxiliar += caracter;
                        }
                        break;
                    case 2:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 2;
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter;
                        }
                        break;
                    case 3:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 3;
                            auxiliar += caracter;
                        }
                        else if (Punto(caracter))
                        {
                            estadoActual = 4;
                            auxiliar += caracter;

                        }
                        else if (Coma(caracter))
                        {

                            Similitud(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {

                            Similitud(auxiliar);
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter;
                        }
                        break;
                    case 4:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 5;
                            auxiliar += caracter;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            estadoActual = 4;
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter;
                        }
                        break;
                    case 5:
                        if (CaracterNumerico(caracter))
                        {
                            estadoActual = 5;
                            auxiliar += caracter;

                        }
                        else if ((Coma(caracter)) || (Punto(caracter)))
                        {

                            Similitud(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else if ((Espacio(caracter)) || (Saltos(caracter)))
                        {

                            Similitud(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter;
                        }
                        break;
                    case 6:
                        // AQUÍ VAN LOS COMENTARIOS
                        if (Slash(caracter))
                        {
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        else if (Asterisco(caracter))
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);

                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 6;

                        }
                        else
                        {
                            estadoActual = 888;
                            auxiliar += caracter;
                        }

                        break;
                    case 7:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)) || (CaracterNumerico(caracter)))
                        {
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        else if (Saltos(caracter))
                        {

                            Similitud(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else
                        {
                            estadoActual = 7;
                            auxiliar += caracter;
                            Console.WriteLine("Así va la cadena -->" + auxiliar);
                        }
                        break;
                    case 8:
                        if ((CaracterMayuscula(caracter)) || (CaracterMinuscula(caracter)) || (CaracterNumerico(caracter)))
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                        }
                        else if (Espacio(caracter))
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                        }
                        else if (Saltos(caracter))
                        {
                            estadoActual = 8;
                        }
                        else if (Asterisco(caracter))
                        {
                            estadoActual = 9;
                            auxiliar += caracter;
                        }
                        else
                        {
                            estadoActual = 8;
                            auxiliar += caracter;
                        }
                        break;
                    case 9:
                        if (Slash(caracter))
                        {
                            auxiliar += caracter;

                            Similitud(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else
                        {
                            estadoActual = 999;
                            auxiliar += caracter;
                        }
                        break;
                    case 666:
                        //ERRORES DE IDENTIFICADORES
                        if ((Espacio(caracter)) || (Saltos(caracter)))
                        {
                            SimilitudErrores(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else
                        {
                            estadoActual = 666;
                            auxiliar += caracter;
                        }
                        break;

                    case 777:
                        // ERROR DE NUMEROS
                        if ((Espacio(caracter)) || (Saltos(caracter)))
                        {

                            SimilitudErrores(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else
                        {
                            estadoActual = 777;
                            auxiliar += caracter;
                        }
                        break;
                    case 888:
                        // ERROR DE COMENTARIOS LINEALES
                        if (Saltos(caracter))
                        {

                            SimilitudErrores(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else
                        {
                            estadoActual = 888;
                            auxiliar += caracter;
                        }
                        break;
                    case 999:
                        // ERRORES DE COMENTARIOS EXTENSOS
                        if (Slash(caracter))
                        {
                            auxiliar += caracter;
                            SimilitudErrores(auxiliar);
                            porcentaje = cantidad / total;
                        }
                        else
                        {
                            estadoActual = 999;
                            auxiliar += caracter;
                        }
                        break;

                }

            }

        }

        public double RetornarTotal()
        {
            return total;
        }

        //Todos estos métodos son para reconocer un caracter en específico, valuándolos con su valor en ASCII
        public Boolean CaracterMinuscula(char valor)
        {
            if ((valor > 96) & (valor < 123))
            {
                return true;
            }
            else
                return false;
        }
        public Boolean CaracterMayuscula(char valor)
        {
            if ((valor > 64) & (valor < 123))
            {
                return true;
            }
            else
                return false;
        }

        public Boolean CaracterNumerico(char valor)
        {
            if ((valor > 47) & (valor < 58))
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Espacio(char valor)
        {
            if (valor == 32)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Saltos(char valor)
        {
            if (valor == 10)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean MenorQue(char valor)
        {
            if (valor == 60)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean MayorQue(char valor)
        {
            if (valor == 62)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Slash(char valor)
        {
            if (valor == 47)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Punto(char valor)
        {
            if (valor == 46)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Arroba(char valor)
        {
            if (valor == 64)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean CierraI(char valor)
        {
            if (valor == 63)
            {
                return true;
            }
            else
                return false;
        }
        public Boolean Coma(char valor)
        {
            if (valor == 44)
            {
                return true;
            }
            else
                return false; 
        }
        public Boolean SubRaya(char valor)
        {
            if (valor == 95)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Suma(char valor)
        {
            if (valor == 43)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Resta(char valor)
        {
            if (valor == 45)
            {
                return true;
            }
            else
                return false;
        }

        public Boolean Asterisco(char valor)
        {
            if (valor == 42)
            {
                return true;
            }
            else
                return false; 
        }
    }
}
