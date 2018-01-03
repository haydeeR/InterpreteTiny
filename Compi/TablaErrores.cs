using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManejoDeErrores
{
    public class TablaErrores
    {
        List<Error> listaErrores = null;
        static TablaErrores instanceTable = null;

        public static TablaErrores InstanceTable
        {
            get
            {
                if (instanceTable == null)
                    instanceTable = new TablaErrores();
                return instanceTable;
            }
        }

        private TablaErrores()
        {
            this.listaErrores = new List<Error>();
        }

        public void agregaError(Error e)
        {
            this.listaErrores.Add(e);
        }

        public void eliminaError(Error e)
        {
            this.listaErrores.Remove(e);
        }

        public void limpiaError()
        {
            this.listaErrores.Clear();
        }

        public bool isEmpty()
        {
            bool res = true;
            if (this.listaErrores.Count > 0)
                res = false;
            return res;
        }

        public string allErrors()
        {
            string res = "";
            foreach (Error e in this.listaErrores)
            {
                res += e.Value;
            }
            return res;
        }


        public List<Error> Errores { get { return this.listaErrores; } }
    }

    public class Error
    {
        //errores (numero de linea,error,como solucionarlo)
        public int num_lineaE;
        public string error;
        public string descrip;
        public string sol_error;

        /// <summary>
        /// Crea un error con los datos enviados por parametro.
        /// </summary>
        /// <param name="numLinea">El número de línea en la que ocurrio el error</param>
        /// <param name="strError">El texto del error</param>
        /// <param name="solucionError">La solución para resolver el error</param>
        /// <param name="descripcion">Una descripción más ampliada del error</param>
        public Error(int numLinea, string strError, string solucionError, string descripcion)
        {
            num_lineaE = numLinea;
            error = strError;
            sol_error = solucionError;
            descrip = descripcion;

        }

        public Error(string strError, string solucionError, string descripcion)
        {
            error = strError;
            sol_error = solucionError;
            descrip = descripcion;

        }

        public Error()
        {

        }

        public int NumerodeLinea
        {
            get { return num_lineaE; }
            set { num_lineaE = value; }
        }

        public string Value
        {
            get { return error; }
            set { error = value; }
        }

        public string Solucion
        {
            get { return sol_error; }
            set { sol_error = value; }
        }

        public string Descripcion
        {
            get { return descrip; }
            set { descrip = value; }
        }
    }
}
