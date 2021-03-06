using System.Linq;
using System.Threading;
using API_sing_inter.Models;
using System.Net;
using System.Text.RegularExpressions;

namespace API_sing_inter
{
    public class TreatmentHTML
    {
        public void WordSearch(ProcessItem item) 
        {
            
            //Регулярка для поиска всех тегов, знаков препинания и других небуквенных символов
            string pattern = "<.+?>|[^A-Za-zА-Яа-я]";
            string htmlCode = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    htmlCode = client.DownloadString(item.url);
                }
                Thread.Sleep(30000);// имитируем долгий процесс

                //Разделитель - все что найдет регулярка
                var words = Regex.Split(htmlCode, pattern);
                //Отсеиваем пустые строки
                words = words.Where(n => !string.IsNullOrEmpty(n)).ToArray();
                
                item.result = words.Length;
                item.status = 1;
            }
            catch 
            {
                item.status = 2;
            }
        }

        
    }
}
