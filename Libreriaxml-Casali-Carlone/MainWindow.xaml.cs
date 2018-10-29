using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Libreriaxml_Casali_Carlone
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        XDocument xmlDoc = XDocument.Parse(File.ReadAllText(@"../../libri.XML", System.Text.Encoding.UTF8), LoadOptions.None);

        private void btn_crea_Click(object sender, RoutedEventArgs e)
        {           
            IEnumerable<string> barcode = from biblioteca in xmlDoc.Descendants("wiride")
                                          select biblioteca.Element("codice_scheda").Value;
            IEnumerable<string> title = from biblioteca in xmlDoc.Descendants("wiride")
                                        select biblioteca.Element("titolo").Value;
            IEnumerable<string> author = from biblioteca in xmlDoc.Descendants("wiride").Elements("autore")
                                         select biblioteca.Element("cognome").Value;
            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Creazione di una nuova biblioteca con LINQ to XML"),
                new XElement("Biblioteca"));
            int x = 0;
            foreach(string count in barcode)
            {
                xmlDocument.Elements("Biblioteca").FirstOrDefault().Add(
                    
                    (new XElement("Libro",
                    new XElement("titolo", title.ElementAt(x)),
                    new XElement("codice", count),
                    new XElement("autore", author.ElementAt(x))                    
                    )));
                x++;
            }
            xmlDocument.Save(@"C:\Users\andrea.casali\source\repos\Libreriaxml-Casali-Carlone\Libreriaxml-Casali-Carlone\libriShort.xml");
            btn_crea.IsEnabled = false;
        }

        private void btn_titolo_Click(object sender, RoutedEventArgs e)
        {           
            string Autore = txt_aut.Text;
            IEnumerable<string> Ricerca_autore = from biblioteca in xmlDoc.Descendants("wiride")
                                          
                                            where biblioteca.Element("autore").Element("nome").Value == Autore
                                                              
                                            select biblioteca.Element("titolo").Value;
            foreach (string autore in Ricerca_autore)
            {
                lst_out.Items.Add(autore);
            }
        }

        private void btn_nrom_Click(object sender, RoutedEventArgs e)
        {          
            string Genere = "romanzo";
            int x = 0;
            IEnumerable<string> Num_romanzi = from biblioteca in xmlDoc.Descendants("wiride")

                                              where biblioteca.Element("genere").Value.Contains(Genere)

                                              select biblioteca.Element("genere").Value;

            foreach (string cont in Num_romanzi)
            {
                x++;
            }
            numeroromanzilbl.Content = x.ToString();

        }

        private void btn_ntitolo_Click(object sender, RoutedEventArgs e)
        {           
            string Titolo = txt_numt.Text;
            int x = 0;
            IEnumerable<string> Num_titoli = from biblioteca in xmlDoc.Descendants("wiride")

                                              where biblioteca.Element("titolo").Value == Titolo

                                              select biblioteca.Element("titolo").Value;

            foreach (string cont in Num_titoli)
            {
                x++;
            }
            numerocopielbl.Content = x.ToString();
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            xmlDoc.Nodes().OfType<XElement>().Elements("wiride").Elements("abstract").Remove();
            xmlDoc.Save(@"../../libri.xml");
        }

        private void btn_modificagenere_Click(object sender, RoutedEventArgs e)
        {
            string titolo = txt_titolo2.Text;
            string testo = txt_inputtesto.Text;
            IEnumerable<XElement> Mod_Gen = from biblioteca in xmlDoc.Descendants("wiride")

                                             where biblioteca.Element("titolo").Value == titolo

                                             select biblioteca.Element("genere");
            if(Mod_Gen.OfType<XElement>().First().Value == null)
            {
                xmlDoc.Element("Biblioteca")
               .Elements("wiride")
               .Where(x => x.Attribute("titolo").Value == titolo).First()
               .AddBeforeSelf(
               new XElement("genere", testo));
            }
            else
            {
                Mod_Gen.OfType<XElement>().First().Value = testo;
            }

                xmlDoc.Save(@"../../libri.xml");
        }
    }
}
