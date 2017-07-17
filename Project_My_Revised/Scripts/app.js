$(document).ready(function () {
    $('#Start_date').datepicker({
        onSelect: function (dateText, inst) {
            $('#End_date').datepicker('option', 'minDate', new Date(dateText));
        },
    });

    $('#End_date').datepicker({
        onSelect: function (dateText, inst) {
            $('#Start_date').datepicker('option', 'maxDate', new Date(dateText));
        }
    });
});