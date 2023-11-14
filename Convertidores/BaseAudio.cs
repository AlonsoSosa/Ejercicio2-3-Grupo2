using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Ejercicio2_3_Grupo2.Modelos;

namespace Ejercicio2_3_Grupo2.Convertidores
{
    public class BaseAudio
    {
        readonly SQLiteAsyncConnection dbase;

        public BaseAudio(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);


            dbase.CreateTableAsync<Audios>();

        }

        #region OperacionesAudio
        //CREATE
        public Task<int> insertUpdateAudio(Audios Audio)
        {
            if (Audio.id != 0)
            {
                return dbase.UpdateAsync(Audio);
            }
            else
            {
                return dbase.InsertAsync(Audio);
            }
        }

        //READ
        public Task<List<Audios>> getListAudio()
        {
            return dbase.Table<Audios>().ToListAsync();
        }

        public Task<Audios> getAudio(int id)
        {
            return dbase.Table<Audios>()
                .Where(i => i.id == id)
                .FirstOrDefaultAsync();
        }

        //DELETE
        public async Task<bool> Eliminar(Audios audi)
        {
            try
            {
                int result = await dbase.DeleteAsync(audi);
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el audio: {ex.Message}");
                return false;
            }
        }

        #endregion OperacionesAudio

    }
}


