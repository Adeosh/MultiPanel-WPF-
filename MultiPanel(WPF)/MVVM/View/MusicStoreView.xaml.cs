using Microsoft.EntityFrameworkCore;
using MultiPanel_WPF_.Core;
using MultiPanel_WPF_.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MultiPanel_WPF_.MVVM.View
{
    public partial class MusicStoreView : UserControl
    {
        public List<Album> DatabaseAlbums { get; private set; }

        public MusicStoreView()
        {
            InitializeComponent();
            Read();
            ComboBoxValues();
        }

        private void ComboBoxValues()
        {
            for (int i = 1960; i <= 2030; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i;
                cbYear.Items.Add(item);
            }
        }

        public bool IsValid()
        {
            if (tbAlbum.Text == string.Empty)
            {
                MessageBox.Show("Название альбома не указано", "Провал", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tbArtist.Text == string.Empty)
            {
                MessageBox.Show("Название исполнителя не указано", "Провал", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (cbYear.Text == string.Empty)
            {
                MessageBox.Show("Год альбома не указан", "Провал", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (tbImage.Text == string.Empty)
            {
                MessageBox.Show("Ссылка на изображение не указано", "Провал", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public void Create()
        {
            if (IsValid())
            {
                try
                {
                    using (DataContext context = new DataContext())
                    {
                        var albumTitle = tbAlbum.Text;
                        var artist = tbArtist.Text;
                        var year = cbYear.Text;
                        var imageLink = tbImage.Text;

                        if (albumTitle != null && artist != null && year != null && imageLink != null)
                        {
                            context.Albums.Add(new Model.Album() { AlbumTitle = albumTitle, Artist = artist, Year = year, ImageLink = imageLink });
                            context.SaveChanges();
                            Read();
                            Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Read()
        {
            using (DataContext context = new DataContext())
            {
                DatabaseAlbums = context.Albums.ToList();
                listAlbum.ItemsSource = DatabaseAlbums;
            }
        }

        public void Update()
        {
            using (DataContext context = new DataContext())
            {
                Album selectedAlbum = listAlbum.SelectedItem as Album;

                var albumTitle = tbAlbum.Text;
                var artist = tbArtist.Text;
                var year = cbYear.Text;
                var imageLink = tbImage.Text;

                if (albumTitle != null && artist != null && year != null && imageLink != null)
                {
                    Album album = context.Albums.Find(selectedAlbum.Id);
                    album.AlbumTitle = albumTitle;
                    album.Artist = artist;
                    album.Year = year;
                    album.ImageLink = imageLink;
                    context.SaveChanges();
                    Read();
                    Clear();
                }
            }
        }

        public void Delete()
        {
            using (DataContext context = new DataContext())
            {
                Album selectedAlbum = listAlbum.SelectedItem as Album;

                if (selectedAlbum != null)
                {
                    Album album = context.Albums.Single(a => a.Id == selectedAlbum.Id);
                    context.Remove(album);
                    context.SaveChanges();
                    Read();
                }
            }
        }

        public void Clear()
        {
            tbAlbum.Clear();
            tbArtist.Clear();
            tbImage.Clear();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Create();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void listAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Album selectedItem = (Album)e.AddedItems[0];
                string imageUrl = selectedItem.ImageLink;

                imgAlbum.Source = new BitmapImage(new Uri(imageUrl));
            }
        }
    }
}
