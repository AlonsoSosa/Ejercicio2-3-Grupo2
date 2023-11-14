using Ejercicio2_3_Grupo2.Modelos;
using CommunityToolkit.Maui.Views;
using Ejercicio2_3_Grupo2.Convertidores;
using System.Collections.ObjectModel;

namespace Ejercicio2_3_Grupo2.Vistas;

public partial class PageList : ContentPage
{
    private ObservableCollection<Audios> listaAudios;
    private MediaElement mediaElement;

    public PageList()
    {
        InitializeComponent();
        listaAudios = new ObservableCollection<Audios>();
        List.ItemsSource = listaAudios;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (App.DBase != null)
        {
            await ActualizarListaAudios();
        }
        else
        {
        }
    }

    private async System.Threading.Tasks.Task ActualizarListaAudios()
    {
        var audios = await App.DBase.getListAudio();
        listaAudios.Clear();

        foreach (var audio in audios)
        {
            listaAudios.Add(audio);
        }
    }

    private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (App.DBase != null)
        {
            await ActualizarListaAudios();
            var audioSeleccionado = (Audios)e.Item;

            if (audioSeleccionado != null)
            {
                string action = await DisplayActionSheet("Selecciona una opción", "Cancelar", null, "Reproducir Audio", "Eliminar Audio");

                switch (action)
                {
                    case "Reproducir":
                        ReproducirAudio(audioSeleccionado.url);
                        break;

                    case "Eliminar":
                        bool confirmacion = await DisplayAlert("Confirmar", "¿Estás seguro de Eliminar el audio?", "Sí", "No");
                        if (confirmacion)
                        {
                            await EliminarAudio(audioSeleccionado);
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        else
        {
            
        }
    }

    private void ReproducirAudio(string filePath)
    {
        if (mediaElement != null)
        {
            mediaElement.Stop();
            (Content as StackLayout)?.Children.Remove(mediaElement);
        }

        mediaElement = new MediaElement
        {
            Source = filePath,
            ShouldAutoPlay = true
        };

        (Content as StackLayout)?.Children.Add(mediaElement);
    }

    private async Task EliminarAudio(Audios audio)
    {
        if (App.DBase != null)
        {
            if (mediaElement != null)
            {
                mediaElement.Stop();
                (Content as StackLayout)?.Children.Remove(mediaElement);
            }

            bool eliminacionExitosa = await App.DBase.Eliminar(audio);

            if (eliminacionExitosa)
            {
                await DisplayAlert("Éxito", "Registro eliminado correctamente", "Ok");
                await ActualizarListaAudios();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar el Registro", "Ok");
            }
        }
        else
        {
        }
    }

    private async void B_Play_Invoked(object sender, EventArgs e)
    {
        if (List.SelectedItem != null)
        {
            var audioSeleccionado = (Audios)List.SelectedItem;
            ReproducirAudio(audioSeleccionado.url);
        }
        else
        {
            await DisplayAlert("Aviso", "Por favor, Seleccione un Audio.", "Ok");
        }
    }

    private async void B_Delete_Invoked(object sender, EventArgs e)
    {
        if (List.SelectedItem != null)
        {
            var audioSeleccionado = (Audios)List.SelectedItem;
            bool confirmacion = await DisplayAlert("Confirmar", "¿Estás seguro de Eliminar el audio?", "Sí", "No");
            if (confirmacion)
            {
                await EliminarAudio(audioSeleccionado);
            }
        }
        else
        {
            await DisplayAlert("Aviso", "Por favor, Sleccione una opcion.", "Ok");
        }
    }

    private void PausarClicked(object sender, EventArgs e)
    {
        if (mediaElement != null)
        {
            mediaElement.Pause();
        }
    }

}
