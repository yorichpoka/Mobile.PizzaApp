namespace PizzaApp.Domain.Models
{
    public class InputParameterModel
    {
        public string UrlRequestUri { get; set; }
        public string UrlLocalStorageFile { get; set; }

        public InputParameterModel(string urlRequestUri, string urlLocalStorageFile)
        {
            this.UrlRequestUri = urlRequestUri;
            this.UrlLocalStorageFile = urlLocalStorageFile;
        }
    }
}