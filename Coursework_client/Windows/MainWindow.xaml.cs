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
        private static readonly string[] btns =
        {
            "btn_ins_pln_md", "btn_upd_pln_md", "btn_del_pln_md",
            "btn_ins_pln", "btn_upd_pln", "btn_del_pln",
            "btn_ins_fl", "btn_upd_fl", "btn_del_fl",
            "btn_ins_tck", "btn_upd_tck", "btn_del_tck",
            "btn_ins_pers", "btn_upd_pers", "btn_del_pers",
            "btn_upd_ft"
        };

        #endregion


        public MainWindow(string login, string password)
            {
            InitializeComponent();

            worker = new User(login, password);
            usernameText.Text = login.ToUpper();
            SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);

            if (login.ToLower() == "user")
                {
                foreach (string btn in btns)
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
                        responseText_st.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_st.Text = "Выберите строку для редактирования.";
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
                        responseText_gr.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_gr.Text = "Выберите строку для редактирования.";
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
                        responseText_mr.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_mr.Text = "Выберите строку для редактирования.";
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
                        responseText_sj.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_sj.Text = "Выберите строку для редактирования.";
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
                        responseText_dp.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_dp.Text = "Выберите строку для редактирования.";
                        break;
                        }
                    index = Convert.ToString((selectedItems[0] as DataRowView)[0]);
                    mw = new UpdWindow(Tables.person, worker, index);
                    mw.Show();
                    break;
                case "btn_upd_ft":
                    selectedItems = DataGrid_full_ticket_view.SelectedItems;
                    if (selectedItems.Count > 1)
                        {
                        responseText_mv.Text = "Невозможно редактировать более 1 строки.";
                        break;
                        }
                    if (selectedItems.Count == 0)
                        {
                        responseText_mv.Text = "Выберите строку для редактирования.";
                        break;
                        }
                    index = Convert.ToString((selectedItems[0] as DataRowView)[0]);
                    mw = new UpdWindow(Tables.full_ticket_view, worker, index);
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
                        responseText_st.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var icao_code = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_model('{icao_code}');");
                            //responseText_st.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_st.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_pln":
                    selectedItems = DataGrid_pln.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_gr.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var registration_number = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_plane('{registration_number}');");
                            //responseText_gr.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_gr.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_fl":
                    selectedItems = DataGrid_fl.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_mr.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var flight_id = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_flight('{flight_id}');");
                            //responseText_mr.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_mr.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_tck":
                    selectedItems = DataGrid_tck.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_sj.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var etkt = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_ticket('{etkt}')");
                            //responseText_sj.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_sj.Text = exception.Message;
                            break;
                            }
                        }
                    update_all_db();
                    break;
                case "btn_del_pers":
                    selectedItems = DataGrid_pers.SelectedItems;
                    if (selectedItems.Count == 0)
                        {
                        responseText_dp.Text = "Выберите строки для удаления.";
                        break;
                        }
                    if (!confirmDelete()) break;
                    foreach (var item in selectedItems)
                        {
                        var passport_id = Convert.ToString((item as DataRowView)[0]).ToUpper();
                        try
                            {
                            var response = worker.Query($"CALL delete_person('{passport_id}');");
                            //responseText_dp.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        catch (Npgsql.PostgresException exception)
                            {
                            responseText_dp.Text = exception.Message;
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
                        "SELECT marks.id AS mark_id, " +
                        "s.surname, " +
                        "s.name, " +
                        "s.middle_name, " +
                        "sj.title AS subject, " +
                        "CASE " +
                            "WHEN mark = 5 THEN 'Отлично' " +
                            "WHEN mark = 4 THEN 'Хорошо' " +
                            "WHEN mark = 3 THEN 'Удовлетворительно' " +
                            "WHEN mark = 2 THEN 'Пересдача' END AS mark " +
                        "FROM marks " +
                            "JOIN students s ON marks.student_id = s.id " +
                            "JOIN subjects sj ON marks.subject_id = sj.id " +
                        "ORDER BY surname;");
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 1:
                    MessageBox.Show("Выполнение задания 3b - во вкладке MVIEW", "Внимание");
                    break;
                case 2:
                    dataset = worker.Query(
                        "SELECT (SELECT surname FROM students WHERE surname = 'Иванов' LIMIT 1);"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 3:
                    dataset = worker.Query(
                        "SELECT * FROM (SELECT surname, name, group_id FROM students) AS m " +
                        "WHERE m.group_id = 3; ");
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 4:
                    dataset = worker.Query(
                        "SELECT count(mark) AS resits FROM marks " +
                        "WHERE mark = (SELECT MIN(mark) FROM marks); "
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 5:
                    dataset = worker.Query(
                        "SELECT surname, name, " +
                            "(SELECT cypher FROM \"groups\" WHERE id = group_id) AS \"group\" " +
                        "FROM students;"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 6:
                    dataset = worker.Query(
                        "SELECT * FROM \"groups\" g WHERE NOT " +
                            "exists(SELECT * FROM students s WHERE g.id = s.group_id) " +
                            "ORDER BY id;"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 7:
                    dataset = worker.Query(
                        "SELECT id, mark, passes, " +
                           "(SELECT surname FROM students WHERE student_id = students.id), " +
                           "(SELECT title FROM subjects WHERE subject_id = subjects.id) " +
                        "FROM marks; "
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 8:
                    dataset = worker.Query(
                        "SELECT surname, name, MIN(mark) as min_mark " +
                        "FROM marks JOIN students s on marks.student_id = s.id " +
                        "GROUP BY surname, name " +
                        "HAVING MIN(mark) = 3;"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 9:
                    dataset = worker.Query(
                        "SELECT * FROM marks_view WHERE group_name = ANY('{БИСО-01-20,БИСО-02-20}');"
                    );
                    if (dataset != null)
                        DataGrid_tasks.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case 10:
                    dataset = worker.Query(
                        "SELECT cypher, student_count " +
                        "FROM \"groups\" " +
                        "WHERE student_count > ALL(SELECT AVG(student_count) FROM \"groups\");"
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
                case "btn_rel_st":
                    var dataset = worker.Query("SELECT * FROM plane_model ORDER BY icao_code;");
                    if (dataset != null)
                        DataGrid_pln_mdl.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_gr":
                    dataset = worker.Query("SELECT * FROM plane ORDER BY registration_number;");
                    if (dataset != null)
                        DataGrid_pln.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_mr":
                    dataset = worker.Query("SELECT * FROM flight ORDER BY flight_id;");
                    if (dataset != null)
                        DataGrid_fl.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_sj":
                    dataset = worker.Query("SELECT * FROM ticket ORDER BY etkt;");
                    if (dataset != null)
                        DataGrid_tck.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_dp":
                    dataset = worker.Query("SELECT * FROM person ORDER BY passport_id;");
                    if (dataset != null)
                        DataGrid_pers.ItemsSource = dataset.Tables[0].DefaultView;
                    break;
                case "btn_rel_mv":
                    dataset = worker.Query("SELECT * FROM full_ticket_view;");
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
        private void toggleTheme(object sender, RoutedEventArgs e)
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
            }

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
        }
    }
