using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemploWebForm.Models
{
    public class Usuario
    {
        private Guid _codigoUsuario;
        private string _nombre;
        private string _apellidos;

        public Usuario()
        {
            _codigoUsuario = new Guid("-1");
            _nombre = "";
            _apellidos = "";
        }

        public Guid CodigoUsuario
        {
            get
            {
                return _codigoUsuario;
            }

            set
            {
                _codigoUsuario = value;
            }
        }

        public string Nombre
        {
            get
            {
                return _nombre;
            }

            set
            {
                _nombre = value;
            }
        }

        public string Apellidos
        {
            get
            {
                return _apellidos;
            }

            set
            {
                _apellidos = value;
            }
        }
    }
}