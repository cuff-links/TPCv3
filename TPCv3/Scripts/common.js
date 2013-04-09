$(function() {
    $('#search-form').submit(function() {
        if ($('#txtSearch').val().trim()) {
            return true;
        } else {
            return false;
        }
    });
})