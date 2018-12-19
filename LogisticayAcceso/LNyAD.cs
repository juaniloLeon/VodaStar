using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticayAcceso.vodastarDataSetTableAdapters;
using System.Text.RegularExpressions;
using LogisticayAcceso.Entidades;


namespace LogisticayAcceso
{
    public class LNyAD
    {
        static ClientesTableAdapter clientesAdapter = new ClientesTableAdapter();
        static vodastarDataSet.ClientesDataTable clientesTabla = new vodastarDataSet.ClientesDataTable();

        static TarifasTableAdapter tarifaAdaptar = new TarifasTableAdapter();
        static vodastarDataSet.TarifasDataTable tarifaTabla = new vodastarDataSet.TarifasDataTable();

        static ConsumosTableAdapter consumosAdapter = new ConsumosTableAdapter();
        static vodastarDataSet.ConsumosDataTable consumosTabla = new vodastarDataSet.ConsumosDataTable();

        public static Cliente ObtenClienteVerificado(string user)
        {
            int id = BuscaUsuarioLogin(user);
            Cliente c=ObtenClientePorID(id);

            return c;

        }


        public static int Acceso(string user, string pass)
        {
            int status;

            int idUser = BuscaUsuarioLogin(user);

            if (idUser == -1)//usuario no existe
                return -1;
            else
            {
                status = CompruebaClave(idUser, pass);
            }

            return status;
        }

        private static int CompruebaClave(int idUser, string pass)
        {
            clientesTabla = clientesAdapter.GetClientByID(idUser);

            if (clientesTabla[0].Clave.Equals(pass))
            {
                return clientesTabla[0].Tipo;
            }
            else
            {
                return -1;
            }

        }

        private static int BuscaUsuarioLogin(string user)
        {
            int idUsuario = -1;
            user = user.ToUpper();

            if (user.Equals("ADMIN") || user.Equals("NORMAL") || user.Equals("DESHABILITADO"))
            {
                user += "@vodastar.com";
            }

            if (FormaEmail(user))
            {
                clientesTabla = clientesAdapter.GetClientByMail(user);
                if(clientesTabla.Count>0)
                    idUsuario = clientesTabla[0].idClientes;
            }

            else
            {
                clientesTabla = clientesAdapter.GetClienteByDNI(user);
                if (clientesTabla.Count > 0)
                    idUsuario = clientesTabla[0].idClientes;
                
            }

            return idUsuario;
        }

        public static string NumeroDisponible()
        {
            int numero;
            Random rnd = new Random();

            do
            {
                numero = rnd.Next(6, 8) * 100000000;
                numero += rnd.Next(0, 100) * 1000000;
                numero += rnd.Next(0, 1000) * 1000;
                numero += rnd.Next(0, 1000);
            } while (ExisteTelefono(numero));


            return numero.ToString();

        }

        public static bool ExisteTelefono(int numero)
        {
            clientesTabla = clientesAdapter.GetClienteByTelf(numero.ToString());

            if (clientesTabla.Count>0)
                return true;
            return false;
        }

        public static bool ExisteTelefono(int numero, int idCliente)
             //comprueba si existe un telefono, si existe comprueba que lo tenga asignado dicho cliente
            // devuelve TRUE si el telefono está disponible, o si es de ese cliente
        {
            if (!ExisteTelefono(numero)) //el telefono está libre :D
                return true;

            clientesTabla = clientesAdapter.GetClienteByTelf(numero.ToString());

            if (clientesTabla[0].idClientes == idCliente) //ese numero es el tuyo, por lo que no hay problemas :)
                return true;

            return false; //tristemente ese telefono pertenece a otra personita, por lo que no se te puede asignar
          
        }

