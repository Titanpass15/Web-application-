using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace translator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dictionary : Page
    {
        int myIndex;

        DataTransferManager myDataManager = DataTransferManager.GetForCurrentView();

        public Dictionary()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if ((Application.Current as App)._dictionaryList == null)
            {
                (Application.Current as App)._dictionaryList = new List<clsWords>();
                await InitialiseListOfWords();
            }     
                signImages.ItemsSource = (Application.Current as App)._dictionaryList;
                myIndex = -1;
               
            

            
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            myDataManager.DataRequested -= myDataManager_DataRequested;
        }

        private async void myDataManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
          
            if (tblTranslation.Text == "testing")
                return;

            DataPackage myData = args.Request.Data;
            myData.Properties.Description = "Meaning: " + tblTranslation.Text + ".";
            myData.SetText("Translation" + tblTranslation.Text);

            DataRequestDeferral deferral = args.Request.GetDeferral();

           
            try
            {
                
                StorageFile imageFile =
                    await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
                                                        new System.Uri((Application.Current as App)._dictionaryList[myIndex].strSignImage));
                myData.SetBitmap(RandomAccessStreamReference.CreateFromFile(imageFile));
            }
            finally
            {
                deferral.Complete();
            }

        }

      
        private async Task InitialiseListOfWords()
        {
            var file = await Package.Current.InstalledLocation.GetFileAsync("Data\\dictionarys.txt");
            var result = await FileIO.ReadTextAsync(file);
            try
            {
                // p[arse the json file text to a json array
                var tempList = JsonArray.Parse(result);
                convertArrayToList(tempList);

            }
            catch (Exception e)
            {
                string trouble = e.Message;
            }

        }

        private void convertArrayToList(JsonArray tempList)
        {
           
            foreach (var item in tempList)
            {
                var obj = item.GetObject();
                clsWords word = new clsWords();

                foreach (var key in obj.Keys)
                {
                    IJsonValue value;
                    if (!obj.TryGetValue(key, out value))
                        continue;

                    switch (key)
                    {
                        case "strWord":
                            word.strWord = value.GetString();
                            break;
                       
                        case "strSignImage":
                            word.strSignImage = "ms-appx:///Images/" + value.GetString();
                            break;
                       
                    } // end switch
                } // end for each key
                (Application.Current as App)._dictionaryList.Add(word);
            }
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel curr = (StackPanel)sender;

            for (int i = 0; i < (Application.Current as App)._dictionaryList.Count(); i++)
            {
                if (curr.Tag.ToString() ==
                    (Application.Current as App)._dictionaryList[i].strWord)
                {
                    myIndex = i;
                    break;
                }
            }
            if (myIndex == -1)
            {
                e.Handled = true;
                return;
            }


            imgWordImage.Source = new BitmapImage(new Uri((Application.Current as App)._dictionaryList[myIndex].strSignImage, UriKind.RelativeOrAbsolute));

            tblTranslation.Text = (Application.Current as App)._dictionaryList[myIndex].strWord;


            if (gridTranslation.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
            {
                gridTranslation.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

            e.Handled = true;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        FileOpenPicker myFilePicker;
        StorageFile myOpenFile; // returned by the user when they select a file.y

        private async void AddBtn(object sender, RoutedEventArgs e)
        {
            myFilePicker = new FileOpenPicker();

            // set the file filter types
            myFilePicker.FileTypeFilter.Add(".jpg");
            myFilePicker.FileTypeFilter.Add(".png");

            // set the suggest starting location
            myFilePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            myOpenFile = await myFilePicker.PickSingleFileAsync();

            if (myOpenFile != null)
            {
                showFile();
            }
        }

      

        private async void showFile()
        {
            // use the storage file as source for a bitmap
            // use the bitmap as source for the displayimage.
            IRandomAccessStream fileStream = await myOpenFile.OpenAsync(FileAccessMode.Read);
            BitmapImage myBitmap = new BitmapImage();
            myBitmap.SetSource(fileStream);
            imageDisplay.Source = myBitmap;
        }

        // save a file back as a copy
        StorageFile mySaveFile;
        FileSavePicker myFileSaver;

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            myFileSaver = new FileSavePicker();

            // set suggest start
            myFileSaver.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            myFileSaver.FileTypeChoices.Add("jpg image", new List<string>() { ".jpg" });

            myFileSaver.SuggestedFileName = "saveNewVersion";

            mySaveFile = await myFileSaver.PickSaveFileAsync();
            if (mySaveFile != null)
            {
                await myOpenFile.CopyAndReplaceAsync(mySaveFile);
            }
        }  
       
    }
}
