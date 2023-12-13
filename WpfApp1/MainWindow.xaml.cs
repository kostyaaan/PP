using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Добавляем ссылки на нужные нам пространства имён
using System.Xml.Linq;

namespace WpfApp1
{
    ///<summary>
    /// Interaction logic for MainWindow.xaml
    ///</summary>
    ///
    public partial class MainWindow : Window
    {
    XElement xml;
    public MainWindow()
    {
        InitializeComponent();
        xml = XElement.Load(@"d:\XMLFile1.xml");
    }

    //В этом методе заключается вся логика по указанию источника данных, //которые следует отображать в окне

    private void Change_DataSource(object sender, RoutedEventArgs e)
    {
        treeStructure.ItemsSource = xml.Elements("Category");
        treeStructure.ItemTemplate = (HierarchicalDataTemplate)FindResource("key2");

        //Настраиваем привязку примечаний
        DescriptionBinding(selectedNodeDescription, "SelectedItem.Attribute[Description].Value", treeStructure);
        DescriptionBinding(selectedBookDescription, "SelectedItem.Attribute[Description].Value", listBooks);

        //Настраиваем привязку отображения книг
        listBooks.ItemTemplate = (DataTemplate)FindResource("key3");
        Binding bind = new Binding("SelectedItem.Elements[Book]") { Source = treeStructure };
        listBooks.SetBinding(ItemsControl.ItemsSourceProperty, bind);
    }

    /// Настройка привязки отображения данных
    ///<param name="textBlock">Текстовый объект, который должен 
    /// отображать текст примечания</param>
    ///<paramname="pathValue">значениеPathпривязки</param>
    ///<param name="source">ссылка на объект-источник, из которого считываются данные через свойство Path</param>

    void DescriptionBinding(TextBlock textBlock, string pathValue, Control source)
    {
        textBlock.SetBinding(TextBlock.TextProperty, new Binding(pathValue) { Source = source });
    }

    private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }
}
}

