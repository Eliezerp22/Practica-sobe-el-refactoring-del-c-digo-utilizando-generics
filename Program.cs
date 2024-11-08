using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_sobe_el_refactoring_del_código_utilizando_generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
   
        public class Asiento
        {
            public int AsientoId { get; set; }
            public int BusId { get; set; }
            public int NumeroPiso { get; set; }
            public int NumeroAsiento { get; set; }
            public DateTime FechaCreacion { get; set; }
        }

        public interface IRepository<T> where T : class
        {
            void Agregar(T entidad);
            void Actualizar(T entidad);
            void Remover(T entidad);
            List<T> TraerTodos();
            T ObtenerPorId(int id);
        }

        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly List<T> _entidades = new List<T>();

            public void Agregar(T entidad)
            {
                _entidades.Add(entidad);
            }

            public void Actualizar(T entidad)
            {
                var index = _entidades.FindIndex(e => e.Equals(entidad));
                if (index >= 0)
                {
                    _entidades[index] = entidad;
                }
            }

            public void Remover(T entidad)
            {
                _entidades.Remove(entidad);
            }

            public List<T> TraerTodos()
            {
                return _entidades;
            }

            public T ObtenerPorId(int id)
            {
                var propertyInfo = typeof(T).GetProperty("AsientoId") ?? typeof(T).GetProperty("Id");
                return _entidades.SingleOrDefault(e => (int)propertyInfo.GetValue(e) == id);
            }
        }

        public class AsientoRepository : Repository<Asiento>
        {
        }

        class Programa
        {
            static void Main()
            {
                var asientoRepo = new AsientoRepository();

                asientoRepo.Agregar(new Asiento { AsientoId = 1, BusId = 10, NumeroPiso = 1, NumeroAsiento = 20, FechaCreacion = DateTime.Now });

                var asientos = asientoRepo.TraerTodos();
                Console.WriteLine($"Total de asientos: {asientos.Count}");

                var asiento = asientoRepo.ObtenerPorId(1);
                if (asiento != null)
                {
                    Console.WriteLine($"Asiento encontrado: ID {asiento.AsientoId}, Bus ID {asiento.BusId}");
                }

                asiento.NumeroAsiento = 21;
                asientoRepo.Actualizar(asiento);

                asientoRepo.Remover(asiento);
                Console.WriteLine($"Total de asientos después de eliminar: {asientoRepo.TraerTodos().Count}");
            }
        }
    }


