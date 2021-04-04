using AirlineManagementSystemDatabasesAssistant.views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineManagementSystemDatabasesAssistant
{
    public partial class Form1 : Form
    {
        private SizeChanger _sizeChanger;

        private DAO _dao = new DAO();
        private TypeToDataConverter _converterToData;

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }
        private async void Initialize()
        {
            this.BackColor = Color.FromArgb(124,129, 125);
            rtbItemInfo.Location = new Point(cmbSelectedResult.Location.X, cmbSelectedResult.Location.Y + cmbSelectedResult.Height + 5);
            rtbItemInfo.Width = cmbSelectedResult.Width;
            pbxItemImage.Location = new Point(cmbSelectedResult.Location.X, cmbSelectedResult.Location.Y + cmbSelectedResult.Height + 5);
            pbxItemImage.Visible = false;

            lblWaitMessage.drawBorder(3, Color.DarkBlue);
            lblWaitMessage.Font = new System.Drawing.Font("Arial", 20, System.Drawing.FontStyle.Bold);
            lblWaitMessage.TextAlign = ContentAlignment.MiddleCenter;
            lblWaitMessage.Padding = new Padding(5);
            lblWaitMessage.Text = "Please Wait";

            pnlControlsHolder.Visible = false;

            Control[] controlsToChangeSize = new Control[] { lblWaitMessage, rtbItemInfo, cmbSelectedResult };

            _sizeChanger = new SizeChanger(this.Width, this.Height, controlsToChangeSize);
            var dimentions = _sizeChanger.ControlsForChangingSizeDimentions;
            this.MaximumSize = new Size(_sizeChanger.MainFormInitialWidth, _sizeChanger.MainFormInitialHeight);
            this.SizeChanged += (object sender, EventArgs e) => 
            {
                _sizeChanger.Resize(this.Width, this.Height, out int resizeFacrorX, out int resizefactorY);
                lblWaitMessage.Width = dimentions[0][0] + resizeFacrorX;
                lblWaitMessage.drawBorder(3, Color.DarkBlue);
                cmbSelectedResult.Width = dimentions[2][0] + resizeFacrorX;
                rtbItemInfo.Width = dimentions[1][0] + resizeFacrorX;
                rtbItemInfo.Height = dimentions[1][1] + resizefactorY;
            };


            Task tsk = _dao.SetConnectionStringAsync("The_very_important_Flight_Center_Project");
            _converterToData = new TypeToDataConverter(_dao);
            Timer timer = new Timer();
            timer.Interval = 10;
            int count = 0;
            timer.Tick += (object sender, EventArgs e) =>
            {
                count++;
                lblWaitMessage.Text = $"Please Wait, -= {count} =-";
                if (tsk.IsCompleted)
                {
                    timer.Stop();
                    pnlControlsHolder.Visible = true;
                    lblWaitMessage.Text = $"-= {count} =-";
                }
            };
            timer.Start();
            await tsk;
            cmbTableNames.Items.AddRange(_dao.GetAllTableNames().ToArray());
            pnlControlsHolder.Visible = true;

            cmbTableNames.SelectedIndexChanged += (object sender, EventArgs e) => 
            {
                rtbItemInfo.Text = string.Empty;
                pbxItemImage.Visible = false;
                rtbItemInfo.Width = cmbSelectedResult.Width;
                rtbItemInfo.Location = new Point(cmbSelectedResult.Location.X, cmbSelectedResult.Location.Y + cmbSelectedResult.Height + 5);
                cmbSelectedResult.Text = string.Empty;
                cmbSelectedResult.Items.Clear();
            };

            btnGetAll.Click += async(object sender, EventArgs e) => 
            {
                Timer locTimer = new Timer();
                locTimer.Interval = 100;
                locTimer.Tick += (object currentSender, EventArgs ea) => 
                {
                    rtbItemInfo.Text += "* ";
                };
                locTimer.Start();


                cmbSelectedResult.Items.Clear();
                btnGetAll.Enabled = false;
                cmbTableNames.Enabled = false;
                cmbSelectedResult.Enabled = false;


                if(cmbTableNames.SelectedItem == null)
                {
                    SwitchDefaultFalling(locTimer);
                    MessageBox.Show("Please select the type from the dropdown on the right");
                    return;
                }

                Assembly asm = this.GetType().Assembly;
                Type type = asm.GetType($"AirlineManagementSystemDatabasesAssistant.{((string)cmbTableNames.SelectedItem).SingularizeTableName()}");
                if (type == null)
                {
                    SwitchDefaultFalling(locTimer);
                    MessageBox.Show("This table isn't related to the project");
                    return;
                }

                MethodInfo methodInfo = typeof(DAO).GetMethod(nameof(_dao.GetAll));
                MethodInfo genericMethod = methodInfo.MakeGenericMethod(type);
                dynamic task = genericMethod.Invoke(_dao, null);
                var genericIEnumerable = await task;
                foreach(var s in genericIEnumerable as IEnumerable)
                {
                    switch(s.GetType().Name)
                    {
                        case "AirlineCompany":
                            var airlineData = await _converterToData.ConversionSelector(s as AirlineCompany, type) as AirlineCompanyData;
                            cmbSelectedResult.Items.Add(airlineData);
                            break;
                        case "Country":
                            var countryData = await _converterToData.ConversionSelector(s as Country, type) as CountryData;
                            cmbSelectedResult.Items.Add(countryData);
                            break;
                        case "Customer":
                            var customerData = await _converterToData.ConversionSelector(s as Customer, type) as CustomerData;
                            cmbSelectedResult.Items.Add(customerData);
                            break;
                        case "Administrator":
                            var adminData = await _converterToData.ConversionSelector(s as Administrator, type) as AdministratorData;
                            cmbSelectedResult.Items.Add(adminData);
                            break;
                        case "Utility_class_User":
                            string userKind = s.GetType().GetProperty("USER_KIND").GetValue(s) as string;

                            switch (userKind)
                            {
                                case "Customer":
                                    var customerAsUserData = await _converterToData.ConversionSelector(s as Utility_class_User, type) as Utility_class_UserCustomerData;
                                    cmbSelectedResult.Items.Add(customerAsUserData);
                                    break;
                                case "AirlineCompany":
                                    var airlineAsUserData = await _converterToData.ConversionSelector(s as Utility_class_User, type) as Utility_class_UserAirlineCompanyData;
                                    cmbSelectedResult.Items.Add(airlineAsUserData);
                                    break;
                                case "Administrator":
                                    var adminAsUserData = await _converterToData.ConversionSelector(s as Utility_class_User, type) as Utility_class_UserAdministratorData;
                                    cmbSelectedResult.Items.Add(adminAsUserData);
                                    break;
                            }

                            break;
                        default:
                            SwitchDefaultFalling(locTimer);
                            MessageBox.Show($"You currently can't retrive {s.GetType().Name.PluraliseNoun()}");
                            return;
                    }
                }

                SwitchDefaultFalling(locTimer);
                cmbSelectedResult.Text = "Ready!";
            };

            cmbSelectedResult.SelectedIndexChanged += (object sender, EventArgs e) => 
            {
                pbxItemImage.Visible = false;
                rtbItemInfo.Text = string.Empty;
                Bitmap selectedItemImage = null;
                if(cmbSelectedResult.SelectedItem.GetType().GetProperty("Image") != null)
                    selectedItemImage = (Bitmap)cmbSelectedResult.SelectedItem.GetType().GetProperty("Image").GetValue(cmbSelectedResult.SelectedItem);
                if (selectedItemImage != null)
                {
                    int resizeFactor = 256;

                    if (selectedItemImage.Width <= resizeFactor)
                    {
                        pbxItemImage.Width = selectedItemImage.Width;
                        pbxItemImage.Height = selectedItemImage.Height;
                    }
                    else
                    {
                        selectedItemImage = ImageProvider.ResizeImageProportionally(selectedItemImage, resizeFactor);
                        pbxItemImage = Statics.ResizeControlProportionally(pbxItemImage, resizeFactor);
                    }
                    rtbItemInfo.Location = new Point(pbxItemImage.Location.X + pbxItemImage.Width + 5, cmbSelectedResult.Location.Y + cmbSelectedResult.Height + 5);
                    rtbItemInfo.Width = cmbSelectedResult.Width - pbxItemImage.Width - 5;
                    pbxItemImage.Image = selectedItemImage;
                    pbxItemImage.Visible = true;
                }
                PropertyInfo[] selectedItemProperties = cmbSelectedResult.SelectedItem.GetType().GetProperties();
                int n = selectedItemProperties[0].MetadataToken;
                Array.Sort(selectedItemProperties, new ComparerByNumericValuedProperty<PropertyInfo>("MetadataToken"));
                int n2 = selectedItemProperties[0].MetadataToken;
                for (int i = 0; i < selectedItemProperties.Length; i++)
                {
                    if(selectedItemProperties[i].PropertyType == typeof(String) && selectedItemProperties[i].GetValue(cmbSelectedResult.SelectedItem) == null)
                    {
                        string selctedItemName = cmbSelectedResult.SelectedItem.GetType().Name;
                        if (cmbSelectedResult.SelectedItem.GetType().Name.Contains("Data"))
                            selctedItemName = cmbSelectedResult.SelectedItem.GetType().Name.Replace("Data", "");
                            selectedItemProperties[i].SetValue(cmbSelectedResult.SelectedItem, $"{selectedItemProperties[i].Name} don't exists or can't be retrived for this {selctedItemName}");
                    }
                    //.OrderBy(x => x.MetadataToken).ToArray()
                    if (selectedItemProperties[i].PropertyType != typeof(Customer) && selectedItemProperties[i].PropertyType != typeof(Administrator) && selectedItemProperties[i].PropertyType != typeof(AirlineCompany) && (selectedItemProperties[i].GetValue(cmbSelectedResult.SelectedItem) != null))
                    {
                        if ((!selectedItemProperties[i].GetValue(cmbSelectedResult.SelectedItem).Equals("forInterfaceImplementation")) && (!selectedItemProperties[i].GetValue(cmbSelectedResult.SelectedItem).Equals(-111111L)))
                        {
                            if (selectedItemProperties[i].GetValue(cmbSelectedResult.SelectedItem) is Bitmap)
                                continue;
                            rtbItemInfo.Text += $" {selectedItemProperties[i].Name}: {selectedItemProperties[i].GetValue(cmbSelectedResult.SelectedItem)}" + Environment.NewLine;
                        }
                    }

                }
            };

            rtbItemInfo.MouseDown += (object sender, MouseEventArgs e) => 
            {
                if (e.Button == MouseButtons.Right)
                {
                    CopyTextToClipboard(sender as RichTextBox, e);
                }
            };

            rtbItemInfo.DoubleClick += (object sender, EventArgs e) => 
            {
                if (!String.IsNullOrEmpty((sender as RichTextBox).SelectedText))
                    Clipboard.SetText((sender as RichTextBox).SelectedText);
                else
                    Clipboard.Clear();
                MenuItem mi = new MenuItem(Clipboard.GetText());
                MenuItem[] mis = new MenuItem[] { mi };
                ContextMenu cm = new ContextMenu(mis);
                cm.Show((sender as RichTextBox), new Point(0, 0));
            };


        }

        private void CopyTextToClipboard(RichTextBox rtb, MouseEventArgs e)
        {
            MenuItem mi = new MenuItem("Copy", (object sender2, EventArgs e2) =>
            {
                if(!String.IsNullOrEmpty(rtb.SelectedText))
                    Clipboard.SetText(rtb.SelectedText);
                else 
                    Clipboard.Clear();
                (sender2 as MenuItem).Text = Clipboard.GetText();
                new ContextMenu(new MenuItem[] { sender2 as MenuItem }).Show(rtb, new Point(0));
            });
            if (e != null)
            {
                MenuItem[] mis = new MenuItem[] { mi };
                ContextMenu cm = new ContextMenu(mis);
                cm.Show(rtb, new Point(e.X, e.Y));
            }
        }

        private void SwitchDefaultFalling(Timer tmr)
        {
            btnGetAll.Enabled = true;
            cmbTableNames.Enabled = true;
            cmbSelectedResult.Enabled = true;
            tmr.Stop();
        }










        //The_very_important_Flight_Center_Project
    }

}