        public static bool ExisteMail(string mail, int idCliente)
        //comprueba si existe un mail, si existe comprueba que lo tenga asignado dicho cliente
        // devuelve TRUE si el mail está disponible, o si es de ese cliente
        {


            clientesTabla = clientesAdapter.GetClientByMail(mail);

            if (clientesTabla.Count==0) //el mail no está asignado
                return true;

            

            if (clientesTabla[0].idClientes == idCliente) //ese mail es el tuyo
                return true;

            return false; //mail ya registrado




        }
        public static bool ExisteDNI(string dni, int idCliente)
        //comprueba si existe un dni, si existe comprueba que lo tenga asignado dicho cliente
        // devuelve TRUE si el dni está disponible, o si es de ese cliente
        {


            clientesTabla = clientesAdapter.GetClienteByDNI(dni);

            if (clientesTabla.Count == 0) //el dni no está asignado
                return true;



            if (clientesTabla[0].idClientes == idCliente) //ese dni es el tuyo
                return true;

            return false; //dni ya registrado




        }
        public static Boolean FormaTelefono(String telefono)
        {
            int numero, n;

            bool rango = false;
                       
            if(Int32.TryParse(telefono[0].ToString(),out n))
            {
                if (n >= 6 && n <= 9)//es movil o fijo español 6,7,8,9
                    rango = true;
            }


            if (Int32.TryParse(telefono, out numero) && telefono.Length == 9 && rango)
                return true;
            return false;


        }


