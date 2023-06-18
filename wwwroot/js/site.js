// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Process post upload
$(document).ready(function () {

    $('#createPostForm').submit(function (event) {

        event.preventDefault();

        var form = $(this);
        var url = form.attr('action');
        var formData = new FormData(form[0]);

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {

                $('#myBlogUserPostUploaded').html(data);

            },
            error: function (xhr, status, error) {
                var errorMessage = xhr.responseText || 'An error occurred.';

                alert('Error: ' + errorMessage);
            }

        });
    });


    //// Apply Select2 to the select element
    //$(document).ready(function () {
    //    $('select[name="Category"]').select2();
    //});

});