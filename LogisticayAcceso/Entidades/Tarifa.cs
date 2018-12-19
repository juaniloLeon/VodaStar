using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticayAcceso.Entidades
{
    public class Tarifa
    {
        int idTarifa;
        string nombre;
        double precioFijo, precioVariable;

        public Tarifa( )
        {
            this.IdTarifa = -1;
        }

        public Tarifa(int idTarifa, string nombre, double precioFijo, double precioVariable)
        {
            this.IdTarifa = idTarifa;
            this.Nombre = nombre;
            this.PrecioFijo = precioFijo;
            this.PrecioVariable = precioVariable;
        }

        public Tarifa(vodastarDataSet.TarifasRow tarifasRow)
        {
            this.IdTarifa = tarifasRow.idTarifa;
            this.Nombre = tarifasRow.Nombre;
            this.PrecioFijo = tarifasRow.PrecioFijo;
            this.PrecioVariable = tarifasRow.PrecioVariable;
        }


        public int IdTarifa
        {
            get
            {
                return idTarifa;
            }

            set
            {
                idTarifa = value;
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

        public double PrecioFijo
        {
            get
            {
                return precioFijo;
            }

            set
            {
                precioFijo = value;
            }
        }

        public double PrecioVariable
        {
            get
            {
                return precioVariable;
            }

            set
            {
                precioVariable = value;
            }
        }
    }
}
