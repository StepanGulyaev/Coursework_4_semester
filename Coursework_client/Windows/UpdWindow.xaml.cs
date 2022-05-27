using MaterialDesignThemes.Wpf;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Coursework_client.DB;

namespace Coursework_client
    {
    public partial class UpdWindow : Window
        {

        #region fields
        private Tables DataType { get; init; }
        private bool IsUpdating { get; init; } = false;
        private string UpdatingId { get; init; }
        private User Worker { get; init; } = default!;
        #endregion

        #region UpdWindow constructors
        private UpdWindow()
            {
            InitializeComponent();
            SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            if ((new PaletteHelper()).GetTheme().GetBaseTheme() == BaseTheme.Dark)
                bgcard.Background = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            }

        public UpdWindow(Tables dataType, User worker)
            : this()
            {
            DataType = dataType;
            windowTitle.Text = $"{windowTitle.Text} {dataType}";
            Worker = worker;
            setInputs();
            }

        public UpdWindow(Tables dataType, User worker, string updatingId)
            : this()
            {
            DataType = dataType;
            windowTitle.Text = $"Изменение {dataType}";
            IsUpdating = true;
            UpdatingId = updatingId;
            Worker = worker;
            setInputs();
            }
        #endregion

        #region Configuring form inputs
        private void setInputs()
            {
            if (IsUpdating)
                {
                id_box.Visibility = Visibility.Collapsed;
                }
            switch (DataType)
                {
                case Tables.plane_model:

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;
                    sixth.MaxLength = 128;
                    seventh.MaxLength = 128;
                    eight.MaxLength = 128;
                    ninth.MaxLength = 128;
                    tenth.MaxLength = 128;
                    eleventh.MaxLength = 128;
                    twelwe.MaxLength = 128;
                    thirteenth.MaxLength = 128;
                    fourteenth.MaxLength = 128;

                    if (IsUpdating)
                        {
                        DataSet plane_model = Worker.Query($"SELECT * FROM plane_model WHERE icao_code= '{UpdatingId}';");
                        var model = plane_model.Tables[0].Rows[0];
                        id_box.Text = UpdatingId.ToString();
                        first.Text = model[1].ToString();
                        second.Text = model[2].ToString();
                        third.Text = model[3].ToString();
                        fourth.Text = model[4].ToString();
                        fifth.Text = model[5].ToString();
                        sixth.Text = model[6].ToString();
                        seventh.Text = model[7].ToString();
                        eight.Text = model[8].ToString();
                        ninth.Text = model[9].ToString();
                        tenth.Text = model[10].ToString();
                        eleventh.Text = model[11].ToString();
                        twelwe.Text = model[12].ToString();
                        thirteenth.Text = model[13].ToString();
                        fourteenth.Text = model[14].ToString();
                        }
                    break;

                case Tables.plane:

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;

                    if (IsUpdating)
                        {
                        DataSet plane = Worker.Query($"SELECT * FROM plane WHERE registration_number= '{UpdatingId}';");
                        var pln = plane.Tables[0].Rows[0];
                        id_box.Text = UpdatingId.ToString();
                        first.Text = pln[1].ToString();
                        second.Text = pln[2].ToString();
                        third.Text = pln[3].ToString();
                        fourth.Text = pln[4].ToString();
                        fifth.Text = pln[5].ToString();
                        }

                    break;

                case Tables.flight:

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;
                    sixth.MaxLength = 128;
                    seventh.MaxLength = 128;
                    eight.MaxLength = 128;
                    ninth.MaxLength = 128;
                    tenth.MaxLength = 128;

                    if (IsUpdating)
                        {
                        DataSet flight = Worker.Query($"SELECT * FROM flight WHERE flight_id= '{UpdatingId}';");
                        var fl = flight.Tables[0].Rows[0];
                        id_box.Text = UpdatingId.ToString();
                        first.Text = fl[1].ToString();
                        second.Text = fl[2].ToString();
                        third.Text = fl[3].ToString();
                        fourth.Text = fl[4].ToString();
                        fifth.Text = fl[5].ToString();
                        sixth.Text = fl[6].ToString();
                        seventh.Text = fl[7].ToString();
                        eight.Text = fl[8].ToString();
                        ninth.Text = fl[9].ToString();
                        tenth.Text = fl[10].ToString();
                        }
                    break;

                case Tables.ticket:

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;
                    sixth.MaxLength = 128;

                    if (IsUpdating)
                        {
                        DataSet ticket = Worker.Query($"SELECT * FROM ticket WHERE etkt = '{UpdatingId}';");
                        var tck = ticket.Tables[0].Rows[0];
                        id_box.Text = UpdatingId.ToString();
                        first.Text = tck[1].ToString();
                        second.Text = tck[2].ToString();
                        third.Text = tck[3].ToString();
                        fourth.Text = tck[4].ToString();
                        fifth.Text = tck[5].ToString();
                        sixth.Text = tck[6].ToString();
                        }
                    break;

                case Tables.person:

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;

                    if (IsUpdating)
                        {
                        DataSet person = Worker.Query($"SELECT * FROM person WHERE passport_id= '{UpdatingId}';");
                        var pers = person.Tables[0].Rows[0];
                        id_box.Text = UpdatingId.ToString();
                        first.Text = pers[1].ToString();
                        second.Text = pers[2].ToString();
                        third.Text = pers[3].ToString();
                        fourth.Text = pers[4].ToString();
                        fifth.Text = pers[5].ToString();
                        }
                    break;

                case Tables.full_ticket_view:
                    grid.RowDefinitions.Add(new RowDefinition());
                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;
                    sixth.MaxLength = 128;
                    seventh.MaxLength = 128;
                    eight.MaxLength = 128;
                    ninth.MaxLength = 128;
                    tenth.MaxLength = 128;
                    eleventh.MaxLength = 128;

                    if (IsUpdating)
                        {
                        DataSet full_ticket_view = Worker.Query($"SELECT passport_id, etkt, plane_registration_number,flight_id,class,price_$,seat,last_name" +
                        $"first_name,father_name, date_of_birth, sex FROM full_ticket_view " +
                        $"WHERE etkt = '{UpdatingId}';");
                        var ftv = full_ticket_view.Tables[0].Rows[0];
                        id_box.Text = UpdatingId.ToString();
                        first.Text = ftv[1].ToString();
                        second.Text = ftv[2].ToString();
                        third.Text = ftv[3].ToString();
                        fourth.Text = ftv[4].ToString();
                        fifth.Text = ftv[5].ToString();
                        sixth.Text = ftv[6].ToString();
                        seventh.Text = ftv[7].ToString();
                        eight.Text = ftv[8].ToString();
                        ninth.Text = ftv[9].ToString();
                        tenth.Text = ftv[10].ToString();
                        eleventh.Text = ftv[11].ToString();
                        }

                    break;
                }
            }


        #endregion

        private void makeChanges(object sender, RoutedEventArgs e)
            {
            switch (DataType)
                {
                case Tables.plane_model:
                    var icao_code = id_box.Text;
                    var model_name = first.Text;
                    var plane_type = second.Text;
                    var engine = third.Text;
                    var crew = Convert.ToInt32(fourth.Text);
                    var length_m = Convert.ToString(fifth.Text.Replace(',', '.'));
                    var wingspan_m = Convert.ToString(sixth.Text.Replace(',', '.'));
                    var height_m = Convert.ToString(seventh.Text.Replace(',', '.'));
                    var interior_width_m = Convert.ToString(eight.Text.Replace(',', '.'));
                    var maximal_takeoff_weight_kg = Convert.ToInt32(ninth.Text);
                    var capacity_of_passengers = Convert.ToInt32(tenth.Text);
                    var cruising_speed_km_per_h = Convert.ToString(eleventh.Text.Replace(',', '.'));
                    var flight_distance_m = Convert.ToInt32(twelwe.Text);
                    var height_limit_m = Convert.ToInt32(thirteenth.Text);
                    var takeoff_distance_m = Convert.ToInt32(fourteenth.Text);


                    if (string.IsNullOrWhiteSpace(icao_code)
                        || string.IsNullOrWhiteSpace(model_name)
                        || string.IsNullOrWhiteSpace(plane_type)
                        || string.IsNullOrWhiteSpace(engine)
                        || (crew == 0)
                        || string.IsNullOrWhiteSpace(length_m)
                        || string.IsNullOrWhiteSpace(wingspan_m)
                        || string.IsNullOrWhiteSpace(height_m)
                        || string.IsNullOrWhiteSpace(interior_width_m)
                        || (maximal_takeoff_weight_kg == 0)
                        || (capacity_of_passengers == 0)
                        || string.IsNullOrWhiteSpace(cruising_speed_km_per_h)
                        || (flight_distance_m == 0)
                        || (height_limit_m == 0)
                        || (takeoff_distance_m == 0))
                        {
                        responseText.Text = "Заполните необходимые поля!";
                        return;
                        }

                    try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_model('{icao_code}', " +
                                $"'{model_name}', '{plane_type}', '{engine}', {crew}, {length_m}, {wingspan_m}, {height_m}," +
                                $" {interior_width_m},{maximal_takeoff_weight_kg},{capacity_of_passengers}," +
                                $" {cruising_speed_km_per_h},{flight_distance_m},{height_limit_m},{takeoff_distance_m})");
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_model('{UpdatingId}', " +
                                $"'{model_name}', '{plane_type}', '{engine}', {crew}, {length_m}, {wingspan_m}, {height_m}," +
                                $" {interior_width_m},{maximal_takeoff_weight_kg},{capacity_of_passengers}," +
                                $" {cruising_speed_km_per_h},{flight_distance_m},{height_limit_m},{takeoff_distance_m})");
                            StartCloseTimer();
                            btn_confirm.IsEnabled = false;
                            break;
                            }
                        }
                    catch (Npgsql.PostgresException exception)
                        {
                        responseText.Text = exception.Message;
                        break;
                        }
                    id_box.Clear();
                    first.Clear();
                    second.Clear();
                    third.Clear();
                    fourth.Clear();
                    fifth.Clear();
                    sixth.Clear();
                    seventh.Clear();
                    eight.Clear();
                    ninth.Clear();
                    tenth.Clear();
                    eleventh.Clear();
                    twelwe.Clear();
                    thirteenth.Clear();
                    fourteenth.Clear();
                    break;

                case Tables.plane:
                    var registration_number = id_box.Text;
                    var model = first.Text;
                    var manufacturer = second.Text;
                    var year_of_issue = Convert.ToInt32(third.Text);
                    var owner = fourth.Text;
                    var status = fifth.Text;
                   

                    if (string.IsNullOrWhiteSpace(registration_number)
                        || string.IsNullOrWhiteSpace(model)
                        || string.IsNullOrWhiteSpace(manufacturer)
                        || (year_of_issue == 0)
                        || string.IsNullOrWhiteSpace(owner)
                        || string.IsNullOrWhiteSpace(status))
                        {
                        responseText.Text = "Заполните необходимые поля!";
                        return;
                        }

                    try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_plane('{registration_number}','{model}','{manufacturer}',{year_of_issue},'{owner}','{status}');");
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_plane('{UpdatingId}','{model}','{manufacturer}',{year_of_issue},'{owner}','{status}');");
                            StartCloseTimer();
                            btn_confirm.IsEnabled = false;
                            break;
                            }
                        }
                    catch (Npgsql.PostgresException exception)
                        {
                        responseText.Text = exception.Message;
                        break;
                        }

                    id_box.Clear();
                    first.Clear();
                    second.Clear();
                    third.Clear();
                    fourth.Clear();
                    fifth.Clear();
                    break;

                case Tables.flight:
                    var flight_id = id_box.Text;
                    var plane_registration_number = first.Text;
                    var departure_point = second.Text;
                    var arrival_point = third.Text;
                    var departure_time_scheduled = fourth.Text;
                    var departure_time_actual = fifth.Text;
                    var arrival_time_scheduled = sixth.Text;
                    var arrival_time_actual = seventh.Text;
                    var terminal = eight.Text;
                    var gate = ninth.Text;
                    var remark = tenth.Text;

                    if (string.IsNullOrWhiteSpace(flight_id)
                        || string.IsNullOrWhiteSpace(plane_registration_number)
                        || string.IsNullOrWhiteSpace(departure_point)
                        || string.IsNullOrWhiteSpace(arrival_point)
                        || string.IsNullOrWhiteSpace(departure_time_scheduled)
                        || string.IsNullOrWhiteSpace(arrival_time_scheduled))
                        {
                        responseText.Text = "Заполните необходимые поля!";
                        return;
                        }

                    if(!string.IsNullOrWhiteSpace(departure_time_actual))
                        {
                        departure_time_actual = $"'{departure_time_actual}'";
                        }
                    else
                        {
                        departure_time_actual = "null";
                        }

                    if (!string.IsNullOrWhiteSpace(arrival_time_actual))
                        {
                        arrival_time_actual = $"'{arrival_time_actual}'";
                        }
                    else
                        {
                        arrival_time_actual = "null";
                        }

                    if (!string.IsNullOrWhiteSpace(terminal))
                        {
                        terminal = $"'{terminal}'";
                        }
                    else
                        {
                        terminal = "null";
                        }

                    if (!string.IsNullOrWhiteSpace(gate))
                        {
                        gate = $"'{gate}'";
                        }
                    else
                        {
                        gate= "null";
                        }

                    if (!string.IsNullOrWhiteSpace(remark))
                        {
                        remark = $"'{remark}'";
                        }
                    else
                        {
                        remark = "null";
                        }


                    try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_flight('{flight_id}'," +
                                $"'{plane_registration_number}','{departure_point}', '{arrival_point}'," +
                                $"'{departure_time_scheduled}',{departure_time_actual},'{arrival_time_scheduled}'," +
                                $"{arrival_time_actual},{terminal},{gate},{remark});");
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_flight('{UpdatingId}'," +
                                $"'{plane_registration_number}', '{departure_point}','{arrival_point}'," +
                                $"'{departure_time_scheduled}',{departure_time_actual},'{arrival_time_scheduled}'," +
                                $"{arrival_time_actual},{terminal},{gate},{remark});");
                            StartCloseTimer();
                            btn_confirm.IsEnabled = false;
                            break;
                            }
                        }
                    catch (Npgsql.PostgresException exception)
                        {
                        responseText.Text = exception.MessageText;
                        break;
                        }

                    id_box.Clear();
                    first.Clear();
                    second.Clear();
                    third.Clear();
                    fourth.Clear();
                    fifth.Clear();
                    sixth.Clear();
                    seventh.Clear();
                    eight.Clear();
                    ninth.Clear();
                    tenth.Clear();
                    break;

                case Tables.ticket:

                    var etkt = id_box.Text;
                    var plane_reg_number = first.Text;
                    var flight_code = second.Text;
                    var passport_code = third.Text;
                    var ticket_class = fourth.Text;
                    var price = Convert.ToString(fifth.Text.Replace(',', '.'));
                    var seat = sixth.Text;


                    if (string.IsNullOrWhiteSpace(etkt)
                        || string.IsNullOrWhiteSpace(plane_reg_number)
                        || string.IsNullOrWhiteSpace(flight_code)
                        || string.IsNullOrWhiteSpace(passport_code)
                        || string.IsNullOrWhiteSpace(ticket_class)
                        || string.IsNullOrWhiteSpace(price)
                        || string.IsNullOrWhiteSpace(seat))
                        {
                        responseText.Text = "Заполните необходимые поля!";
                        return;
                        }

                    try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_ticket('{etkt}'," +
                                $"'{plane_reg_number}', '{flight_code}','{passport_code}','{ticket_class}',{price},'{seat}');");
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_ticket('{UpdatingId}', " +
                               $"'{plane_reg_number}', '{flight_code}', '{passport_code}', '{ticket_class}', {price},'{seat}');");
                            StartCloseTimer();
                            btn_confirm.IsEnabled = false;
                            break;
                            }
                        }
                    catch (Npgsql.PostgresException exception)
                        {
                        responseText.Text = exception.Message;
                        break;
                        }

                    id_box.Clear();
                    first.Clear();
                    second.Clear();
                    third.Clear();
                    fourth.Clear();
                    fifth.Clear();
                    sixth.Clear();
                    break;

                case Tables.person:

                    var passport_id = id_box.Text;
                    var last_name = first.Text;
                    var first_name = second.Text;
                    var father_name = third.Text;
                    var date_of_birth = fourth.Text;
                    var sex = fifth.Text;

                    if (string.IsNullOrWhiteSpace(passport_id)
                        || string.IsNullOrWhiteSpace(last_name)
                        || string.IsNullOrWhiteSpace(first_name)
                        || string.IsNullOrWhiteSpace(date_of_birth)
                        || string.IsNullOrWhiteSpace(sex))
                        {
                        responseText.Text = "Заполните необходимые поля!";
                        return;
                        }

                    if (!string.IsNullOrWhiteSpace(father_name))
                        {
                        father_name = $"'{father_name}'";
                        }
                    else
                        {
                        father_name = "null";
                        }

                    try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_person('{passport_id}','{last_name}','{first_name}'," +
                                $"{father_name},'{date_of_birth}','{sex}');");
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_person('{UpdatingId}','{last_name}','{first_name}'," +
                                $"{father_name},'{date_of_birth}','{sex}');");
                            StartCloseTimer();
                            btn_confirm.IsEnabled = false;
                            break;
                            }
                        }
                    catch (Npgsql.PostgresException exception)
                        {
                        responseText.Text = exception.Message;
                        break;
                        }

                    id_box.Clear();
                    first.Clear();
                    second.Clear();
                    third.Clear();
                    fourth.Clear();
                    fifth.Clear();
                    break;
                }
            }

        #region utils
        private void closeWindow(object sender, RoutedEventArgs e) =>
            Close();

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
            {
            base.OnMouseLeftButtonDown(e);
            DragMove();
            }
            

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
            {
            Regex regex = new Regex("^[0-9]+$");
            e.Handled = !regex.IsMatch(e.Text);
            }

        private void MarkValidationTextBox(object sender, TextCompositionEventArgs e)
            {
            Regex regex = new Regex("^[2-5]$");
            e.Handled = !regex.IsMatch(e.Text);
            }

        private void TextValidationTextBox(object sender, TextCompositionEventArgs e)
            {
            Regex regex = new Regex(@"^[a-zA-Zа-яА-Я\s] *$");
            e.Handled = !regex.IsMatch(e.Text);
            }

        private void StartCloseTimer()
            {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3d);
            timer.Tick += TimerTick;
            timer.Start();
            }

        private void TimerTick(object sender, EventArgs e)
            {
            var timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            Close();
            }
        #endregion
        }
    }
