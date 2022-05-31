using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Coursework_client.DB;
using System.Reflection;

namespace Coursework_client
    {
    public partial class MainWindow : Window
        {
        #region fields
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private User worker { get; init; }
        private static readonly string[] work_btns =
        {
            "btn_ins_pln_md", "btn_upd_pln_md", "btn_del_pln_md",
            "btn_ins_pln", "btn_upd_pln", "btn_del_pln",
            "btn_ins_fl", "btn_upd_fl", "btn_del_fl",
            "btn_ins_tck", "btn_upd_tck", "btn_del_tck",
            "btn_ins_pers", "btn_upd_pers", "btn_del_pers",
            "btn_upd_ft"
        };

        private static readonly string[] fleet_btn_exclude =
            {
            "btn_ins_fl", "btn_upd_fl", "btn_del_fl",
            "btn_ins_tck", "btn_upd_tck", "btn_del_tck",
            "btn_ins_pers", "btn_upd_pers", "btn_del_pers",
            };

        private static readonly string[] flight_btn_exclude =
        {
            "btn_ins_pln_md", "btn_upd_pln_md", "btn_del_pln_md",
            "btn_ins_pln", "btn_upd_pln", "btn_del_pln",
            "btn_ins_tck", "btn_upd_tck", "btn_del_tck",
            "btn_ins_pers", "btn_upd_pers", "btn_del_pers",
            "btn_upd_ft"
        };

        private static readonly string[] ticket_btn_exclude =
        {
            "btn_ins_pln_md", "btn_upd_pln_md", "btn_del_pln_md",
            "btn_ins_pln", "btn_upd_pln", "btn_del_pln",
            "btn_ins_fl", "btn_upd_fl", "btn_del_fl",
        };

        #endregion


        public MainWindow(string login, string password)
            {
            InitializeComponent();

            worker = new User(login, password);
            SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);

            if (login.ToLower() == "balyaba" || login.ToLower() == "kulema")
                {
                foreach (string btn in work_btns)
                    {
                    var button = body.FindName(btn) as Control;
                    if (button != null)
                        {
                        button.IsEnabled = false;
                        button.ToolTip = "Действие невозможно для пользователя " + login;
                        ToolTipService.SetShowOnDisabled(button, true);
                        }
                    }
                }

            if (login.ToLower() == "ivy")
                {
                foreach (string btn in fleet_btn_exclude)
                    {
                    var button = body.FindName(btn) as Control;
                    if (button != null)
                        {
                        button.IsEnabled = false;
                        button.ToolTip = "Действие невозможно для пользователя " + login;
                        ToolTipService.SetShowOnDisabled(button, true);
                        }
                    }
                }


            if (login.ToLower() == "albert")
                {
                foreach (string btn in flight_btn_exclude)
                    {
                    var button = body.FindName(btn) as Control;
                    if (button != null)
                        {
                        button.IsEnabled = false;
                        button.ToolTip = "Действие невозможно для пользователя " + login;
                        ToolTipService.SetShowOnDisabled(button, true);
                        }
                    }
                }

            if (login.ToLower() == "john")
                {
                foreach (string btn in ticket_btn_exclude)
                    {
                    var button = body.FindName(btn) as Control;
                    if (button != null)
                        {
                        button.IsEnabled = false;
                        button.ToolTip = "Действие невозможно для пользователя " + login;
                        ToolTipService.SetShowOnDisabled(button, true);
                        }
                    }
                }

            update_all_db();
            }



        #region DataGrid Work
        private void query_insert(object sender, RoutedEventArgs e)
            {
            var name = ((Button)sender).Name;
            UpdWindow mw;
            if (Application.Current.Windows.OfType<UpdWindow>().Any())
                Application.Current.Windows.OfType<UpdWindow>().First().Close();
            switch (name)
                {
                case "btn_ins_pln_md":
                    mw = new UpdWindow(Tables.plane_model,worker);
                    mw.Show();
                    break;
                case "btn_ins_pln":
                    mw = new UpdWindow(Tables.plane, worker);
                    mw.Show();
                    break;
                case "btn_ins_fl":
                    mw = new UpdWindow(Tables.flight, worker);
                    mw.Show();
                    break;
                case "btn_ins_tck":
                    mw = new UpdWindow(Tables.ticket, worker);
                    mw.Show();
                    break;
                case "btn_ins_pers":
                    mw = new UpdWindow(Tables.person, worker);
                    mw.Show();
                    break;

                }
            }

        private void query_update(object sender, RoutedEventArgs e)
            {
            var name = ((Button)sender).Name;
            UpdWindow mw;
            if (Application.Current.Windows.OfType<UpdWindow>().Any())
                Application.Current.Windows.OfType<UpdWindow>().First().Close();
            switch (name)
                {
                case "btn_upd_pln_md":
                    var selectedItems = DataGrid_pln_mdl.SelectedItems;
                    if (selectedItems.Count > 1)
                        {
                        responseText_pln_md.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_pln_md.Text = "Выберите строку для редактирования.";
                        break;
                        }
                    var index = Convert.ToString((selectedItems[0] as DataRowView)[0]);
                    mw = new UpdWindow(Tables.plane_model, worker, index);
                    mw.Show();
                    break;
                case "btn_upd_pln":
                    selectedItems = DataGrid_pln.SelectedItems;
                    if (selectedItems.Count > 1)
                        {
                        responseText_pln.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_pln.Text = "Выберите строку для редактирования.";
                        break;
                        }
                    index = Convert.ToString((selectedItems[0] as DataRowView)[0]);
                    mw = new UpdWindow(Tables.plane, worker, index);
                    mw.Show();
                    break;
                case "btn_upd_fl":
                    selectedItems = DataGrid_fl.SelectedItems;
                    if (selectedItems.Count > 1)
                        {
                        responseText_fl.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_fl.Text = "Выберите строку для редактирования.";
                        break;
                        }
                    index = Convert.ToString((selectedItems[0] as DataRowView)[0]);
                    mw = new UpdWindow(Tables.flight, worker, index);
                    mw.Show();
                    break;
                case "btn_upd_tck":
                    selectedItems = DataGrid_tck.SelectedItems;
                    if (selectedItems.Count > 1)
                        {
                        responseText_tck.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_tck.Text = "Выберите строку для редактирования.";
                        break;
                        }
                    index = Convert.ToString((selectedItems[0] as DataRowView)[0]);
                    mw = new UpdWindow(Tables.ticket, worker, index);
                    mw.Show();
                    break;
                case "btn_upd_pers":
                    selectedItems = DataGrid_pers.SelectedItems;
                    if (selectedItems.Count > 1)
                        {
                        responseText_pers.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_pers.Text = "Выберите строку для редактирования.";
                        break;
                        }
                    index = Convert.ToString((selectedItems[0] as DataRowView)[0]);
                    mw = new UpdWindow(Tables.person, worker, index);
                    mw.Show();
                    break;
                }
            }

        private void query_delete(object sender, RoutedEventArgs e)
            {
            var name = ((Button)sender).Name;
            switch (name)
                {
                case "btn_del_pln_md":
                    var selectedItems = DataGrid_pln_mdl.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_pln_md.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var icao_code = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_model('{icao_code}');");
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_pln_md.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_pln":
                    selectedItems = DataGrid_pln.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_pln.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var registration_number = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_plane('{registration_number}');");
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_pln.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_fl":
                    selectedItems = DataGrid_fl.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_fl.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var flight_id = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_flight('{flight_id}');");
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_fl.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_tck":
                    selectedItems = DataGrid_tck.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_tck.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var etkt = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_ticket('{etkt}')");
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_tck.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_pers":
                    selectedItems = DataGrid_pers.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_pers.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var passport_id = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_person('{passport_id}');");
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_pers.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                }
            }

        private void make_task(object sender, RoutedEventArgs e)
            {
            switch (task_combobox.SelectedIndex)
                {
                case 0:
                    var dataset = worker.Query(
                        "SELECT *, " +
                        "CASE " +
                            "WHEN seat LIKE '%A' THEN 'A'" +
                            "WHEN seat LIKE '%B'  THEN 'B'" +
                            "WHEN seat LIKE '%C'  THEN 'C'" +
                            "WHEN seat LIKE '%D'  THEN 'D'" +
                            "WHEN seat LIKE '%E'  THEN 'E'" +
                        "ELSE 'No info'" +
                        "END " +
                        "FROM person NATURAL INNER JOIN ticket;");
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 1:
                    try
                        {
                        dataset = worker.Query($"SELECT * FROM ticket.full_ticket_view WHERE seat LIKE '%A'");
                        }
                    catch (Npgsql.PostgresException exception)
                        {
                        responseText_pers.Text = exception.Message;
                        break;
                        }
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 2:
                    dataset = worker.Query(
                        "SELECT passport_id,last_name,first_name,father_name," +
                        "(SELECT seat FROM ticket WHERE seat LIKE '%B') FROM " +
                        "(SELECT * FROM ticket NATURAL INNER JOIN person) AS full_ticket_table WHERE " +
                        "passport_id = (SELECT passport_id FROM person WHERE passport_id = '12 34 567890');" 
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 3:
                    dataset = worker.Query(
                        "SELECT DISTINCT class,(SELECT etkt FROM ticket WHERE etkt = '724 3369250194') FROM ticket;"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 4:
                    dataset = worker.Query(
                        "SELECT passport_id,last_name,first_name,father_name FROM(SELECT * FROM full_ticket_view) AS full_name;"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 5:
                    dataset = worker.Query(
                        "SELECT * FROM ticket WHERE flight_id = (SELECT flight_id FROM flight WHERE flight_id = 'K345');"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 6:
                    dataset = worker.Query(
                        "(SELECT owner, SUM(price_$) FROM ticket NATURAL INNER JOIN plane " +
                        "WHERE plane_registration_number = registration_number GROUP BY owner HAVING count_company_profit(owner) > 300);");
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 7:
                    dataset = worker.Query(
                        "SELECT * FROM full_ticket_view WHERE flight_id = ANY('{BT211,K345}');"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                }
            }

        private void reload_db(object sender, RoutedEventArgs e)
            {
            var name = ((Button)sender).Name;
            switch (name)
                {
                case "btn_rel_pln_md":
                    var dataset = worker.Query("SELECT * FROM plane_model ORDER BY icao_code;");
                    if (dataset != null)
                        DataGrid_pln_mdl.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_pln":
                    dataset = worker.Query("SELECT * FROM plane ORDER BY registration_number;");
                    if (dataset != null)
                        DataGrid_pln.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_fl":
                    dataset = worker.Query("SELECT * FROM flight ORDER BY flight_id;");
                    if (dataset != null)
                        DataGrid_fl.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_tck":
                    dataset = worker.Query("SELECT * FROM ticket ORDER BY etkt;");
                    if (dataset != null)
                        DataGrid_tck.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_pers":
                    dataset = worker.Query("SELECT * FROM person ORDER BY passport_id;");
                    if (dataset != null)
                        DataGrid_pers.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_full_ticket_view":
                    dataset = worker.Query("SELECT * FROM full_ticket_view ORDER BY etkt;");
                    if (dataset != null)
                        DataGrid_full_ticket_view.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                }
            }

        private void update_all_db()
            {
            var dataset = worker.Query("SELECT * FROM plane_model ORDER BY icao_code;");
            if (dataset != null)
                DataGrid_pln_mdl.ItemsSource = dataset.Tables[0].DefaultView;
            dataset = worker.Query("SELECT * FROM plane ORDER BY registration_number;");
            if (dataset != null)
                DataGrid_pln.ItemsSource = dataset.Tables[0].DefaultView;
            dataset = worker.Query("SELECT * FROM flight ORDER BY flight_id;");
            if (dataset != null)
                DataGrid_fl.ItemsSource = dataset.Tables[0].DefaultView;
            dataset = worker.Query("SELECT * FROM ticket ORDER BY etkt;");
            if (dataset != null)
                DataGrid_tck.ItemsSource = dataset.Tables[0].DefaultView;
            dataset = worker.Query("SELECT * FROM person ORDER BY passport_id;");
            if (dataset != null)
                DataGrid_pers.ItemsSource = dataset.Tables[0].DefaultView;
            dataset = worker.Query("SELECT * FROM full_ticket_view;");
            if (dataset != null)
                DataGrid_full_ticket_view.ItemsSource = dataset.Tables[0].DefaultView;
            }
        #endregion

        #region utils
        /*private void toggleTheme(object sender, RoutedEventArgs e)
            {
            var theme = paletteHelper.GetTheme();

            if (IsDarkTheme)
                {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
                }
            else
                {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
                }
            paletteHelper.SetTheme(theme);
            }*/

        private bool confirmDelete() =>
            MessageBox.Show("Удалить выбранные строки?",
                            "Подтверждение удаления",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning)
            == MessageBoxResult.Yes ? true : false;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
            {
            base.OnMouseLeftButtonDown(e);
            DragMove();
            }

        private void exitApp(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();
        #endregion
        private void DataGrid_pln_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {

            }
        }
    }
