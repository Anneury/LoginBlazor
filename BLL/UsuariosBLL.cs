using LoginBlazor.DAL;
using LoginBlazor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LoginBlazor.BLL
{
    public class UsuariosBLL
    {
        public static bool Guardar(Usuarios suplidor)
        {
            if (!Existe(suplidor.UsuarioId))
                return Insertar(suplidor);
            else
                return Modificar(suplidor);
        }

        private static bool Existe(int id)
        {
            bool Existencia = false;
            Contexto contexto = new Contexto();

            try
            {
                Existencia = contexto.Usuarios.Any(x => x.UsuarioId == id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Existencia;
        }

        private static bool Insertar(Usuarios suplidor)
        {
            bool Insertado = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Usuarios.Add(suplidor);
                Insertado = (contexto.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Insertado;
        }

        private static bool Modificar(Usuarios suplidor)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                contexto.Entry(suplidor).State = EntityState.Modified;
                paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool Eliminado = false;
            Contexto contexto = new Contexto();

            try
            {
                var suplidor = Buscar(id);

                contexto.Entry(suplidor).State = EntityState.Deleted;
                Eliminado = (contexto.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Eliminado;
        }

        public static Usuarios Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Usuarios suplidor;

            try
            {
                suplidor = contexto.Usuarios.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return suplidor;
        }

        public static List<Usuarios> GetList(Expression<Func<Usuarios, bool>> suplidor)
        {
            List<Usuarios> Lista = new List<Usuarios>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Usuarios.Where(suplidor).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }
        public static bool ExisteUsuario(string user, string password)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                paso = contexto.Usuarios.Any(e => e.NombreDeUsuario.Contains(user) && e.Password.Contains(password));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }
    }
}
