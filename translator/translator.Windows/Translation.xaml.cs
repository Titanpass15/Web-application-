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
    public sealed partial class Translation : Page
    {
        
        public int imageIndex;
        bool correct = false;
        bool wrong = false;
        public int scoreCorrect = 0;
        public int scoreWrong = 0;
        public int score = 0 ;

        DataTransferManager myDataManager = DataTransferManager.GetForCurrentView();

        public Translation()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            correctTextBox.Height = 0;
            wrongAnswer.Height = 0;
            GuessBox.Height = 0;
            GuessBox.Width = 0;
            TryAgain.Height = 0;
            answerBtn.Height = 0;

            if ((Application.Current as App)._dictionaryList == null)
            {
                (Application.Current as App)._dictionaryList = new List<clsWords>();
                await InitialiseListOfWords();
            }
            myIndex = -1;

        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            myDataManager.DataRequested -= myDataManager_DataRequested;
        }

        private async void myDataManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {

            if (GuessBox.Text == " ")
                return;

            DataPackage myData = args.Request.Data;
            myData.Properties.Description = "Meaning: " + GuessBox.Text + ".";
            myData.SetText("Translation" + GuessBox.Text);

            DataRequestDeferral deferral = args.Request.GetDeferral();


            try
            {

                StorageFile imageFile =await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
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

        public string wordsConvert;

        int myIndex;
        
        private void guessBtn_Click(object sender, RoutedEventArgs e)
        {
            if (GuessBox != null)
            {
                wordsConvert = GuessBox.Text;
                //txtBlockDisplay.Text = wordsConvert;
 
                for (int i = 0; i < (Application.Current as App)._dictionaryList.Count(); i++)
                {
                    if (wordsConvert == (Application.Current as App)._dictionaryList[imageIndex].strWord)
                    {
                        correct = true;
                        wrong = false;
                        correctTextBox.Height = 50;
                        break;
                        scoreCorrect++;
                        scoreCorrect+= score;
                    }

                    else
                    {
                        wrong = true;
                        
                        
                        if(score>0)
                        {
                            scoreWrong++;
                            scoreWrong-=score;
                        }
                    }

                   

                }

                if (wrong == true)
                {
                    wrongAnswer.Height = 50;
                }

                TryAgain.Height = 130;
                startButton.Height = 0;
                answerBtn.Height = 0;
            }

            GuessBox.Text = " ";

            if (score >= 0)
            {
                string display = "SCORE:  " + score.ToString();
                scoreBoard.Text = display;

            }

        }

        private void txtEnterText_TextChanged(object sender, TextChangedEventArgs e)
        {
          
        }

        private void starBtn_Click(object sender, RoutedEventArgs e)
        {
            
            answerBtn.Height = 130;
            correctTextBox.Height = 0;
            wrongAnswer.Height = 0;
            GuessBox.Height = 130;
            GuessBox.Width = 225;
            correctTextBox.Height = 0;
            imageIndex = 0;
            Random rnd = new Random();
             imageIndex = rnd.Next(0, 24);

             imageDisplay.Source = new BitmapImage(new Uri((Application.Current as App)._dictionaryList[imageIndex].strSignImage, UriKind.RelativeOrAbsolute));

             startButton.Height = 0;
             TryAgain.Height = 0;
        }

        private void txtBlockDisplay_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }

        private void correctTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void GuessBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void scoreBoard_TextChanged(object sender, TextChangedEventArgs e)
        {
           
           
        }

        #region File Picker Section

        FileOpenPicker myFilePicker;
        StorageFile myOpenFile;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        } 

     
 
     /*   private async void btnSaveFile_Click(object sender, RoutedEventArgs e)
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

        }*/

        #endregion
    }
}
