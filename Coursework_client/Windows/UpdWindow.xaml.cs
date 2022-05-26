﻿using MaterialDesignThemes.Wpf;
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
        #region constants
        private static readonly string[][] hints =
        {
            new string[] { "Код модели", "Название модели", "Тип самолета", "Двигатель", "Численность экипажа", "Длина(м)","Размах крыльев(м)", "Высота(м)", "Ширина салона(м)", 
                         "Максимальная грузоподъемность(кг)","Пассажироподъемность", "Скорость(км/ч)", "Дальность полета(м)","Лимит высоты(м)", "Влетная дистанция(м)"},
            new string[] { "Код самолета", "Код модели", "Производитель", "Год выпуска", "Владелец", "Статус", "Последнее обновление статуса" },
            new string[] { "Код рейса", "Код самолета", "Место отправления", "Место прибытия", "Время отбытия(по расписанию)", "Время отбытия", "Время прибытия(по расписанию)", "Время прибытия",
                         "Терминал", "Выход", "Ремарка"},
            new string[] { "Код билета", "Код самолета", "Код рейса", "Код паспорта", "Класс", "Цена($)", "Место"},
            new string[] { "Код паспорта", "Фамилия","Имя", "Отчество", "Код паспорта", "Дата рождения", "Пол" },
            new string[] { "Код паспорта", "Код билета", "Код самолета", "Код рейса", "Класс", "Цена($)", "Место", "Фамилия", "Имя", "Отчество", "Код паспорта", "Дата рождения", "Пол" }
        };

        private static readonly string[][] tooltips =
        {
            new string[] { "Введите код модели", "Введите название модели",
                           "Введите тип самолета", "Введите название двигателя",
                           "Введите численность экипажа", "Введите длину(м)",
                           "Введите размах крыльев(м)","Введите высоту(м)",
                           "Введите ширину салона(м)","Введите максимальную грузоподъемность(кг)","Введите пассажироподъемность",
                           "Введите скорость(км/ч)","Введите дальность полета(м)","Введите лимит высоты(м)","Введите влетную дистанцию(м)"},

            new string[] { "Введите код самолета", "Введите код модели","Введите производителя", "Введите год выпуска",
                           "Введите владелеца","Введите владелеца", "Введите статус", "Статус обновляется автоматически"},

            new string[] { "Введите код рейса", "Введите код самолета", "Введите место отправления", "Введите место прибытия", "Введите время отбытия(по расписанию)",
                         "Введите время отбытия", "Введите время прибытия(по расписанию)", "Введите время прибытия",
                         "Введите терминал", "Введите выход", "Введите ремарку"},

            new string[] { "Введите код билета", "Введите код самолета", "Введите код рейса", "Введите код паспорта", "Введите класс", "Введите цену($)", "Введите место"},

            new string[] { "Введите код паспорта", "Введите фамилия", "Введите имя", "Введите отчество", "Введите код паспорта", "Введите дату рождения", "Введите пол"},
            new string[] { "Введите код паспорта", "Введите код билета", "Введите код самолета", "Введите код рейса", "Введите класс", "Введите цену($)", "Введите место", "Введите фамилия",
                         "Введите имя", "Введите отчество", "Введите код паспорта", "Введите дату рождения", "Введите пол"}
        };

        private static readonly string[] inputs =
        {
            "id_box", "first", "second", "third",
            "fourth", "fifth","sixth", "seventh", "eighth", "ninth",
            "tenth", "eleventh","twelwe", "thirteenth","fourteenth"
        };

        private static readonly string[][] collapes =
        {
            new string[] { "id_box", "first"},
            new string[] { "second", "third" },
            new string[] { "fourth", "fifth"},
            new string[] { "sixth", "seventh" },
            new string[] { "eighth", "ninth"},
            new string[] { "tenth", "eleventh" },
            new string[] { "twelwe", "thirteenth" },
            new string[] { "fourteenth"},
        };
        #endregion

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
            switch (DataType)
                {
                case Tables.plane_model:
                    setCollapsed();
                    //setHintsAndTooltips();

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

                    break;

               case Tables.plane:
                    setCollapsed();
                    //setHintsAndTooltips();

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;
                    sixth.MaxLength = 128;

                    DataSet plane = Worker.Query($"SELECT * FROM plane WHERE registration_number= '{UpdatingId}';");
                    var pln = plane.Tables[0].Rows[0];
                    id_box.Text = UpdatingId.ToString();
                    first.Text = pln[1].ToString();
                    second.Text = pln[2].ToString();
                    third.Text = pln[3].ToString();
                    fourth.Text = pln[4].ToString();
                    fifth.Text = pln[5].ToString();
                    sixth.Text = pln[6].ToString();

                    //Height = 200;
                    break;
                case Tables.flight:
                    setCollapsed();
                    //setHintsAndTooltips();

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
                    break;

                case Tables.ticket:
                    setCollapsed();
                    //setHintsAndTooltips();

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;
                    sixth.MaxLength = 128;

                    DataSet ticket = Worker.Query($"SELECT * FROM ticket WHERE etkt= '{UpdatingId}';");
                    var tck = ticket.Tables[0].Rows[0];
                    id_box.Text = UpdatingId.ToString();
                    first.Text = tck[1].ToString();
                    second.Text = tck[2].ToString();
                    third.Text = tck[3].ToString();
                    fourth.Text = tck[4].ToString();
                    fifth.Text = tck[5].ToString();
                    sixth.Text = tck[6].ToString();
                    seventh.Text = tck[7].ToString();
                    eight.Text = tck[8].ToString();
                    ninth.Text = tck[9].ToString();
                    tenth.Text = tck[10].ToString();
                    break;

                case Tables.person:
                    setCollapsed();
                    //setHintsAndTooltips();

                    id_box.MaxLength = 128;
                    first.MaxLength = 128;
                    second.MaxLength = 128;
                    third.MaxLength = 128;
                    fourth.MaxLength = 128;
                    fifth.MaxLength = 128;
                        
                    DataSet person = Worker.Query($"SELECT * FROM subjects WHERE id={UpdatingId};");
                    var pers = person.Tables[0].Rows[0];
                    id_box.Text = UpdatingId.ToString();
                    first.Text = pers[1].ToString();
                    second.Text = pers[2].ToString();
                    third.Text = pers[3].ToString();
                    fourth.Text = pers[4].ToString();
                    fifth.Text = pers[5].ToString();
                    //Height = 170;
                    break;

                case Tables.full_ticket_view:
                    grid.RowDefinitions.Add(new RowDefinition());
                    setHintsAndTooltips();
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

                    Height += 50;
                    break;
                }
            }

        private void setHintsAndTooltips()
            {
            int i = 0;
            foreach (string input in inputs)
                {
                var inp = grid.FindName(input) as Control;
                if (inp != null && inp.Visibility != Visibility.Collapsed)
                    {
                    inp.ToolTip = tooltips[(int)DataType][i];
                    HintAssist.SetHint(inp, hints[(int)DataType][i++]);
                    }
                }
            }

        private void setCollapsed()
            {
            foreach (string input in collapes[(int)DataType])
                {
                var inp = grid.FindName(input) as Control;
                if (inp != null)
                    inp.Visibility = Visibility.Collapsed;
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
                    var length_m = Convert.ToDouble(fifth.Text);
                    var wingspan_m = Convert.ToDouble(sixth.Text);
                    var height_m = Convert.ToDouble(seventh.Text);
                    var interior_width_m = Convert.ToDouble(eight.Text);
                    var maximal_takeoff_weight_kg = Convert.ToInt32(ninth.Text);
                    var capacity_of_passengers = Convert.ToInt32(tenth.Text);
                    var cruising_speed_km_per_h = Convert.ToDouble(eleventh.Text);
                    var flight_distance_m = Convert.ToInt32(twelwe.Text);
                    var height_limit_m = Convert.ToInt32(thirteenth.Text);
                    var takeoff_distance_m = Convert.ToInt32(fourteenth.Text);


                    if (string.IsNullOrWhiteSpace(icao_code)
                        || string.IsNullOrWhiteSpace(model_name)
                        || string.IsNullOrWhiteSpace(plane_type)
                        || string.IsNullOrWhiteSpace(engine)
                        || (crew == 0)
                        || (length_m == 0)
                        || (wingspan_m == 0)
                        || (height_m == 0)
                        || (interior_width_m == 0)
                        || (maximal_takeoff_weight_kg == 0)
                        || (capacity_of_passengers == 0)
                        || (cruising_speed_km_per_h == 0)
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
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_model('{UpdatingId}', " +
                                $"'{model_name}', '{plane_type}', '{engine}', {crew}, {length_m}, {wingspan_m}, {height_m}," +
                                $" {interior_width_m},{maximal_takeoff_weight_kg},{capacity_of_passengers}," +
                                $" {cruising_speed_km_per_h},{flight_distance_m},{height_limit_m},{takeoff_distance_m})");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
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
                            var response = Worker.Query($"CALL insert_plane('{registration_number}','{model}' " +
                                $"'{manufacturer}',{year_of_issue},'{owner}','{status}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_plane('{UpdatingId}','{model}' " +
                                $"'{manufacturer}',{year_of_issue},'{owner}','{status}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
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

                    try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_flight('{flight_id}', " +
                                $"'{plane_registration_number}', '{departure_point}', '{arrival_point}'," +
                                $"'{departure_time_scheduled}','{departure_time_actual}',{arrival_time_scheduled}" +
                                $"'{arrival_time_actual}','{terminal}','{gate}','{remark}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_flight('{UpdatingId}'," +
                                $"'{plane_registration_number}', '{departure_point}', '{arrival_point}'," +
                                $"'{departure_time_scheduled}','{departure_time_actual}',{arrival_time_scheduled}" +
                                $"'{arrival_time_actual}','{terminal}','{gate}','{remark}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
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
                    var price = Convert.ToDouble(fifth.Text);
                    var seat = sixth.Text;


                    if (string.IsNullOrWhiteSpace(etkt)
                        || string.IsNullOrWhiteSpace(plane_reg_number)
                        || string.IsNullOrWhiteSpace(flight_code)
                        || string.IsNullOrWhiteSpace(passport_code)
                        || string.IsNullOrWhiteSpace(ticket_class)
                        || (price == 0)
                        || string.IsNullOrWhiteSpace(seat))
                        {
                        responseText.Text = "Заполните необходимые поля!";
                        return;
                        }

                    try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_ticket('{etkt}', " +
                                $"'{plane_reg_number}', '{flight_code}', '{passport_code}', '{ticket_class}', {price},'{seat}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_ticket('{UpdatingId}', " +
                               $"'{plane_reg_number}', '{flight_code}', '{passport_code}', '{ticket_class}', {price},'{seat}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
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

                        try
                        {
                        if (!IsUpdating)
                            {
                            var response = Worker.Query($"CALL insert_person('{passport_id}','{last_name}','{first_name}'," +
                                $"'{father_name}','{date_of_birth}','{sex}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
                            }
                        else
                            {
                            var response = Worker.Query($"CALL update_person('{UpdatingId}','{last_name}','{first_name}'," +
                                $"'{father_name}','{date_of_birth}','{sex}');");
                            responseText.Text = response.Tables[0].Rows[0][0].ToString();
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

                case Tables.full_ticket_view:
                    passport_id = id_box.Text;
                    etkt = first.Text;
                    plane_registration_number = second.Text;
                    flight_id = third.Text;
                    ticket_class = fourth.Text;
                    price = Convert.ToDouble(fifth.Text);
                    last_name = sixth.Text;
                    first_name = seventh.Text;
                    father_name = eight.Text;
                    date_of_birth = tenth.Text;
                    sex = eleventh.Text;

                    try
                        {
                        var response = Worker.Query($"SELECT update_marks_view('{UpdatingId}', " +
                            $"'{passport_id}', '{etkt}', '{plane_registration_number}', '{flight_id}', '{ticket_class}', '{price}', '{last_name}'," +
                            $"'{first_name}', '{father_name}', '{date_of_birth}','{sex}');");
                        responseText.Text = response.Tables[0].Rows[0][0].ToString();
                        StartCloseTimer();
                        btn_confirm.IsEnabled = false;
                        break;
                        }
                    catch (Npgsql.PostgresException exception)
                        {
                        responseText.Text = exception.Message;
                        break;
                        }
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
