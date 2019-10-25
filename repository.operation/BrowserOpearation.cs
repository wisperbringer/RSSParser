using System.Net;

namespace repository.operation
{
    public class BrowserOperation
    {
        public System.Xml.XmlDocument DawloadXML(string url)
        {
            System.Xml.XmlDocument xmlDocument = null;
            try
            {
                var webClient = new WebClient();
                var loadedData = webClient.DownloadString(url);
                xmlDocument = new System.Xml.XmlDocument();
                xmlDocument.LoadXml(loadedData);
                return xmlDocument;
            }
            catch (WebException ex)
            {
                System.Console.WriteLine(ex);
            }
            return xmlDocument;
        }
    }
}
