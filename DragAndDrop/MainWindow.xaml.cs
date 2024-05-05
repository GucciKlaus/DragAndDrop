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

namespace DragAndDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Patient> patients { get; set; }
        public List<Doctor> doctors { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            patients = new List<Patient>
            {
                new Patient { Firstname = "Harry", Lastname = "Styles", PatientID = 1, BirthDate = new DateOnly(2001, 04, 08) },
                new Patient { Firstname = "Anton", Lastname = "Aigner", PatientID = 2, BirthDate = new DateOnly(1990, 04, 08) },
                new Patient { Firstname = "Jennifer", Lastname = "Lawrence", PatientID = 3, BirthDate = new DateOnly(2005, 04, 08) },
                new Patient { Firstname = "Sesam", Lastname = "Straße", PatientID = 4, BirthDate = new DateOnly(1989, 08, 04) },

        };
            doctors = new List<Doctor>
            {
                new Doctor { Firstname ="Susamme", Lastname="Muster", DoctorID=1, Title="Prim Dr." },
                new Doctor { Firstname ="Susamme", Lastname="Muster", DoctorID=2, Title="Ass Dr." },
                new Doctor { Firstname ="Susamme", Lastname="Muster", DoctorID=3, Title="OA Dr." }

            };
            foreach(Doctor doctor in doctors)
            {
                TreeViewItem tviDoc = new TreeViewItem();
                tviDoc.Header = doctor.ToString();
                tviDoc.Tag = doctor;
                tviDoc.IsExpanded = true;
                trvPatients.Items.Add(tviDoc);
            }
            for(int i = 0; i < patients.Count; i++) { 
                TreeViewItem tviNewPatient = new TreeViewItem();
                tviNewPatient.Header = patients[i].ToString();
                tviNewPatient.Tag = patients[i];
                (trvPatients.Items[i % trvPatients.Items.Count] as TreeViewItem).Items.Add(tviNewPatient);
            }
            lboPatients.ItemsSource = patients;
            lblObject.Content = patients[3];
        }
        #region Drag Drop Label to Label with string
        private void lblStringTarget_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
            
        }

        private void lblStringTarget_Drop(object sender, DragEventArgs e)
        {
            string? data = e.Data.GetData(typeof(string)) as string;
            if (data != null)
            {
                lblStringTarget.Content = data;
            }
        }
        

        private void lblString_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left || e.ChangedButton == MouseButton.Right)
            {
                Label? lblSource = sender as Label;
                string? data = lblSource?.Content?.ToString();
                if(data != null) { 
                    DragDrop.DoDragDrop(lblSource, data, DragDropEffects.Copy);
                }
            }
        }

        #endregion

        #region Drag Drop to Label with Object

        private void lblObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left || e.ChangedButton == MouseButton.Right)
            {
                Label? lblSource = sender as Label;
                Patient? data = lblSource?.Content as Patient;
                if (data != null)
                {
                    DragDrop.DoDragDrop(lblSource, data, DragDropEffects.Copy);
                }
            }
        }

        private void lblObjectTarget_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Patient)))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void lblObjectTarget_Drop(object sender, DragEventArgs e)
        {
            Patient? data = e.Data.GetData(typeof(Patient)) as Patient;
            if (data != null)
            {
                lblObjectTarget.Content = data;
            }
        }


        #endregion
        #region Drag Drop ListBox to ListBox within object (patient)
        private void lboPatients_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem? clickedItem = ItemsControl.ContainerFromElement(lboPatients, e.OriginalSource as DependencyObject) as ListBoxItem;
            Patient? p = clickedItem?.Content as Patient;
            if (p != null)
            {
                DragDrop.DoDragDrop(lboPatients,p,DragDropEffects.Copy);
            }
        }

        private void lboPatientsTarget_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Patient)))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void lboPatientsTarget_Drop(object sender, DragEventArgs e)
        {
            Patient? data = e.Data.GetData(typeof(Patient)) as Patient;
            if (data != null)
            {
                lboPatientsTarget.Items.Add(data);
            }
        }
        #endregion

        private void trvPatients_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DependencyObject? elem = e.OriginalSource as DependencyObject;
            TreeViewItem tvi = GetTreeViewItem(elem);

            Patient? p = tvi?.Tag as Patient;
            if(p != null)
            {
                DragDrop.DoDragDrop(trvPatients, p,DragDropEffects.Copy);
            }
        }
        private TreeViewItem? GetTreeViewItem(DependencyObject? obj)
        {
            while(obj != null && !(obj is TreeViewItem))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }
            return obj as TreeViewItem;
        }
        private void trvPatientsTarget_Drop(object sender, DragEventArgs e)
        {
            Patient? p = e.Data?.GetData(typeof(Patient)) as Patient;
            if(p != null)
            {
                trvPatientsTarget.Items.Add(p);
            }
        }

        private void trvPatientsTarget_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Patient)))
            {
                e.Effects=DragDropEffects.Copy;
            }
            else
            {
                e.Effects=DragDropEffects.None;
            }
            e.Handled = true;
        }

    }
}