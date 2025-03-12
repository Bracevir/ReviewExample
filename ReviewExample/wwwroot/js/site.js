// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    let currentPage = 2;

    $('#loadMore').click(function () {
        $.get(`/Reviews/Index?page=${currentPage}`, function (data) {
            $('#reviewsTable').append(data);
            currentPage++;
        });
    });
});