            public static Boolean FormaEmail(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public static void ActualizarAnyadirRegistro(Cliente cliente)
        {
            vodastarDataSet.ClientesRow regCliente;
            // si el alumno ya existe (estamos modificando) tomo su registro de la tabla
            // si no, Construimos un registro nuevo
            if (cliente.IdCliente > 0)
                regCliente = clientesTabla.FindByidClientes(cliente.IdCliente);
            else
                regCliente = clientesTabla.NewClientesRow();

            // actualizo el registro
            regCliente.Apellidos = cliente.Apellidos;
            regCliente.Nombre = cliente.Nombre;
            regCliente.DNI = cliente.Dni;
            regCliente.Clave = cliente.Clave;
            regCliente.Tipo = cliente.Tipo;
            regCliente.Telefono = cliente.Telefono;
            regCliente.eMail = cliente.Email;



            // Si era un alumno nuevo, añado el registro a la tabla
            if (cliente.IdCliente < 0)
                clientesTabla.AddClientesRow(regCliente);

            // En cualquier caso, actualizo la bd
            clientesAdapter.Update(regCliente);
        }

        public static void ActualizarAnyadirRegistro(Consumo consumo)
        {
            vodastarDataSet.ConsumosRow regConsumo;
            // si el alumno ya existe (estamos modificando) tomo su registro de la tabla
            // si no, Construimos un registro nuevo
            if (consumo.IdConsumo > 0)
                regConsumo = consumosTabla.FindByidConsumo(consumo.IdConsumo);
            else
                regConsumo = consumosTabla.NewConsumosRow();

            // actualizo el registro           
            regConsumo.Duracion = Convert.ToDecimal(consumo.Duracion);

            regConsumo.Destino = consumo.Destino;
            regConsumo.idCliente = consumo.IdCliente;
            regConsumo.idTarifa = consumo.IdTarifa;
            regConsumo.FechaHora = DateTime.Now;



            // Si era un alumno nuevo, añado el registro a la tabla
            if (consumo.IdConsumo < 0)
                consumosTabla.AddConsumosRow(regConsumo);

            // En cualquier caso, actualizo la bd
            consumosAdapter.Update(regConsumo);
        }


        public static void ActualizarAnyadirRegistro(Tarifa tarifa)
        {
            vodastarDataSet.TarifasRow regTarifa;
            // si el alumno ya existe (estamos modificando) tomo su registro de la tabla
            // si no, Construimos un registro nuevo
            if (tarifa.IdTarifa > 0)
                regTarifa = tarifaTabla.FindByidTarifa(tarifa.IdTarifa);
            else
                regTarifa = tarifaTabla.NewTarifasRow();

            // actualizo el registro           

            regTarifa.Nombre = tarifa.Nombre;
            regTarifa.PrecioFijo = tarifa.PrecioFijo;
            regTarifa.PrecioVariable = tarifa.PrecioVariable;
            



            // Si era un alumno nuevo, añado el registro a la tabla
            if (tarifa.IdTarifa < 0)
                tarifaTabla.AddTarifasRow(regTarifa);

            // En cualquier caso, actualizo la bd
            tarifaAdaptar.Update(regTarifa);
        }


        public static vodastarDataSet.ClientesDataTable ObtenerClientes()
        {
            clientesTabla = clientesAdapter.GetData();
            return clientesTabla;
        }
        public static vodastarDataSet.ClientesDataTable ObtenerClientes(int estado)
        {
            if (estado < 0)
                return ObtenerClientes();

            clientesTabla = clientesAdapter.GetClientesByStatus(estado);
            return clientesTabla;
        }

        public static Cliente ObtenClientePorDNI(string dni)
        {
            clientesTabla = clientesAdapter.GetClienteByDNI(dni);

            Cliente c = new Cliente(clientesTabla[0]);
            return c;
        }

        public static bool FormaDNI(string dni)
        {
            
            int numero;
            char letra;

            if (dni.Length > 9)
                return false;
            letra = dni[dni.Length - 1];
            if (letra < 'A' || letra > 'Z')
                return false;
            if (!Int32.TryParse(dni.Substring(0, dni.Length - 1), out numero))
                return false;
            return true;
                       
        }

        public static Cliente ObtenClientePorID(int idCliente)
        {
            Cliente cliente;
            clientesTabla = clientesAdapter.GetClientByID(idCliente);

            cliente = new Cliente(clientesTabla[0]);

            return cliente;


        }

        public static string ObtenNumero(int idCliente)
        {
            string numeroCliente;

            Cliente cliente = new Cliente(clientesAdapter.GetClientByID(idCliente)[0]);


            numeroCliente = cliente.Telefono;
            return numeroCliente;
        }


        public static string ObtenNombreID(int idCliente)
        {
            Cliente cliente;
            clientesTabla = clientesAdapter.GetClientByID(idCliente);

            cliente = new Cliente(clientesTabla[0]);

            return cliente.Nombre;
        }

            public static bool TieneConsumos(Cliente cliente)
        {
            consumosTabla = consumosAdapter.GetConsumoByCliente(cliente.IdCliente);
            if (consumosTabla.Count > 0)
                return true;
            return false;
        }


        public static void BorrarCliente(Cliente cliente)
        {
            clientesAdapter.DeleteClientePorID(cliente.IdCliente);
        }
        public static void BorrarTarifa(Tarifa tarifa)
        {
            tarifaAdaptar.DeleteByID(tarifa.IdTarifa);
        }

        public static vodastarDataSet.TarifasDataTable ObtenTodasTarifas()
        {
            return tarifaAdaptar.GetData();

        }

        public static vodastarDataSet.ConsumosDataTable ObtenerConsumos(int idCliente)
        {

            return consumosAdapter.GetConsumoByCliente(idCliente);

        }
        public static vodastarDataSet.ConsumosDataTable ObtenerConsumos(int idCliente,int idTarifa)
        {

            return consumosAdapter.GetConsumosByCliente(idCliente,idTarifa);

        }
        public static vodastarDataSet.ConsumosDataTable ObtenerConsumos()
        {

            return consumosAdapter.GetData();

        }
        public static vodastarDataSet.ConsumosDataTable ObtenerConsumosDeTarifa(int idTarifa)
        {

            return consumosAdapter.GetConsumosPorTarifas(idTarifa);

        }

        public static Tarifa ObtenTarifaPorNombre(string nombre)
        {
            tarifaTabla = tarifaAdaptar.GetTarifaByName(nombre);
            return new Tarifa(tarifaTabla[0]);
            
        }
        public static Tarifa ObtenTarifaPorID(string id)
        {
            tarifaTabla = tarifaAdaptar.GetTarfiaByID(Convert.ToInt32(id));
            return new Tarifa(tarifaTabla[0]);

        }
          
        public static double SumaConsumo(string idConsumo)
        {
            double total = 0;

            vodastarDataSet.ConsumosRow c = consumosAdapter.ObtenerConsumoID(Convert.ToInt32(idConsumo))[0];
            Tarifa t = new Tarifa(tarifaAdaptar.GetTarfiaByID(c.idTarifa)[0]);

            total = t.PrecioFijo + t.PrecioVariable * (double)c.Duracion / 100.0;

            return Math.Round(total,3);

        }


        public static string TotalDeTotales(string idCliente)
        {
            double total = 0;


            consumosTabla = consumosAdapter.GetConsumoByCliente(Convert.ToInt32(idCliente));

            for(int i=0; i < consumosTabla.Count; i++)
            {

                total += SumaConsumo(consumosTabla[i].idConsumo.ToString());
            }



            return Math.Round(total,2).ToString();
        }


        public static bool ExisteConsumoDeTarifa(string idTarifa)
        {
            bool existe = false;

            consumosTabla = consumosAdapter.GetConsumosPorTarifas(Convert.ToInt32(idTarifa));

            if (consumosTabla.Count > 0)
                existe = true;

            return existe;
        }

    }
}
