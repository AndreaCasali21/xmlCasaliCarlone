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
            IEnumerable<string> barcode = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                               .Elements("Biblioteca").Elements("wiride")
                                          select biblioteca.Element("codice_scheda").Value;
            IEnumerable<string> title = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                               .Elements("Biblioteca").Elements("wiride")
                                        select biblioteca.Element("titolo").Value;
            IEnumerable<string> author = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                                .Elements("Biblioteca").Elements("wiride").Elements("titolo")
                                         select biblioteca.Element("responsabilita").Value;
            IEnumerable<string> subject = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                                .Elements("Biblioteca").Elements("wiride")
                                          select biblioteca.Element("descrittori_lgi").Value;
            IEnumerable<string> description = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                                .Elements("Biblioteca").Elements("wiride")
                                              select biblioteca.Element("soggetti").Value;
            IEnumerable<string> category = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                                .Elements("Biblioteca").Elements("wiride")
                                           select biblioteca.Element("genere").Value;
            IEnumerable<string> media = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                                .Elements("Biblioteca").Elements("wiride")
                                        select biblioteca.Element("genere").Value;
            IEnumerable<string> publisher = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                                .Elements("Biblioteca").Elements("wiride").Elements("pubblicazione")
                                            select biblioteca.Element("luogo").Value;
            IEnumerable<string> publication = from biblioteca in XDocument.Load(@"../../libri.XML")
                                                                .Elements("Biblioteca").Elements("wiride").Elements("pubblicazione")
                                              select biblioteca.Element("editore").Value;


            foreach (string name in title)
            {
                lst_out.Items.Add(name);
            }

            xmlDoc.Save(@"C:\Users\andrea.casali\source\repos\Libreriaxml-Casali-Carlone\Libreriaxml-Casali-Carlone\newLibri.xml");
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

                                              where biblioteca.Element("genere").Value == Genere

                                              select biblioteca.Element("genere").Value;

            foreach (string cont in Num_romanzi)
            {
                x++;
            }
            lst_out.Items.Add(x);
            
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
            lst_out.Items.Add(x);
        }

        private void btn_elimina_Click(object sender, RoutedEventArgs e)
        {
            xmlDoc.Nodes().OfType<XElement>().Elements("wiride").Elements("abstract").Remove();
            xmlDoc.Save(@"../../libri.xml");
        }
    }
}
