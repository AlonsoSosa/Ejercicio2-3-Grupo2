using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Ejercicio2_3_Grupo2.Modelos;

namespace Ejercicio2_3_Grupo2.Convertidores
{
    public class Servicios
    {
        public async Task<bool> saveAudios(Audios Audio)
        {
            var result = await App.DBase.insertUpdateAudio(Audio);

            return (result > 0);

        }


        public async Task<List<Audios>> GetAudios()
        {
            return await App.DBase.getListAudio();
        }

       /* public async Task<bool> DeleteAudio(Audios audio)
        {
            //var result = await App.DBase.deleteAudio(audio);
            //return (result > 0);
        }*/
    }
}