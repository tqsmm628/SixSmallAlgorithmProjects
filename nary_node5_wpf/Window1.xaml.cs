using System.Windows;

namespace nary_node5
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Build a test tree.
            // A
            //         |
            //     +---+---+
            // B   C   D
            //     |       |
            //    +-+      +
            // E F      G
            //    |        |
            //    +      +-+-+
            // H      I J K
            var GeneriGloop = new NaryNode<string>("GeneriGloop");
            var RD = new NaryNode<string>("R & D");
            var Sales = new NaryNode<string>("Sales");
            var ProfessionalServices = new NaryNode<string>("Professional Services");
            var Applied = new NaryNode<string>("Applied");
            var Basic = new NaryNode<string>("Basic");
            var Advanced = new NaryNode<string>("Advanced");
            var SciFi = new NaryNode<string>("Sci Fi");
            var InsideSales = new NaryNode<string>("Inside Sales");
            var OutsideSales = new NaryNode<string>("Outside Sales");
            var B2B = new NaryNode<string>("B2B");
            var Consumer = new NaryNode<string>("Consumer");
            var AccountManagement = new NaryNode<string>("Account Mangement");
            var HR = new NaryNode<string>("HR");
            var Accounting = new NaryNode<string>("Accounting");
            var Legal = new NaryNode<string>("Legal");
            var Training = new NaryNode<string>("Training");
            var Hiring = new NaryNode<string>("Hiring");
            var Equity = new NaryNode<string>("Equity");
            var Discipline = new NaryNode<string>("Discipline");
            var Payroll = new NaryNode<string>("Payroll");
            var Billing = new NaryNode<string>("Billing");
            var Reporting = new NaryNode<string>("Reporting");
            var Opacity = new NaryNode<string>("Opacity");
            var Compliance = new NaryNode<string>("Compliance");
            var ProgressPrevention = new NaryNode<string>("Progress Prevention");
            var BailServices = new NaryNode<string>("Bail Services");

            GeneriGloop.AddChild(RD);
            GeneriGloop.AddChild(Sales);
            GeneriGloop.AddChild(ProfessionalServices);
            RD.AddChild(Applied);
            RD.AddChild(Basic);
            RD.AddChild(Advanced);
            RD.AddChild(SciFi);
            Sales.AddChild(InsideSales);
            Sales.AddChild(OutsideSales);
            Sales.AddChild(B2B);
            Sales.AddChild(Consumer);
            Sales.AddChild(AccountManagement);
            ProfessionalServices.AddChild(HR);
            ProfessionalServices.AddChild(Accounting);
            ProfessionalServices.AddChild(Legal);
            HR.AddChild(Training);
            HR.AddChild(Hiring);
            HR.AddChild(Equity);
            HR.AddChild(Discipline);
            Accounting.AddChild(Payroll);
            Accounting.AddChild(Billing);
            Accounting.AddChild(Reporting);
            Accounting.AddChild(Opacity);
            Legal.AddChild(Compliance);
            Legal.AddChild(ProgressPrevention);
            Legal.AddChild(BailServices);

            // Draw the tree.
            GeneriGloop.ArrangeAndDrawSubtree(mainCanvas, 10, 10);
        }
    }
}
