using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticayAcceso.Entidades
{
    public class Consumo
    {
        int idConsumo, idCliente, idTarifa;
        string destino, duracion, fechaHora;


        public Consumo()
        {
            IdConsumo = -1;
        }

        public Consumo(int idConsumo, int idCliente, int idTarifa, string destino, string duracion, string fechaHora)
        {
            this.IdConsumo = idConsumo;
            this.IdCliente = idCliente;
            this.IdTarifa = idTarifa;
            this.Destino = destino;
            this.Duracion = duracion;
            this.FechaHora = fechaHora;
        }

        public int IdConsumo
        {
            get
            {
                return idConsumo;
            }

            set
            {
                idConsumo = value;
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

        public string Destino
        {
            get
            {
                return destino;
            }

            set
            {
                destino = value;
            }
        }

        public string Duracion
        {
            get
            {
                return duracion;
            }

            set
            {
                duracion = value;
            }
        }

        public string FechaHora
        {
            get
            {
                return fechaHora;
            }

            set
            {
                fechaHora = value;
            }
        }

      
    }
}
