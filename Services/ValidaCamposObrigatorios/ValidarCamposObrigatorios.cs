using System;
using System.Collections.Generic;
using System.Linq;

namespace TesteBanco.Services
{
    public class CamposObrigatoriosValidator
    {
        public void ValidarCamposObrigatorios<T>(T objeto)
        {
            var tipo = objeto.GetType();
            var propriedades = tipo.GetProperties();

            var camposObrigatorios = new List<string>();

            foreach (var propriedade in propriedades)
            {
                var valor = propriedade.GetValue(objeto);
                if (valor == null || (valor is string && string.IsNullOrWhiteSpace((string)valor)))
                {
                    camposObrigatorios.Add(propriedade.Name);
                }
            }

            if (camposObrigatorios.Any())
            {
                throw new ArgumentException($"Campos obrigatórios não preenchidos: {string.Join(", ", camposObrigatorios)}");
            }
        }
    }
}
