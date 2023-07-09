
/* ---------------------- Process post upload ----------------------*/
//$(document).ready(function () {

//    $('#createPostForm').submit(function (event) {

//        event.preventDefault();

//        var form = $(this);
//        var url = form.attr('action');
//        var formData = new FormData(form[0]);

//        $.ajax({
//            url: url,
//            type: 'POST',
//            data: formData,
//            processData: false,
//            contentType: false,
//            success: function (data) {

//                $('#myBlogUserPostUploaded').html(data);

//            },
//            error: function (xhr, status, error) {
//                var errorMessage = xhr.responseText || 'An error occurred.';

//                alert('Error: ' + errorMessage);
//            }

//        });
//    });


//});

///* ---------------------- QUILL LIBRARY ----------------------*/

//var customToolbar = [
//    ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
//    ['blockquote', 'code-block'],

//    [{ 'header': 1 }, { 'header': 2 }],               // custom button values
//    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
//    [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
//    [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
//    [{ 'direction': 'rtl' }],                         // text direction

//    [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
//    [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

//    [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
//    [{ 'font': [] }],
//    [{ 'align': [] }],

//    ["image", "link", "video", "formula"],

//    ['clean']                                         // remove formatting button
//];

//var options = {
//    /*debug: 'info',*/
//    modules: {
//        toolbar: customToolbar,
//    },
//    placeholder: 'Compose your post...',
//    readOnly: false,
//    theme: 'snow'
//};

//let editor = new Quill('.createPostContent', options);

//var quill = new Quill('#editor-container', {
//    modules: {
//        toolbar: [
//            ['bold', 'italic'],
//            ['link', 'blockquote', 'code-block', 'image'],
//            [{ list: 'ordered' }, { list: 'bullet' }]
//        ]
//    },
//    placeholder: 'Compose an epic...',
//    theme: 'snow'
//});

//var form = document.querySelector('.QuillCreatePostForm');
//form.onsubmit = function (e) {

//    e.preventDefault();

//    var quillContent = document.querySelector('input[name=postContent]');
//    quillContent.value = JSON.stringify(quill.getContents());

//    console.log(quill.root.innerHTML);

//    let extraData = quill.root.innerHTML;

//    var formHeader = $(this);
//    var url = formHeader.attr('action');

//    var requestData = {
//        formData: new FormData(form),
//        extraData: extraData
//    };

//    $.ajax({
//        url: url,
//        type: 'POST',
//        data: JSON.stringify(requestData),
//        processData: false,
//        contentType: false,
//        contentType: 'application/json',
//        success: function (data) {


//        },
//        error: function (xhr, status, error) {
//            var errorMessage = xhr.responseText || 'An error occurred.';

//            alert('Error: ' + errorMessage);
//        }

//    });
//    // No back end to actually submit to!

//    return false;
//};

