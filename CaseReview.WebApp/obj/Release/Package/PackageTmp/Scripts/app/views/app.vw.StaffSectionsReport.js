//bootstrap-daterangepicker -->

app.view.updateLabelClass = function (cbx) {
    var isChecked = $(cbx).prop("checked")
    $(cbx).parent().removeClass("on");
    if (isChecked) {
        $(cbx).parent().addClass("on");
    }
    
};

app.view.toggleStaff = function (cbx) {
    var isChecked = $(cbx).prop("checked")
    var checkBoxes = $(".staffCheckBox");
    $(checkBoxes).each(function () {
        $(this).prop("checked", isChecked);
        app.view.updateLabelClass(this);
    });
};

app.view.toggleSection = function (cbx) {
    var isChecked = $(cbx).prop("checked")
    var checkBoxes = $(".sectionCheckBox");
    $(checkBoxes).each(function () {
        $(this).prop("checked", isChecked);
        app.view.updateLabelClass(this);
    });
};


$(document).ready(function () {

    var cb = function (start, end, label) {
        console.log(start.toISOString(), end.toISOString(), label);
        $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    };

    var optionSet1 = {
        startDate: moment(reDpStart),
        endDate: moment(reDpEnd),
        minDate: '01/01/2014',
        maxDate: '31/12/2020',
        //dateLimit: {days: 60},
        showDropdowns: true,
        showWeekNumbers: true,
        timePicker: false,
        timePickerIncrement: 1,
        timePicker12Hour: true,
        ranges: {
            //'Today': [moment(), moment()],
            //'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'All of time': [moment().subtract(99, 'years'), moment()],
            'Last selection': [moment(reDpStart), moment(reDpEnd)],
            //'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        opens: 'left',
        buttonClasses: ['btn btn-default'],
        applyClass: 'btn-small btn-primary',
        cancelClass: 'btn-small',
        format: 'DD/MM/YYYY',
        separator: ' to ',
        locale: {
            applyLabel: 'Submit',
            cancelLabel: 'Clear',
            fromLabel: 'From',
            toLabel: 'To',
            customRangeLabel: 'Custom',
            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            firstDay: 1
        }
    };


    // $('#reportrange span').html(moment().subtract(29, 'days').format('D MMMM, YYYY') + ' - ' + moment().format('D MMMM, YYYY'));

    $('#reportrange').daterangepicker(optionSet1, cb);
    $('#reportrange span').html(moment(reDpStart).format('MMMM D, YYYY') + ' - ' + moment(reDpEnd).format('MMMM D, YYYY'));

    //cb(moment(reDpStart), moment(reDpEnd));


    $('#reportrange').on('show.daterangepicker', function (ev, cb) {

        console.log("show event fired");
    });
    $('#reportrange').on('hide.daterangepicker', function () {
        console.log("hide event fired");
    });
    $('#reportrange').on('apply.daterangepicker', function (ev, picker) {
        $("#from").val(picker.startDate.format('DD-MM-YYYY'))
        $("#to").val(picker.endDate.format('DD-MM-YYYY'))
        console.log("apply event fired, start/end dates are " + picker.startDate.format('MMMM D, YYYY') + " to " + picker.endDate.format('MMMM D, YYYY'));
    });
    $('#reportrange').on('cancel.daterangepicker', function (ev, picker) {
        console.log("cancel event fired");
    });
    $('#destroy').click(function () {
        $('#reportrange').data('daterangepicker').remove();
    });
});

//bootstrap-daterangepicker



