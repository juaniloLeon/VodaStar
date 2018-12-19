using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticayAcceso.Entidades
{
   public class Cliente
    {

        int idCliente;
        string nombre, apellidos, dni, telefono, email, clave;
        int tipo;

        public Cliente()
        {
            idCliente = -2;
            clave = "pordefecto";
            tipo = 0;
        }




        public Cliente(vodastarDataSet.ClientesRow clientesRow)
        {
            this.IdCliente =clientesRow.idClientes;
            this.Nombre = clientesRow.Nombre;
            this.Apellidos = clientesRow.Apellidos;
            this.Dni = clientesRow.DNI;
            this.Telefono = clientesRow.Telefono;
            this.Email = clientesRow.eMail;
            this.Clave = clientesRow.Clave;
            this.Tipo = clientesRow.Tipo;
        }

        public Cliente(int idCliente, string nombre, string apellidos, string dni, string telefono, string email, string clave, int tipo)
        {
            this.IdCliente = idCliente;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Dni = dni;
            this.Telefono = telefono;
            this.Email = email;
            this.Clave = clave;
            this.Tipo = tipo;
        }

        public string Apellidos
        {
            get
            {
                return apellidos;
            }

            set
            {
                apellidos = value;
            }
        }

        public string Clave
        {
            get
            {
                return clave;
            }

            set
            {
                clave = value;
            }
        }

        public string Dni
        {
            get
            {
                return dni;
            }

            set
            {
                dni = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public int IdCliente
        {
            get
            {
                return idCliente;
            }

            set
            {
                idCliente = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                telefono = value;
            }
        }

        public int Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }
    }
}
